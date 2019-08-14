using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_WebSiteConfig
    {
		
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int? ConfigId { get; set; }
		
        /// <summary>
        /// ��վ����
        /// </summary>
        
        public string WebSiteName { get; set; }
		
        /// <summary>
        /// ��վ��ַ
        /// </summary>
        
        public string WebSiteUrl { get; set; }
		
        /// <summary>
        /// ��վ����
        /// </summary>
        
        public string WebSiteTitle { get; set; }
		
        /// <summary>
        /// ��վ�ؼ���
        /// </summary>
        
        public string WebSiteKeyWord { get; set; }
		
        /// <summary>
        /// ��վ����
        /// </summary>
        
        public string WebSiteDescription { get; set; }
		
        /// <summary>
        /// �ײ���Ȩ����
        /// </summary>
        
        public string CopyrightText { get; set; }
		
        /// <summary>
        /// �Ƿ񿪷ŵ�¼
        /// </summary>
        
        public bool? IsLogin { get; set; }
		
        /// <summary>
        /// �Ƿ񿪷�ע��
        /// </summary>
        
        public bool? IsRegister { get; set; }
        /// <summary>
        /// ��վ������
        /// </summary>
        public string FilingNo { get; set; }


    }
}