using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Web.ViewModels
{
    public class EditPostsRequestModel
    {
        /// <summary>
        /// 帖子标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 帖子内容
        /// </summary>
        public string Contents { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public int ChannelId { get; set; }
        /// <summary>
        /// 帖子ID
        /// </summary>
        public int PostsId { get; set; }
    }
}
