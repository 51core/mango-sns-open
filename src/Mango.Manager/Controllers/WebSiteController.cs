using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mango.Repository;
using Mango.Models;
namespace Mango.Manager.Controllers
{
    public class WebSiteController : Controller
    {
        /// <summary>
        /// 删除网站导航
        /// </summary>
        /// <param name="navigationId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DeleteNavigation(int navigationId)
        {
            Entity.m_WebSiteNavigation model = new Entity.m_WebSiteNavigation();
            model.NavigationId = navigationId;
            CommonRepository repository = new CommonRepository();
            return repository.Delete(model);
        }
        /// <summary>
        /// 编辑网站导航
        /// </summary>
        /// <param name="navigationId"></param>
        /// <param name="navigationName"></param>
        /// <param name="linkUrl"></param>
        /// <param name="sortCount"></param>
        /// <param name="isTarget"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        [HttpPost]
        public bool EditNavigation(int navigationId,string navigationName, string linkUrl, int sortCount, bool isTarget, bool isShow)
        {
            Entity.m_WebSiteNavigation model = new Entity.m_WebSiteNavigation();
            model.NavigationId = navigationId;
            model.IsShow = isShow;
            model.IsTarget = isTarget;
            model.LinkUrl = linkUrl;
            model.NavigationName = navigationName;
            model.SortCount = sortCount;
            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
        /// <summary>
        /// 添加网站导航
        /// </summary>
        /// <param name="navigationName"></param>
        /// <param name="linkUrl"></param>
        /// <param name="sortCount"></param>
        /// <param name="isTarget"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        [HttpPost]
        public bool AddNavigation(string navigationName,string linkUrl,int sortCount,bool isTarget,bool isShow)
        {
            Entity.m_WebSiteNavigation model = new Entity.m_WebSiteNavigation();
            model.AppendTime = DateTime.Now;
            model.IsShow = isShow;
            model.IsTarget = isTarget;
            model.LinkUrl = linkUrl;
            model.NavigationName = navigationName;
            model.SortCount = sortCount;
            CommonRepository repository = new CommonRepository();
            return repository.Add(model);
        }
        public IActionResult Navigation()
        {
            WebSiteRepository repository = new WebSiteRepository();
            List<WebSiteNavigationModel> model = repository.GetWebSiteNavigations().ToList();
            return View(model);
        }
        /// <summary>
        /// 系统基础数据设置
        /// </summary>
        /// <returns></returns>
        public IActionResult Config()
        {
            WebSiteRepository repository = new WebSiteRepository();
            WebSiteConfigModel model = repository.GetWebSiteConfig();
            return View(model);
        }
        [HttpPost]
        public bool Config(Entity.m_WebSiteConfig model)
        {
            model.IsLogin = model.IsLogin == null ? false : true;
            model.IsRegister = model.IsRegister == null ? false : true;
            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
    }
}