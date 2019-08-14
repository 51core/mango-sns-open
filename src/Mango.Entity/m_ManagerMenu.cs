using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_ManagerMenu
    {
		
        /// <summary>
        /// �˵�Id
        /// </summary>
        [Key]
        public int? MenuId { get; set; }
		
        /// <summary>
        /// �˵�����
        /// </summary>
        
        public string MenuName { get; set; }
		
        /// <summary>
        /// �˵���ַ
        /// </summary>
        
        public string AreaName { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public string ControllerName { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public string ActionName { get; set; }
		
        /// <summary>
        /// �����ϼ��˵�
        /// </summary>
        
        public int? ParentId { get; set; }
		
        /// <summary>
        /// �˵�����
        /// </summary>
        
        public bool? IsPower { get; set; }
		
    }
}