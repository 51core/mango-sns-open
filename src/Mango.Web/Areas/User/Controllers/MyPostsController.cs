using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mango.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860

namespace Mango.Web.Areas.User.Controllers
{
    [Area("User")]
    public class MyPostsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewModels.UserPostsViewModel model = new ViewModels.UserPostsViewModel();
            //查询帖子数据
            if (HttpContext.Session.GetString("UserId") != null)
            {
                int pageIndex = Framework.Core.Transform.GetInt(Request.Query["p"], 1);
                int pageSize = 10;
                int userId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
                PostsRepository repository = new PostsRepository();
                var query= repository.GetPostsPageList();
                model.ListData = query.Where(m => m.UserId == userId).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList(); 
                model.TotalCount = query.Count();
            }
            return View(model);
        }
    }
}
