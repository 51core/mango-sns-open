using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_AppManager
    {
		
        /// <summary>
        /// Ӧ��ID
        /// </summary>
        [Key]
        public int? AppId { get; set; }
		
        /// <summary>
        /// Ӧ������
        /// </summary>
        
        public string AppName { get; set; }
		
        /// <summary>
        /// ��ע��Ϣ
        /// </summary>
        
        public string RemarkText { get; set; }
		
        /// <summary>
        /// �Ƿ��ѹر�
        /// </summary>
        
        public bool? IsOpen { get; set; }
		
        /// <summary>
        /// ��������API�Ŀ��ŵ�ַ
        /// </summary>
        
        public string OpenUrl { get; set; }
		
        /// <summary>
        /// ��������API��IP��ַ������
        /// </summary>
        
        public string OpenIP { get; set; }
		
        /// <summary>
        /// APP��Կ��
        /// </summary>
        
        public string AppKey { get; set; }
		
        /// <summary>
        /// APP��Կ
        /// </summary>
        
        public string AppSecret { get; set; }
		
        /// <summary>
        /// ���ʱ��
        /// </summary>
        
        public DateTime? AppendTime { get; set; }
		
        /// <summary>
        /// ������ʱ��
        /// </summary>
        
        public DateTime? LastTime { get; set; }
		
    }
}