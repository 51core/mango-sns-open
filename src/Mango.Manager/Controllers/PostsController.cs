using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mango.Repository;
using System.Linq;
using Mango.Models;

namespace Mango.Manager.Controllers
{
    public class PostsController : Controller
    {

        #region 帖子管理
        /// <summary>
        /// 帖子管理列表
        /// </summary>
        /// <returns></returns>
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewModels.PostsViewModel model = new ViewModels.PostsViewModel();
            //查询帖子数据
            int pageIndex = Framework.Core.Transform.GetInt(Request.Query["p"], 1);
            int pageSize = 10;
            PostsRepository repository = new PostsRepository();

            var query = repository.GetPostsPageList();
            model.ListData = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            model.TotalCount = query.Count();
            return View(model);
        }
        public bool SetShow(int postsId, bool isShow)
        {
            Entity.m_Posts model = new Entity.m_Posts();
            model.PostsId = postsId;
            model.IsShow = isShow;
            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
        public bool SetReply(int postsId, bool isReply)
        {
            Entity.m_Posts model = new Entity.m_Posts();
            model.PostsId = postsId;
            model.IsReply = isReply;
            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
        #endregion
        #region 帖子标签管理
        /// <summary>
        /// 标签管理列表
        /// </summary>
        /// <returns></returns>
        public IActionResult Tags()
        {
            PostsTagsRepository repository = new PostsTagsRepository();
            List<PostsTagsModel> model = repository.GetList();
            return View(model);
        }
        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="TagsName"></param>
        /// <returns></returns>
        public bool AddTags(string tagsName)
        {
            Entity.m_PostsTags model = new Entity.m_PostsTags();
            model.TagsName = tagsName;
            CommonRepository repository = new CommonRepository();
            return repository.Add(model);
        }
        /// <summary>
        /// 编辑标签
        /// </summary>
        /// <param name="TagsName"></param>
        /// <returns></returns>
        public bool EditTags(int tagsId,string tagsName)
        {
            Entity.m_PostsTags model = new Entity.m_PostsTags();
            model.TagsId = tagsId;
            model.TagsName = tagsName;
            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
        #endregion
        #region 帖子频道管理
        public IActionResult Channel()
        {
            PostsChannelRepository repository = new PostsChannelRepository();
            List<PostsChannelModel> model = repository.GetPostsChannels().ToList();
            return View(model);
        }
        /// <summary>
        /// 添加频道
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="remarks"></param>
        /// <param name="isManager"></param>
        /// <param name="isShow"></param>
        /// <param name="sortCount"></param>
        /// <returns></returns>
        public bool AddChannel(string channelName,string remarks,bool isManager,bool isShow, int sortCount)
        {
            Entity.m_PostsChannel model = new Entity.m_PostsChannel();
            model.AppendTime = DateTime.Now;
            model.IsShow = isShow;
            model.IsManager = isManager;
            model.Remarks = remarks;
            model.ChannelName = channelName;
            model.SortCount = sortCount;
            CommonRepository repository = new CommonRepository();
            return repository.Add(model);
        }
        /// <summary>
        /// 更新帖子频道
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="channelName"></param>
        /// <param name="remarks"></param>
        /// <param name="isManager"></param>
        /// <param name="isShow"></param>
        /// <param name="sortCount"></param>
        /// <returns></returns>
        public bool UpdateChannel(int channelId,string channelName, string remarks, bool isManager, bool isShow, int sortCount)
        {
            Entity.m_PostsChannel model = new Entity.m_PostsChannel();
            model.AppendTime = DateTime.Now;
            model.IsShow = isShow;
            model.IsManager = isManager;
            model.Remarks = remarks;
            model.ChannelName = channelName;
            model.SortCount = sortCount;
            model.ChannelId = channelId;
            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
        /// <summary>
        /// 删除帖子频道
        /// </summary>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public bool DeleteChannel(int channelId)
        {
            Entity.m_PostsChannel model = new Entity.m_PostsChannel();
            model.ChannelId = channelId;
            CommonRepository repository = new CommonRepository();
            return repository.Delete(model);
        }
        #endregion
    }
}
