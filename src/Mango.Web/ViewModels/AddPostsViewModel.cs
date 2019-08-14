using System.ComponentModel.DataAnnotations;
namespace Mango.Web.ViewModels
{
    public class AddPostsViewModel
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
        
    }
}
