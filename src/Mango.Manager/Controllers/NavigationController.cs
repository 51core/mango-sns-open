using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mango.Repository;
using Newtonsoft.Json;
using Mango.Framework.EFCore;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860

namespace Mango.Manager.Controllers
{
    public class NavigationController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            int pageIndex = 1;
            if (Request.Query["p"].Count > 0)
            {
                pageIndex = Framework.Core.Transform.GetInt(Request.Query["p"], 1);
            }
            int pageSize = 10;
            string Where = string.Empty;
            NavigationRepository repository = new NavigationRepository();
            var query = repository.GetNavigationPageList();

            ViewModels.ManagerPageViewModel model = new ViewModels.ManagerPageViewModel();
            model.ListData = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToMangoDataList();
            model.TotalCount = query.Count();
            return View(model);
        }
        /// <summary>
        /// 加载导航分类
        /// </summary>
        /// <returns></returns>
        public string LoadNavigationClassify()
        {
            NavigationRepository repository = new NavigationRepository();
            List<Mango.Models.NavigationClassifyModel> model = repository.GetClassifyList();
            return JsonConvert.SerializeObject(model);
        }
        public bool AddNavigation(Entity.m_Navigation model)
        {
            CommonRepository repository = new CommonRepository();
            model.ClickCount = 0;
            return repository.Add(model);
        }
        public bool EditNavigation(Entity.m_Navigation model)
        {
            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
        public bool DeleteNavigation(int navigationId)
        {
            Entity.m_Navigation model = new Entity.m_Navigation();
            model.NavigationId = navigationId;
            CommonRepository repository = new CommonRepository();
            return repository.Delete(model);
        }
        #region 导航分类管理
        public IActionResult Classify()
        {
            NavigationRepository repository = new NavigationRepository();
            ViewModels.ManagerPageViewModel model = new ViewModels.ManagerPageViewModel();
            model.ListData = repository.GetClassifyListByManager();
            return View(model);
        }
        public bool AddClassify(Entity.m_NavigationClassify model)
        {
            CommonRepository repository = new CommonRepository();
            return repository.Add(model);
        }
        public bool EditClassify(Entity.m_NavigationClassify model)
        {
            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
        public bool DeleteClassify(int classifyId)
        {
            Entity.m_NavigationClassify model = new Entity.m_NavigationClassify();
            model.CId = classifyId;
            CommonRepository repository = new CommonRepository();
            return repository.Delete(model);
        }
        #endregion
    }
}
