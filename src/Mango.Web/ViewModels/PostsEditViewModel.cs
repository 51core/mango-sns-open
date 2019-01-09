using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Web.ViewModels
{
    public class PostsEditViewModel
    {
        /// <summary>
        /// 帖子频道列表
        /// </summary>
        public List<Models.PostsChannelModel> PostsChannels { get; set; }
        /// <summary>
        /// 帖子信息
        /// </summary>
        public Entity.m_Posts PostsData { get; set; }
    }
}
