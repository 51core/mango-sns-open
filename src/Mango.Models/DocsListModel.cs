using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Models
{
    public class DocsListModel
    {
        /// <summary>
        /// 文档项ID
        /// </summary>
        public int DocsId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>

        public string Title { get; set; }

        /// <summary>
        /// 短标题
        /// </summary>

        public string ShortTitle { get; set; }
        /// <summary>
        /// 所属文档主题
        /// </summary>

        public int ThemeId { get; set; }
        public bool IsShow { get; set; }
    }
}
