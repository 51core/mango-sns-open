using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_Navigation
    {
		
        /// <summary>
        /// ����Id
        /// </summary>
        [Key]
        public int? NavigationId { get; set; }
		
        /// <summary>
        /// ��������
        /// </summary>
        
        public string NavigationName { get; set; }
		
        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        
        public bool? IsShow { get; set; }
		
        /// <summary>
        /// ��������
        /// </summary>
        
        public string Remark { get; set; }
		
        /// <summary>
        /// ������������
        /// </summary>
        
        public int? CId { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public string NavigationUrl { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public int? ClickCount { get; set; }
		
    }
}