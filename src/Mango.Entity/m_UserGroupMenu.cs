using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_UserGroupMenu
    {
		
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int? MId { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public string MName { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public string AreaName { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public string ControllerName { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public string ActionName { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public int? ParentId { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public bool? IsPower { get; set; }
		
    }
}