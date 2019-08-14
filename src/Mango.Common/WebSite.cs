using System;
using System.Collections.Generic;
using System.Text;
using Mango.Repository;
using Mango.Framework.Services;
using Mango.Framework.Services.Cache;
using Newtonsoft.Json;
using System.Linq;
namespace Mango.Common
{
    public class WebSite
    {
        /// <summary>
        /// 获取网站基本配置信息
        /// </summary>
        /// <returns></returns>
        public Models.WebSiteConfigModel GetWebSiteConfigByCache()
        {
            Models.WebSiteConfigModel Result = null;
            ICacheService cacheService = ServiceContext.GetService<ICacheService>();
            string cacheData = cacheService.Get("WebSiteConfigCache");
            if (string.IsNullOrEmpty(cacheData))
            {
                WebSiteRepository repository = new WebSiteRepository();
                Result = repository.GetWebSiteConfig();
                cacheData = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Result)));
                //写入缓存
                cacheService.Add("WebSiteConfigCache", cacheData);
            }
            else
            {
                cacheData = Encoding.UTF8.GetString(Convert.FromBase64String(cacheData.Replace("\"", "")));
                //从缓存中获取
                Result = JsonConvert.DeserializeObject<Models.WebSiteConfigModel>(cacheData);
            }
            return Result;
        }
        /// <summary>
        /// 从缓存服务中获取网站顶部导航数据
        /// </summary>
        /// <returns></returns>
        public List<Models.WebSiteNavigationModel> GetWebSiteNavigationByCache()
        {
            List<Models.WebSiteNavigationModel> Result = null;
            ICacheService cacheService = ServiceContext.GetService<ICacheService>();
            string cacheData = cacheService.Get("WebSiteNavigationCache");
            if (string.IsNullOrEmpty(cacheData))
            {
                WebSiteRepository repository = new WebSiteRepository();
                Result = repository.GetWebSiteNavigations().Where(m => m.IsShow == true).ToList();
                cacheData = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Result)));
                //写入缓存
                cacheService.Add("WebSiteNavigationCache", cacheData);
            }
            else
            {
                cacheData = Encoding.UTF8.GetString(Convert.FromBase64String(cacheData.Replace("\"", "")));
                //从缓存中获取
                Result = JsonConvert.DeserializeObject<List<Models.WebSiteNavigationModel>>(cacheData);
            }
            return Result;
        }
    }
}
