using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mango.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860

namespace Mango.Web.Areas.User.Controllers
{
    [Area("User")]
    public class MyPostsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewModels.UserPostsViewModel model = new ViewModels.UserPostsViewModel();
            //查询帖子数据
            if (HttpContext.Session.GetString("UserId") != null)
            {
                int pageIndex = Framework.Core.Transform.GetInt(Request.Query["p"], 1);
                int pageSize = 10;
                int userId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
                PostsRepository repository = new PostsRepository();
                var query= repository.GetPostsPageList();
                model.ListData = query.Where(m => m.UserId == userId).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList(); 
                model.TotalCount = query.Count();
            }
            return View(model);
        }
        /// <summary>
        /// 帖子编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewModels.PostsEditViewModel model = new ViewModels.PostsEditViewModel();
            int postsId = id;
            int userId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
            //加载帖子数据
            PostsRepository repository = new PostsRepository();
            model.PostsData = repository.GetPostsByEdit(postsId, userId);
            if (model.PostsData != null)
            {
                //加载帖子频道数据
                Common.PostsChannel postsProperty = new Common.PostsChannel();
                model.PostsChannels = postsProperty.GetListByCache();

                return View(model);
            }
            return new ContentResult()
            {
                Content = "您的请求未得到授权!",
                StatusCode = 401
            };
        }
        [HttpPost]
        public bool Edit(ViewModels.EditPostsRequestModel model)
        {
            //
            Entity.m_Posts m = new Entity.m_Posts();
            m.PostsId = model.PostsId;
            m.Contents = model.Contents;//Framework.Core.HtmlFilter.SanitizeHtml(model.Contents);
            m.LastDate = DateTime.Now;
            m.Title = Framework.Core.HtmlFilter.StripHtml(model.Title);
            m.ChannelId = model.ChannelId;
            CommonRepository repository = new CommonRepository();
            return repository.Update(m);
        }
        /// <summary>
        /// 发布帖子
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            Common.PostsChannel postsProperty = new Common.PostsChannel();
            var model = postsProperty.GetListByCache();
            return View(model);
        }
        [HttpPost]
        public bool Add(ViewModels.AddPostsViewModel model)
        {
            //
            Entity.m_Posts m = new Entity.m_Posts();
            m.Contents = model.Contents;//Framework.Core.HtmlFilter.SanitizeHtml(model.Contents);
            m.ImgUrl = string.Empty;
            m.IsReply = true;
            m.IsShow = true;
            m.LastDate = DateTime.Now;
            m.PlusCount = 0;
            m.PostDate = DateTime.Now;
            m.Tags = "";
            m.ReadCount = 0;
            m.Title = Framework.Core.HtmlFilter.StripHtml(model.Title);
            m.UserId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
            m.AnswerCount = 0;
            m.ChannelId = model.ChannelId;
            CommonRepository repository = new CommonRepository();
            return repository.Add(m);
        }
    }
}
