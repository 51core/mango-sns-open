using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_PostsAnswerRecords
    {
		
        /// <summary>
        /// 记录Id
        /// </summary>
        [Key]
        public int? RecordsId { get; set; }
		
        /// <summary>
        /// 回答Id
        /// </summary>
        
        public int? AnswerId { get; set; }
		
        /// <summary>
        /// 添加人
        /// </summary>
        
        public int? UserId { get; set; }
		
        /// <summary>
        /// 添加时间
        /// </summary>
        
        public DateTime? AppendTime { get; set; }
		
        /// <summary>
        /// 记录类型 1表示点赞操作 2表示反对操作
        /// </summary>
        
        public int? RecordsType { get; set; }
		
    }
}