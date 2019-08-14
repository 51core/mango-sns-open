using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_PostsAttention
    {
		
        /// <summary>
        /// ��עId
        /// </summary>
        [Key]
        public int? AttentionId { get; set; }
		
        /// <summary>
        /// ����Id
        /// </summary>
        
        public int? PostsId { get; set; }
		
        /// <summary>
        /// ��עʱ��
        /// </summary>
        
        public DateTime? AttentionDate { get; set; }
		
        /// <summary>
        /// �û�Id
        /// </summary>
        
        public int? UserId { get; set; }
		
    }
}