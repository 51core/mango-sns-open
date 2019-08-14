﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Web.ViewModels
{
    public class AddDocsRequestViewModel
    {
        /// <summary>
        /// 所属文档主题
        /// </summary>
        public int ThemeId { get; set; }
        /// <summary>
        /// 文档标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 短标题
        /// </summary>
        public string ShortTitle { get; set; }
        /// <summary>
        /// 文档内容
        /// </summary>
        public string Contents { get; set; }
    }
}
