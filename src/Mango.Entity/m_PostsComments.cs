using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_PostsComments
    {
		
        /// <summary>
        /// ����Id
        /// </summary>
        [Key]
        public int? CommentId { get; set; }
		
        /// <summary>
        /// ��������
        /// </summary>
        
        public string Contents { get; set; }
		
        /// <summary>
        /// ����ʱ��
        /// </summary>
        
        public DateTime? PostDate { get; set; }
		
        /// <summary>
        /// �û�Id
        /// </summary>
        
        public int? UserId { get; set; }
		
        /// <summary>
        /// ��Ӧ�û�Id
        /// </summary>
        
        public int? ToUserId { get; set; }
		
        /// <summary>
        /// �ش�Id
        /// </summary>
        
        public int? AnswerId { get; set; }
		
        /// <summary>
        /// �����Ƿ���ʾ
        /// </summary>
        
        public bool? IsShow { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public int? Plus { get; set; }
		
    }
}