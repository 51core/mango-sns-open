using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mango.Models;
namespace Mango.Web.ViewModels
{
    public class DocsViewModel
    {
        /// <summary>
        /// 分页返回总记录
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 分页数据返回集合
        /// </summary>
        public List<DocsThemeModel> ListData { get; set; }
    }
}
