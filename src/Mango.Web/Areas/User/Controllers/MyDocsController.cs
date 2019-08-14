using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Mango.Repository;
namespace Mango.Web.Areas.User.Controllers
{
    [Area("User")]
    public class MyDocsController : Controller
    {
        public IActionResult Index()
        {
            ViewModels.UserDocsThemeViewModel model = new ViewModels.UserDocsThemeViewModel();
            //查询帖子数据
            int pageIndex = Framework.Core.Transform.GetInt(Request.Query["p"], 1);
            int pageSize = 10;
            int userId= Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
            DocsRepository repository = new DocsRepository();
            var query = repository.GetDocsThemeList();
            model.ListData = query.Where(q=>q.UserId==userId).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            model.TotalCount = query.Where(q => q.UserId == userId).Count();
            return View(model);
        }
        /// <summary>
        /// 根据文档主题ID显示文档内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Docs(int id)
        {
            ViewModels.UserDocsViewModel model = new ViewModels.UserDocsViewModel();
            //查询帖子数据
            int pageIndex = Framework.Core.Transform.GetInt(Request.Query["p"], 1);
            int pageSize = 10;
            int userId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
            DocsRepository repository = new DocsRepository();
            var query = repository.GetDocsList();
            model.ListData = query.Where(q => q.UserId == userId&&q.ThemeId==id).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            model.TotalCount = query.Where(q => q.UserId == userId && q.ThemeId == id).Count();
            return View(model);
        }
        /// <summary>
        /// 发布文档主题
        /// </summary>
        /// <returns></returns>
        public IActionResult AddTheme()
        {
            return View();
        }
        [HttpPost]
        public string AddTheme(ViewModels.AddDocsThemeRequestViewModel requestViewModel)
        {
            if (requestViewModel.Title.Trim().Length <= 0)
            {
                return "请输入文档主题标题";
            }
            if (requestViewModel.Contents.Trim().Length <= 0)
            {
                return "请输入文档主题内容";
            }
            Entity.m_DocsTheme model = new Entity.m_DocsTheme();
            model.AppendTime = DateTime.Now;
            model.Contents = requestViewModel.Contents;
            model.IsShow = true;
            model.LastTime = DateTime.Now;
            model.PlusCount = 0;
            model.ReadCount = 0;
            model.Tags = "";
            model.Title = Framework.Core.HtmlFilter.StripHtml(requestViewModel.Title);
            model.UserId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
            model.VersionText = "";
            CommonRepository repository = new CommonRepository();
            return repository.Add(model) ? "ok" : "数据保存失败,请稍后再尝试提交.";
        }
        /// <summary>
        /// 编辑文档主题
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditTheme(int id)
        {
            ViewModels.DocsThemeEditViewModel model = new ViewModels.DocsThemeEditViewModel();
            int userId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
            //加载帖子数据
            DocsRepository repository = new DocsRepository();
            model.DocsThemeData = repository.GetDocsThemeById(id, userId);
            if (model.DocsThemeData != null)
            {
                return View(model);
            }
            return new ContentResult()
            {
                Content = "您的请求未得到授权!",
                StatusCode = 401
            };
        }
        [HttpPost]
        public string EditTheme(ViewModels.EditDocsThemeRequestModel requestModel)
        {
            Entity.m_DocsTheme model = new Entity.m_DocsTheme();
            model.ThemeId = requestModel.ThemeId;
            model.Contents = requestModel.Contents;
            model.LastTime = DateTime.Now;
            model.Title = Framework.Core.HtmlFilter.StripHtml(requestModel.Title);
            CommonRepository repository = new CommonRepository();
            return repository.Update(model)?"ok":"数据保存失败,请稍后再尝试提交";
        }
        /// <summary>
        /// 发布文档
        /// </summary>
        /// <returns></returns>
        public IActionResult AddDocs(int id)
        {
            ViewData["ThemeId"] = id;
            return View();
        }
        [HttpPost]
        public string AddDocs(ViewModels.AddDocsRequestViewModel requestViewModel)
        {
            if (requestViewModel.Title.Trim().Length <= 0)
            {
                return "请输入文档标题";
            }
            if (requestViewModel.Contents.Trim().Length <= 0)
            {
                return "请输入文档内容";
            }
            Entity.m_Docs model = new Entity.m_Docs();
            model.AppendTime = DateTime.Now;
            model.Contents = requestViewModel.Contents;
            model.IsShow = true;
            model.LastTime = DateTime.Now;
            model.PlusCount = 0;
            model.ReadCount = 0;
            model.Tags = "";
            model.Title = Framework.Core.HtmlFilter.StripHtml(requestViewModel.Title);
            model.UserId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
            model.VersionText = "";
            model.ThemeId = requestViewModel.ThemeId;
            model.ShortTitle = Framework.Core.HtmlFilter.StripHtml(requestViewModel.ShortTitle);
            model.IsAudit = true;
            CommonRepository repository = new CommonRepository();
            return repository.Add(model) ? "ok" : "数据保存失败,请稍后再尝试提交.";
        }
        /// <summary>
        /// 编辑文档
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditDocs(int id)
        {
            ViewModels.DocsEditViewModel model = new ViewModels.DocsEditViewModel();
            ViewData["DocsId"] = id;
            int userId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
            //加载帖子数据
            DocsRepository repository = new DocsRepository();
            model.DocsData = repository.GetDocsById(id, userId);
            if (model.DocsData != null)
            {
                return View(model);
            }
            return new ContentResult()
            {
                Content = "您的请求未得到授权!",
                StatusCode = 401
            };
        }
        [HttpPost]
        public string EditDocs(ViewModels.EditDocsRequestModel requestModel)
        {
            Entity.m_Docs model = new Entity.m_Docs();
            model.DocsId = requestModel.DocsId;
            model.Contents = requestModel.Contents;
            model.LastTime = DateTime.Now;
            model.Title = Framework.Core.HtmlFilter.StripHtml(requestModel.Title);
            model.ShortTitle = Framework.Core.HtmlFilter.StripHtml(requestModel.ShortTitle);
            CommonRepository repository = new CommonRepository();
            return repository.Update(model) ? "ok" : "数据保存失败,请稍后再尝试提交";
        }
    }
}