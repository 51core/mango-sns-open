using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_NavigationClassify
    {
		
        /// <summary>
        /// 分类Id
        /// </summary>
        [Key]
        public int? CId { get; set; }
		
        /// <summary>
        /// 分类名称
        /// </summary>
        
        public string ClassifyName { get; set; }
		
        /// <summary>
        /// 是否显示
        /// </summary>
        
        public bool? IsShow { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public int? SortCount { get; set; }
		
    }
}