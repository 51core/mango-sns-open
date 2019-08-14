using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_Message
    {
		
        /// <summary>
        /// ��ϢId
        /// </summary>
        [Key]
        public int? MessageId { get; set; }

        /// <summary>
        /// ��Ϣ����
        /// ϵͳ��Ϣ: 0:ϵͳ֪ͨ
        /// ������Ϣ: 10:���ӻظ���Ϣ,11:����������Ϣ,12:���ӵ�����Ϣ,13:�ظ�������Ϣ,14:���۵�����Ϣ
        /// </summary>

        public int? MessageType { get; set; }
		
        /// <summary>
        /// ����
        /// </summary>
        
        public string Contents { get; set; }
		
        /// <summary>
        /// �ύʱ��
        /// </summary>
        
        public DateTime? PostDate { get; set; }
		
        /// <summary>
        /// ��Ϣ�����û�
        /// </summary>
        
        public int? UserId { get; set; }
		
        /// <summary>
        /// ������Ϣ�û�
        /// </summary>
        
        public int? AppendUserId { get; set; }
		
        /// <summary>
        /// �洢����Id(PostId ShareId)
        /// </summary>
        
        public int? ObjId { get; set; }
		
        /// <summary>
        /// �Ƿ��Ѿ��Ķ�
        /// </summary>
        
        public bool? IsRead { get; set; }
		
    }
}