using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_ManagerRole
    {
		
        /// <summary>
        /// ��ɫId
        /// </summary>
        [Key]
        public int? RoleId { get; set; }
		
        /// <summary>
        /// ��ɫ����
        /// </summary>
        
        public string RoleName { get; set; }
		
    }
}