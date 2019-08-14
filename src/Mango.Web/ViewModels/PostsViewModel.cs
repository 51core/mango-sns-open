using System;
using System.Collections.Generic;
using System.Text;
using Mango.Models;
namespace Mango.Web.ViewModels
{
    public class PostsViewModel
    {
        /// <summary>
        /// 分页返回总记录
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 分页数据返回集合
        /// </summary>
        public List<PostsModel> ListData { get; set; }
        /// <summary>
        /// 帖子频道
        /// </summary>
        public List<PostsChannelModel> PostsChannelData { get; set; }
        /// <summary>
        /// 一周热门帖子
        /// </summary>
        public List<PostsModel> HotListData { get; set; }
    }
}
