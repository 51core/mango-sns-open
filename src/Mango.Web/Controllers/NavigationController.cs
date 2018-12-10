using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mango.Repository;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860

namespace Mango.Web.Controllers
{
    public class NavigationController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            NavigationRepository repository = new NavigationRepository();
            ViewModels.NavigationViewModel model = new ViewModels.NavigationViewModel();
            model.ClassifyListData= repository.GetClassifyList();
            return View(model);
        }
        /// <summary>
        /// 加载导航数据
        /// </summary>
        /// <returns></returns>
        public string LoadList()
        {
            NavigationRepository repository = new NavigationRepository();
            return Newtonsoft.Json.JsonConvert.SerializeObject(repository.GetNavigationList());
        }
        public bool UpdateClickCount(int navigationId)
        {
            NavigationRepository repository = new NavigationRepository();
            return repository.UpdateClickCount(navigationId);
        }
    }
}
