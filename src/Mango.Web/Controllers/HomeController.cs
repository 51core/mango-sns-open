using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mango.Repository;

namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Message()
        {
            return View();
        }
        /// <summary>
        /// 系统异常显示页面
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //记录到日志中
            _logger.LogError(Activity.Current?.Id ?? HttpContext.TraceIdentifier);
            return View();
        }
        /// <summary>
        /// 测试页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Test()
        {
            return View();
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewModels.HomeViewModel viewModel = new ViewModels.HomeViewModel();
            int pageSize = 5;
            //加载帖子数据
            PostsRepository repository = new PostsRepository();
            var query = repository.GetPostsPageList();
            viewModel.PostsDatas = query.Where(m => m.IsShow == true).Take(pageSize).ToList();
            //加载文档主题数据
            DocsRepository docsRepository = new DocsRepository();
            viewModel.DocsDatas = docsRepository.GetDocsByPage().OrderByDescending(m=>m.DocsId).Where(m => m.IsShow == true).Take(pageSize).ToList();
            return View(viewModel);
        }
    }
}