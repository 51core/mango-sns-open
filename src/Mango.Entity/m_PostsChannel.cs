using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_PostsChannel
    {
		
        /// <summary>
        /// Ƶ��ID
        /// </summary>
        [Key]
        public int? ChannelId { get; set; }
		
        /// <summary>
        /// Ƶ������
        /// </summary>
        
        public string ChannelName { get; set; }
		
        /// <summary>
        /// ��ע��Ϣ
        /// </summary>
        
        public string Remarks { get; set; }
		
        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        
        public bool? IsShow { get; set; }
		
        /// <summary>
        /// Ƶ������ʱ��
        /// </summary>
        
        public DateTime? AppendTime { get; set; }
		
        /// <summary>
        /// �Ƿ�ֻ�������Ա����
        /// </summary>
        
        public bool? IsManager { get; set; }
        /// <summary>
        /// ����(��С����)
        /// </summary>
        public int? SortCount { get; set; }
    }
}