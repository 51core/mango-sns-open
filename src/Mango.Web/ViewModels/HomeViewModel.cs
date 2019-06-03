using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mango.Models;
namespace Mango.Web.ViewModels
{
    public class HomeViewModel
    {
        /// <summary>
        /// 新帖子数据
        /// </summary>
        public List<PostsModel> PostsDatas { get; set; }
        /// <summary>
        /// 新文档主题数据
        /// </summary>
        public List<DocsThemeModel> DocsThemeDatas { get; set; }
        /// <summary>
        /// 推荐导航数据
        /// </summary>
        public List<NavigationModel> NavigationDatas { get; set; }
    }
}
