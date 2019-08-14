using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_ManagerMenu
    {
		
        /// <summary>
        /// 菜单Id
        /// </summary>
        [Key]
        public int? MenuId { get; set; }
		
        /// <summary>
        /// 菜单名称
        /// </summary>
        
        public string MenuName { get; set; }
		
        /// <summary>
        /// 菜单地址
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
        /// 所属上级菜单
        /// </summary>
        
        public int? ParentId { get; set; }
		
        /// <summary>
        /// 菜单级别
        /// </summary>
        
        public bool? IsPower { get; set; }
		
    }
}