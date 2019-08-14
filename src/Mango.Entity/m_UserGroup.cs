using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_UserGroup
    {
		
        /// <summary>
        /// �û���Id
        /// </summary>
        [Key]
        public int? GroupId { get; set; }
		
        /// <summary>
        /// �û�������
        /// </summary>
        
        public string GroupName { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public bool? IsDefault { get; set; }
		
    }
}