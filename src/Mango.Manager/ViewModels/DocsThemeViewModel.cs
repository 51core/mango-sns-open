using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mango.Models;
namespace Mango.Manager.ViewModels
{
    public class DocsThemeViewModel
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
