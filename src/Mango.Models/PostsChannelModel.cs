using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Models
{
    public class PostsChannelModel
    {
        /// <summary>
        /// 频道ID
        /// </summary>
        public int ChannelId { get; set; }

        /// <summary>
        /// 频道名称
        /// </summary>

        public string ChannelName { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>

        public string Remarks { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>

        public bool IsShow { get; set; }

        /// <summary>
        /// 频道创建时间
        /// </summary>

        public DateTime AppendTime { get; set; }

        /// <summary>
        /// 是否只允许管理员发帖
        /// </summary>

        public bool IsManager { get; set; }
        /// <summary>
        /// 排序(从小到大)
        /// </summary>
        public int SortCount { get; set; }
    }
}
