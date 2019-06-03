using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mango.Repository;
using Mango.Models;

namespace Mango.Manager.Controllers
{
    public class DocsController : Controller
    {
        /// <summary>
        /// 文档列表管理
        /// </summary>
        /// <returns></returns>
        public IActionResult List(int id)
        {
            ViewModels.DocsListViewModel model = new ViewModels.DocsListViewModel();
            //查询帖子数据
            int pageIndex = Framework.Core.Transform.GetInt(Request.Query["p"], 1);
            int pageSize = 10;
            DocsRepository repository = new DocsRepository();

            var query = repository.GetDocsByPage();
            model.ListData = query.Where(q=>q.ThemeId==id).OrderByDescending(q => q.DocsId).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            model.TotalCount = query.Where(q => q.ThemeId == id).Count();
            return View(model);
        }
        /// <summary>
        /// 设置文档是否显示
        /// </summary>
        /// <param name="docsId"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        public bool SetDocsShow(int docsId, bool isShow)
        {
            Entity.m_Docs model = new Entity.m_Docs();
            model.DocsId = docsId;
            model.IsShow = isShow;
            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
        /// <summary>
        /// 文档主题管理
        /// </summary>
        /// <returns></returns>
        public IActionResult Theme()
        {
            ViewModels.DocsThemeViewModel model = new ViewModels.DocsThemeViewModel();
            //查询帖子数据
            int pageIndex = Framework.Core.Transform.GetInt(Request.Query["p"], 1);
            int pageSize = 10;
            DocsRepository repository = new DocsRepository();

            var query = repository.GetDocsThemeList();
            model.ListData = query.OrderByDescending(q=>q.ThemeId).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            model.TotalCount = query.Count();
            return View(model);
        }
        /// <summary>
        /// 设置文档主题是否显示
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="isShow"></param>
        /// <returns></returns>
        public bool SetThemeShow(int themeId, bool isShow)
        {
            Entity.m_DocsTheme model = new Entity.m_DocsTheme();
            model.ThemeId = themeId;
            model.IsShow = isShow;
            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
    }
}