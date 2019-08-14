using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_Posts
    {
		
        /// <summary>
        /// ����Id
        /// </summary>
        [Key]
        public int? PostsId { get; set; }
		
        /// <summary>
        /// ���ӱ���
        /// </summary>
        
        public string Title { get; set; }
		
        /// <summary>
        /// ��������
        /// </summary>
        
        public string Contents { get; set; }
		
        /// <summary>
        /// ����ʱ��
        /// </summary>
        
        public DateTime? PostDate { get; set; }
		
        /// <summary>
        /// ������ʱ��
        /// </summary>
        
        public DateTime? LastDate { get; set; }
		
        /// <summary>
        /// �û�Id
        /// </summary>
        
        public int? UserId { get; set; }
		
        /// <summary>
        /// +1��
        /// </summary>
        
        public int? PlusCount { get; set; }
		
        /// <summary>
        /// �Ķ�����
        /// </summary>
        
        public int? ReadCount { get; set; }
		
        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        
        public bool? IsShow { get; set; }
		
        /// <summary>
        /// ����Id
        /// </summary>
        
        public string Tags { get; set; }
		
        /// <summary>
        /// ͼƬ��ַ
        /// </summary>
        
        public string ImgUrl { get; set; }
		
        /// <summary>
        /// �Ƿ�����ظ�
        /// </summary>
        
        public bool? IsReply { get; set; }
		
        /// <summary>
        /// �ظ���
        /// </summary>
        
        public int? AnswerCount { get; set; }
		
        /// <summary>
        /// ����Ƶ��
        /// </summary>
        
        public int? ChannelId { get; set; }
		
    }
}