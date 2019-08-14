using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_Sms
    {
		
        /// <summary>
        /// ����ID
        /// </summary>
        [Key]
        public int? SmsID { get; set; }
		
        /// <summary>
        /// �����ֻ���
        /// </summary>
        
        public string Phone { get; set; }
		
        /// <summary>
        /// ��������
        /// </summary>
        
        public string Contents { get; set; }
		
        /// <summary>
        /// ����ʱ��
        /// </summary>
        
        public DateTime? SendTime { get; set; }
		
        /// <summary>
        /// ����IP��ַ
        /// </summary>
        
        public string SendIP { get; set; }
		
        /// <summary>
        /// �Ƿ��ͳɹ�
        /// </summary>
        
        public bool? IsOk { get; set; }
	
    }
}