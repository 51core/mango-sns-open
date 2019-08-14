using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_PostsAnswer
    {
		
        /// <summary>
        /// �ش�Id
        /// </summary>
        [Key]
        public int? AnswerId { get; set; }
		
        /// <summary>
        /// �ش�����
        /// </summary>
        
        public string Contents { get; set; }
		
        /// <summary>
        /// �ش�ʱ��
        /// </summary>
        
        public DateTime? PostDate { get; set; }
		
        /// <summary>
        /// �ش���
        /// </summary>
        
        public int? UserId { get; set; }
		
        /// <summary>
        /// ������������Id
        /// </summary>
        
        public int? PostsId { get; set; }
		
        /// <summary>
        /// +1��
        /// </summary>
        
        public int? Plus { get; set; }
		
        /// <summary>
        /// ���ۻظ���
        /// </summary>
        
        public int? CommentsCount { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public bool? IsShow { get; set; }
		
    }
}