using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_NavigationClassify
    {
		
        /// <summary>
        /// ����Id
        /// </summary>
        [Key]
        public int? CId { get; set; }
		
        /// <summary>
        /// ��������
        /// </summary>
        
        public string ClassifyName { get; set; }
		
        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        
        public bool? IsShow { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public int? SortCount { get; set; }
		
    }
}