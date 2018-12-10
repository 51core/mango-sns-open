using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Web.ViewModels
{
    public class WebSiteViewModel
    {
        /// <summary>
        /// 网站基础配置数据
        /// </summary>
        public Models.WebSiteConfigModel WebSiteConfigData { get; set; }
        /// <summary>
        /// 顶部导航数据
        /// </summary>
        public List<Models.WebSiteNavigationModel> WebSiteNavigationData { get; set; }

    }
}
