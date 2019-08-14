using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Mango.Web.ViewModels
{
    public class DocsReadViewModel
    {
        /// <summary>
        /// 文档ID
        /// </summary>
        public int DocsId { get; set; }
        /// <summary>
        /// 文档主题ID
        /// </summary>
        public int ThemeId { get; set; }
        /// <summary>
        /// 文档主题数据
        /// </summary>
        public Mango.Models.DocsThemeModel DocsThemeData { get; set; }
        /// <summary>
        /// 文档数据
        /// </summary>
        public Mango.Models.DocsModel DocsData { get; set; }
        /// <summary>
        /// 文档列表数据
        /// </summary>
        public List<Mango.Models.DocsListModel> ItemsListData { get; set; }
    }
}
