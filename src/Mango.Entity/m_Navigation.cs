using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_Navigation
    {
		
        /// <summary>
        /// 导航Id
        /// </summary>
        [Key]
        public int? NavigationId { get; set; }
		
        /// <summary>
        /// 导航名称
        /// </summary>
        
        public string NavigationName { get; set; }
		
        /// <summary>
        /// 是否显示
        /// </summary>
        
        public bool? IsShow { get; set; }
		
        /// <summary>
        /// 导航描述
        /// </summary>
        
        public string Remark { get; set; }
		
        /// <summary>
        /// 所属导航分类
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