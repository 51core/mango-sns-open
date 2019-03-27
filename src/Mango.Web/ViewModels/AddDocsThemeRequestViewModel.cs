using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Web.ViewModels
{
    /// <summary>
    /// 添加文档请求参数
    /// </summary>
    public class AddDocsThemeRequestViewModel
    {
        /// <summary>
        /// 文档标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 文档内容
        /// </summary>
        public string Contents { get; set; }
    }
}
