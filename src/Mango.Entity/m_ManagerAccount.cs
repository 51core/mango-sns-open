using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_ManagerAccount
    {
		
        /// <summary>
        /// 管理员Id
        /// </summary>
        [Key]
        public int? AdminId { get; set; }
		
        /// <summary>
        /// 管理员这账号
        /// </summary>
        
        public string AdminName { get; set; }
		
        /// <summary>
        /// 密码
        /// </summary>
        
        public string Password { get; set; }
		
        /// <summary>
        /// 状态
        /// </summary>
        
        public bool? IsStatus { get; set; }
		
        /// <summary>
        /// 角色Id
        /// </summary>
        
        public int? RoleId { get; set; }
		
    }
}