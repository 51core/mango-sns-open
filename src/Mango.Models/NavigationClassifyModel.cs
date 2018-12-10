using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Models
{
    public class NavigationClassifyModel
    {
        /// <summary>
        /// 分类Id
        /// </summary>
        public int CId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string ClassifyName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int SortCount { get; set; }
    }
}
