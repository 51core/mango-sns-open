using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_ManagerPower
    {
		
        /// <summary>
        /// Ȩ��Id
        /// </summary>
        [Key]
        public int? PowerId { get; set; }
		
        /// <summary>
        /// �˵�Id
        /// </summary>
        
        public int? MenuId { get; set; }
		
        /// <summary>
        /// ��ɫId
        /// </summary>
        
        public int? RoleId { get; set; }
		
    }
}