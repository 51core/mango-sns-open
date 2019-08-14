using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mango.Framework.Services;
using Mango.Framework.Services.Cache;
using Mango.Repository;
using Mango.Framework.EFCore;
using System.Linq;
namespace Mango.Manager.Controllers
{
    public class ManagerSystemController : Controller
    {
        public IActionResult Cache()
        {
            return View();
        }
        [HttpPost]
        public bool CacheRemove(int CacheType)
        {
            try
            {
                EFDbContext dbContext = new EFDbContext();
                List<MangoData> datas = dbContext.m_UserGroup.ToMangoDataList();
                //进行Redis缓存清理
                ICacheService CacheService = ServiceContext.GetService<ICacheService>();
                switch (CacheType)
                {
                    case 0:
                        CacheService.Remove("PostsChannelCache");
                        CacheService.Remove("WebSiteNavigationCache");
                        CacheService.Remove("WebSiteConfigCache");
                        foreach (var row in datas)
                        {
                            CacheService.Remove(string.Format("UserGroupPowerCache{0}", row["GroupId"]));
                        }
                        break;
                    case 1:
                        foreach (var row in datas)
                        {
                            CacheService.Remove(string.Format("UserGroupPowerCache{0}", row["GroupId"]));
                        }
                        break;
                    case 2:
                        CacheService.Remove("PostsChannelCache");
                        break;
                    case 3:
                        CacheService.Remove("WebSiteNavigationCache");
                        break;
                    case 4:
                        CacheService.Remove("WebSiteConfigCache");
                        break;
                }
                return true;
            }
            catch
            {
               return false;
            }
        }
    }
}