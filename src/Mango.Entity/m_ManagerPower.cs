using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_ManagerPower
    {
		
        /// <summary>
        /// 权限Id
        /// </summary>
        [Key]
        public int? PowerId { get; set; }
		
        /// <summary>
        /// 菜单Id
        /// </summary>
        
        public int? MenuId { get; set; }
		
        /// <summary>
        /// 角色Id
        /// </summary>
        
        public int? RoleId { get; set; }
		
    }
}