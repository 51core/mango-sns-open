using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mango.Repository;
using Microsoft.AspNetCore.Http;
using Mango.Framework.EFCore;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860

namespace Mango.Web.Areas.User.Controllers
{
    [Area("User")]
    public class MyMessageController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
           
            int pageIndex = Framework.Core.Transform.GetInt(Request.Query["p"], 1);
            int pageSize = 10;
            int userId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
            ViewModels.MessageViewModel model = new ViewModels.MessageViewModel();
            //分页显示分享数据
            MessageRepository repository = new MessageRepository();
            var query = repository.GetMessageList();

            model.ListData = query.Where(m => m.UserId == userId).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            model.TotalCount = query.Where(m => m.UserId == userId).Count();
            return View(model);
        }
    }
}
