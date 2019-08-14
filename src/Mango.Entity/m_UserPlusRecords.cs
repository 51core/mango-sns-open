using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Mango.Entity
{
    public class m_UserPlusRecords
    {
        /// <summary>
        /// 记录Id
        /// </summary>
        [Key]
        public int? RecordsId { get; set; }

        /// <summary>
        /// 点赞对象ID
        /// </summary>

        public int? ObjectId { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>

        public int? UserId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>

        public DateTime? AppendTime { get; set; }

        /// <summary>
        /// 记录类型 1 帖子点赞 2 帖子回答点赞 3 帖子评论点赞 4 文档主题点赞 5 文档点赞
        /// </summary>

        public int? RecordsType { get; set; }
    }
}
