using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_PostsTags
    {
		
        /// <summary>
        /// ����Id
        /// </summary>
        [Key]
        public int? TagsId { get; set; }
		
        /// <summary>
        /// ��������
        /// </summary>
        
        public string TagsName { get; set; }
		
    }
}