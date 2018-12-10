using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_UserGroup
    {
		
        /// <summary>
        /// 用户组Id
        /// </summary>
        [Key]
        public int? GroupId { get; set; }
		
        /// <summary>
        /// 用户组名称
        /// </summary>
        
        public string GroupName { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public bool? IsDefault { get; set; }
		
    }
}