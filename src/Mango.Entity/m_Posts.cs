using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_Posts
    {
		
        /// <summary>
        /// 帖子Id
        /// </summary>
        [Key]
        public int? PostsId { get; set; }
		
        /// <summary>
        /// 帖子标题
        /// </summary>
        
        public string Title { get; set; }
		
        /// <summary>
        /// 帖子内容
        /// </summary>
        
        public string Contents { get; set; }
		
        /// <summary>
        /// 发布时间
        /// </summary>
        
        public DateTime? PostDate { get; set; }
		
        /// <summary>
        /// 最后更新时间
        /// </summary>
        
        public DateTime? LastDate { get; set; }
		
        /// <summary>
        /// 用户Id
        /// </summary>
        
        public int? UserId { get; set; }
		
        /// <summary>
        /// +1数
        /// </summary>
        
        public int? PlusCount { get; set; }
		
        /// <summary>
        /// 阅读次数
        /// </summary>
        
        public int? ReadCount { get; set; }
		
        /// <summary>
        /// 是否显示
        /// </summary>
        
        public bool? IsShow { get; set; }
		
        /// <summary>
        /// 属性Id
        /// </summary>
        
        public string Tags { get; set; }
		
        /// <summary>
        /// 图片地址
        /// </summary>
        
        public string ImgUrl { get; set; }
		
        /// <summary>
        /// 是否允许回复
        /// </summary>
        
        public bool? IsReply { get; set; }
		
        /// <summary>
        /// 回复数
        /// </summary>
        
        public int? AnswerCount { get; set; }
		
        /// <summary>
        /// 所属频道
        /// </summary>
        
        public int? ChannelId { get; set; }
		
    }
}