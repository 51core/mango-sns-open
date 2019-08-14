using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_User
    {
		
        /// <summary>
        /// �û�Id
        /// </summary>
        [Key]
        public int? UserId { get; set; }
		
        /// <summary>
        /// �û��˺���
        /// </summary>
        
        public string AccountName { get; set; }
		
        /// <summary>
        /// ��½����
        /// </summary>
        
        public string Password { get; set; }
		
        /// <summary>
        /// �ǳ�
        /// </summary>
        
        public string NickName { get; set; }
		
        /// <summary>
        /// ע��ʱ��
        /// </summary>
        
        public DateTime? RegisterDate { get; set; }
		
        /// <summary>
        /// ����½ʱ��
        /// </summary>
        
        public DateTime? LastLoginDate { get; set; }
		
        /// <summary>
        /// ����½IP
        /// </summary>
        
        public string LastLoginIP { get; set; }
		
        /// <summary>
        /// ע��IP��ַ
        /// </summary>
        
        public string RegisterIP { get; set; }
		
        /// <summary>
        /// �û�״̬(true:����
        /// </summary>
        
        public bool? IsStatus { get; set; }
		
        /// <summary>
        /// �û�ͷ���ַ
        /// </summary>
        
        public string HeadUrl { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public int? GroupId { get; set; }
		
        /// <summary>
        /// �ֻ���
        /// </summary>
        
        public string Phone { get; set; }
		
        /// <summary>
        /// ����ƽ̨Id
        /// </summary>
        
        public string OpenId { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public string Email { get; set; }
		
        /// <summary>
        /// ������Ϣ
        /// </summary>
        
        public string AddressInfo { get; set; }
		
        /// <summary>
        /// ����
        /// </summary>
        
        public string Birthday { get; set; }
		
        /// <summary>
        /// ���˱�ǩ
        /// </summary>
        
        public string Tags { get; set; }
		
        /// <summary>
        /// �Ա�
        /// </summary>
        
        public string Sex { get; set; }
		
    }
}