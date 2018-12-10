using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_ManagerRole
    {
		
        /// <summary>
        /// ½ÇÉ«Id
        /// </summary>
        [Key]
        public int? RoleId { get; set; }
		
        /// <summary>
        /// ½ÇÉ«Ãû³Æ
        /// </summary>
        
        public string RoleName { get; set; }
		
    }
}