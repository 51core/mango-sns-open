using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_ManagerAccount
    {
		
        /// <summary>
        /// ����ԱId
        /// </summary>
        [Key]
        public int? AdminId { get; set; }
		
        /// <summary>
        /// ����Ա���˺�
        /// </summary>
        
        public string AdminName { get; set; }
		
        /// <summary>
        /// ����
        /// </summary>
        
        public string Password { get; set; }
		
        /// <summary>
        /// ״̬
        /// </summary>
        
        public bool? IsStatus { get; set; }
		
        /// <summary>
        /// ��ɫId
        /// </summary>
        
        public int? RoleId { get; set; }
		
    }
}