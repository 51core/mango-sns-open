using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_PostsComments
    {
		
        /// <summary>
        /// 评论Id
        /// </summary>
        [Key]
        public int? CommentId { get; set; }
		
        /// <summary>
        /// 评论内容
        /// </summary>
        
        public string Contents { get; set; }
		
        /// <summary>
        /// 评论时间
        /// </summary>
        
        public DateTime? PostDate { get; set; }
		
        /// <summary>
        /// 用户Id
        /// </summary>
        
        public int? UserId { get; set; }
		
        /// <summary>
        /// 回应用户Id
        /// </summary>
        
        public int? ToUserId { get; set; }
		
        /// <summary>
        /// 回答Id
        /// </summary>
        
        public int? AnswerId { get; set; }
		
        /// <summary>
        /// 评论是否显示
        /// </summary>
        
        public bool? IsShow { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public int? Plus { get; set; }
		
    }
}