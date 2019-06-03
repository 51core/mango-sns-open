using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Mango.Repository;
namespace Mango.Web.Controllers
{
    /// <summary>
    /// 文档功能模块
    /// </summary>
    public class DocsController : Controller
    {
        /// <summary>
        /// 文档首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewModels.DocsViewModel model = new ViewModels.DocsViewModel();
            //查询帖子数据
            int pageIndex = Framework.Core.Transform.GetInt(Request.Query["p"], 1);
            int pageSize = 10;
            DocsRepository repository = new DocsRepository();
            var query = repository.GetDocsThemeList();

            model.ListData = query.Where(q => q.IsShow == true).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            model.TotalCount = query.Where(q => q.IsShow == true).Count();

          
            return View(model);
        }
        /// <summary>
        /// 文档阅读浏览
        /// </summary>
        /// <returns></returns>
        [Route("docs/read/{id}")]
        [Route("docs/read/{id}/{docsid}")]
        public IActionResult Read(int id,int docsid = 0)
        {
            DocsRepository repository = new DocsRepository();
            ViewModels.DocsReadViewModel model = new ViewModels.DocsReadViewModel();
            model.DocsId = docsid;
            model.ThemeId = id;
            if (docsid == 0)
            {
                model.DocsThemeData = repository.GetDocsThemeById(id);
            }
            else
            {
                model.DocsData = repository.GetDocsById(docsid);
            }
            model.ItemsListData = repository.GetDocsListById(id);
            return View(model);
        }
        
    }
}