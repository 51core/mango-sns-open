using System;
using System.Collections.Generic;
using System.Text;
using Mango.Models;
namespace Mango.Web.ViewModels
{
    public class NavigationViewModel
    {
        /// <summary>
        /// 导航分类数据
        /// </summary>
        public List<NavigationClassifyModel> ClassifyListData { get; set; }
    }
}
