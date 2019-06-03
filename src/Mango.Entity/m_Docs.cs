using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_Docs
    {
		
        /// <summary>
        /// �ĵ���ID
        /// </summary>
        [Key]
        public int? DocsId { get; set; }
		
        /// <summary>
        /// ����
        /// </summary>
        
        public string Title { get; set; }
		
        /// <summary>
        /// �̱���
        /// </summary>
        
        public string ShortTitle { get; set; }
		
        /// <summary>
        /// ����
        /// </summary>
        
        public string Contents { get; set; }
		
        /// <summary>
        /// ���ʱ��
        /// </summary>
        
        public DateTime? AppendTime { get; set; }
		
        /// <summary>
        /// ������ʱ��
        /// </summary>
        
        public DateTime? LastTime { get; set; }
		
        /// <summary>
        /// ��������
        /// </summary>
        
        public int? ThemeId { get; set; }
		
        /// <summary>
        /// ������
        /// </summary>
        
        public int? UserId { get; set; }
		
        /// <summary>
        /// �����
        /// </summary>
        
        public int? ReadCount { get; set; }
		
        /// <summary>
        /// +1��
        /// </summary>
        
        public int? PlusCount { get; set; }
		
        /// <summary>
        /// ��ǩ
        /// </summary>
        
        public string Tags { get; set; }
		
        /// <summary>
        /// ֧�ֵİ汾��Ϣ
        /// </summary>
        
        public string VersionText { get; set; }
		
        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        
        public bool? IsShow { get; set; }
		
        /// <summary>
        /// �Ƿ��Ѿ����
        /// </summary>
        
        public bool? IsAudit { get; set; }
		
    }
}