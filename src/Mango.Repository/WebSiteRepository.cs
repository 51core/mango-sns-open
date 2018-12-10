using System.Collections.Generic;
using Mango.Framework.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Linq;

namespace Mango.Repository
{
    public class WebSiteRepository
    {
        /// <summary>
        /// 获取网站系统配置信息
        /// </summary>
        /// <returns></returns>
        public Models.WebSiteConfigModel GetWebSiteConfig()
        {
            EFDbContext dbContext = new EFDbContext();
            var query = from cfg in dbContext.m_WebSiteConfig
                        orderby cfg.ConfigId ascending
                        select new Models.WebSiteConfigModel()
                        {
                            ConfigId = cfg.ConfigId.Value,
                            CopyrightText = cfg.CopyrightText,
                            FilingNo = cfg.FilingNo,
                            IsLogin = cfg.IsLogin.Value,
                            IsRegister = cfg.IsRegister.Value,
                            WebSiteDescription = cfg.WebSiteDescription,
                            WebSiteKeyWord = cfg.WebSiteKeyWord,
                            WebSiteName=cfg.WebSiteName,
                            WebSiteTitle=cfg.WebSiteTitle,
                            WebSiteUrl=cfg.WebSiteUrl
                        };
            return query.FirstOrDefault();
        }
        /// <summary>
        /// 获取网站顶部导航数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<Models.WebSiteNavigationModel> GetWebSiteNavigations()
        {
            EFDbContext dbContext = new EFDbContext();
            var query = from nav in dbContext.m_WebSiteNavigation
                        orderby nav.SortCount ascending
                        select new Models.WebSiteNavigationModel() {
                            AppendTime=nav.AppendTime.Value,
                            IsShow=nav.IsShow.Value,
                            IsTarget=nav.IsTarget.Value,
                            LinkUrl=nav.LinkUrl,
                            NavigationId=nav.NavigationId.Value,
                            NavigationName=nav.NavigationName,
                            SortCount=nav.SortCount.Value
                        };
            return query;
        }
    }
}
