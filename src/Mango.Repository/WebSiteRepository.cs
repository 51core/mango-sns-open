using System.Collections.Generic;
using Mango.Framework.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using System.Linq;

namespace Mango.Repository
{
    public class WebSiteRepository
    {
        private EFDbContext _dbContext = null;
        public WebSiteRepository()
        {
            _dbContext = new EFDbContext();
        }
        /// <summary>
        /// 获取网站系统配置信息
        /// </summary>
        /// <returns></returns>
        public Models.WebSiteConfigModel GetWebSiteConfig()
        {
            return _dbContext.m_WebSiteConfig
                .OrderBy(cfg=>cfg.ConfigId)
                .Select(cfg => new Models.WebSiteConfigModel()
                {
                    ConfigId = cfg.ConfigId.Value,
                    CopyrightText = cfg.CopyrightText,
                    FilingNo = cfg.FilingNo,
                    IsLogin = cfg.IsLogin.Value,
                    IsRegister = cfg.IsRegister.Value,
                    WebSiteDescription = cfg.WebSiteDescription,
                    WebSiteKeyWord = cfg.WebSiteKeyWord,
                    WebSiteName = cfg.WebSiteName,
                    WebSiteTitle = cfg.WebSiteTitle,
                    WebSiteUrl = cfg.WebSiteUrl
                })
                .FirstOrDefault();
        }
        /// <summary>
        /// 获取网站顶部导航数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<Models.WebSiteNavigationModel> GetWebSiteNavigations()
        {
            return _dbContext.m_WebSiteNavigation
                .OrderBy(nav => nav.SortCount)
                .Select(nav => new Models.WebSiteNavigationModel()
                {
                    AppendTime = nav.AppendTime.Value,
                    IsShow = nav.IsShow.Value,
                    IsTarget = nav.IsTarget.Value,
                    LinkUrl = nav.LinkUrl,
                    NavigationId = nav.NavigationId.Value,
                    NavigationName = nav.NavigationName,
                    SortCount = nav.SortCount.Value
                });
        }
    }
}
