using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Models
{
    public class NavigationModel
    {
        /// <summary>
        /// 导航Id
        /// </summary>
        public int NavigationId { get; set; }

        /// <summary>
        /// 导航名称
        /// </summary>
        public string NavigationName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 导航描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 所属导航分类
        /// </summary>
        public int CId { get; set; }
        /// <summary>
        /// 导航链接
        /// </summary>
        public string NavigationUrl { get; set; }
        /// <summary>
        /// 点击次数
        /// </summary>
        public int ClickCount { get; set; }
    }
}
