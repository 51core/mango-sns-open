using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mango.Repository;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.SignalR;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860
namespace Mango.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IHubContext<Extensions.MessageHub> _messageHubContext;
        public PostsController(IHubContext<Extensions.MessageHub> messageHubContext)
        {
            _messageHubContext = messageHubContext;
        }
        public IActionResult Index()
        {
            
            ViewModels.PostsViewModel model = LoadMainData(0);
            
            return View(model);
        }
        [Route("posts/channel/{id}")]
        public IActionResult Index(int id = 0)
        {
            ViewModels.PostsViewModel model = LoadMainData(id);
            return View(model);
        }
        public ViewModels.PostsViewModel LoadMainData(int id)
        {
            ViewModels.PostsViewModel model = new ViewModels.PostsViewModel();
            //查询帖子数据
            int pageIndex = Framework.Core.Transform.GetInt(Request.Query["p"], 1);
            int pageSize = 10;
            PostsRepository repository = new PostsRepository();
            var query = repository.GetPostsPageList();

            model.ListData = query.Where(m => (id > 0 ? m.ChannelId == id : m.ChannelId > 0)&&m.IsShow==true).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            model.TotalCount = query.Where(m => (id > 0 ? m.ChannelId == id : m.ChannelId > 0) && m.IsShow == true).Count();

            //加载帖子频道
            Common.PostsChannel postsProperty = new Common.PostsChannel();
            model.PostsChannelData = postsProperty.GetListByCache();
            //加载热门帖子
            model.HotListData = query.OrderByDescending(m => m.ReadCount).Where(m => m.PostDate >= DateTime.Now.AddDays(-7)).Skip(0).Take(10).ToList();
            return model;
        }
        /// <summary>
        /// 帖子内容阅读
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Read(string id)
        {
            int postsId = Framework.Core.Transform.GetInt(id, 0);
            ViewModels.PostsReadViewModel model = new ViewModels.PostsReadViewModel();
            if (postsId != 0)
            {
                PostsRepository repository = new PostsRepository();
                model.PostsData = repository.GetPosts(postsId)[0];
                int pageSize = 10;
                int pageIndex = Framework.Core.Transform.GetInt(Request.Query["p"], 1);

                var query = repository.GetAnswerPageList();
                model.AnswerListData = query.Where(m => m.PostsId == postsId).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
                model.TotalCount = query.Where(m => m.PostsId == postsId).Count();
                //加载帖子频道
                Common.PostsChannel postsProperty = new Common.PostsChannel();
                model.PostsChannelData = postsProperty.GetListByCache();
                //加载热门帖子
                model.HotListData = repository.GetPostsListByHot().OrderByDescending(m => m.PlusCount).Where(m => m.PostDate >= DateTime.Now.AddDays(-7)&&m.IsShow==true).Skip(0).Take(10).ToList();
            }
            return View(model);
        }
        
        #region 帖子回答
        /// <summary>
        /// 添加回复
        /// </summary>
        /// <param name="postsId"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        public bool AddAnswer(int postsId,string contents,string title)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(contents))
            {
                PostsRepository repository = new PostsRepository();
                //
                Entity.m_PostsAnswer model = new Entity.m_PostsAnswer();
                model.CommentsCount = 0;
                model.Contents = contents;
                model.IsShow = true;
                model.Plus = 0;
                model.PostDate = DateTime.Now;
                model.PostsId = postsId;
                model.UserId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
                //消息实体
                Entity.m_Message message = new Entity.m_Message();
                message.AppendUserId = model.UserId;
                message.Contents = Common.MessageHtml.GetMessageContent(HttpContext.Session.GetString("NickName"), postsId, title, 10);
                message.IsRead = false;
                message.MessageType = 10;
                message.ObjId = postsId;
                message.PostDate = DateTime.Now;
                message.UserId = repository.GetPostsByUserId(postsId);
                //保存
                result = repository.AddAnswer(model, message);
                if (result)
                {
                    Extensions.SignalRCore.PushUserMessage(message.UserId.ToString(), _messageHubContext);
                }
            }
            return result;
        }
        /// <summary>
        /// 添加回答评论
        /// </summary>
        /// <param name="answerId"></param>
        /// <param name="contents"></param>
        /// <param name="toUserId"></param>
        /// <returns></returns>
        public bool AddAnswerComments(int answerId, string contents, int toUserId, int postsId, string title)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(contents))
            {
                PostsRepository repository = new PostsRepository();
                Entity.m_PostsComments model = new Entity.m_PostsComments();
                model.Contents = contents;
                model.IsShow = true;
                model.PostDate = DateTime.Now;
                model.AnswerId = answerId;
                model.ToUserId = toUserId;
                model.UserId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
                model.Plus = 0;
                //消息通知
                Entity.m_Message message = new Entity.m_Message();
                message.AppendUserId = model.UserId;
                message.Contents = Common.MessageHtml.GetMessageContent(HttpContext.Session.GetString("NickName"), postsId, title, 11);
                message.IsRead = false;
                message.MessageType = 11;
                message.ObjId = postsId;
                message.PostDate = DateTime.Now;
                message.UserId = repository.GetPostsAnswerByUserId(answerId);
                //
                result = repository.AddAnswerComments(model, message);
                if (result)
                {
                    Extensions.SignalRCore.PushUserMessage(message.UserId.ToString(), _messageHubContext);
                }
            }
            return result;
        }
        /// <summary>
        /// 加载评论数据
        /// </summary>
        /// <param name="answerId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public string LoadComments(int answerId, int pageIndex, int pageSize = 6)
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            //条件处理
            PostsRepository repository = new PostsRepository();
            var query = repository.GetCommentsPageList();
            var queryResult = query.Where(m => m.AnswerId == answerId).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            return JsonConvert.SerializeObject(queryResult, timeFormat);
        }
        /// <summary>
        /// 加载评论数据总记录数
        /// </summary>
        /// <param name="answerId"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public int LoadCommentsByTotal(int answerId, int pageSize = 6)
        {
            //条件处理
            PostsRepository repository = new PostsRepository();
            var query = repository.GetCommentsPageList();
            return query.Where(m => m.AnswerId == answerId).Count();
        }
        #endregion
    }
}
