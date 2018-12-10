using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_PostsTags
    {
		
        /// <summary>
        ///  Ù–‘Id
        /// </summary>
        [Key]
        public int? TagsId { get; set; }
		
        /// <summary>
        ///  Ù–‘√˚≥∆
        /// </summary>
        
        public string TagsName { get; set; }
		
    }
}