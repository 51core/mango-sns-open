using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_UserGroupPower
    {
		
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int? PId { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public int? MId { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public int? GroupId { get; set; }
		
    }
}