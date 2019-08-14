using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_PostsAnswer
    {
		
        /// <summary>
        /// 回答Id
        /// </summary>
        [Key]
        public int? AnswerId { get; set; }
		
        /// <summary>
        /// 回答内容
        /// </summary>
        
        public string Contents { get; set; }
		
        /// <summary>
        /// 回答时间
        /// </summary>
        
        public DateTime? PostDate { get; set; }
		
        /// <summary>
        /// 回答人
        /// </summary>
        
        public int? UserId { get; set; }
		
        /// <summary>
        /// 所属问题帖子Id
        /// </summary>
        
        public int? PostsId { get; set; }
		
        /// <summary>
        /// +1数
        /// </summary>
        
        public int? Plus { get; set; }
		
        /// <summary>
        /// 评论回复数
        /// </summary>
        
        public int? CommentsCount { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public bool? IsShow { get; set; }
		
    }
}