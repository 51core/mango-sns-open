using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_WebSiteNavigation
    {
		
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int? NavigationId { get; set; }
		
        /// <summary>
        /// ���ӵ�ַ
        /// </summary>
        
        public string LinkUrl { get; set; }
		
        /// <summary>
        /// ��������
        /// </summary>
        
        public string NavigationName { get; set; }
		
        /// <summary>
        /// �Ƿ�Ϊ��ת���´���
        /// </summary>
        
        public bool? IsTarget { get; set; }
		
        /// <summary>
        /// ���ʱ��
        /// </summary>
        
        public DateTime? AppendTime { get; set; }
		
        /// <summary>
        /// ����(��С����)
        /// </summary>
        
        public int? SortCount { get; set; }

        /// <summary>
        /// �Ƿ���ʾ��ǰ��
        /// </summary>

        public bool? IsShow { get; set; }

    }
}