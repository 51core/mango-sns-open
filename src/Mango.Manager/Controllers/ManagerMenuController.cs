using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mango.Repository;
using Mango.Framework.EFCore;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860

namespace Mango.Manager.Controllers
{
    public class ManagerMenuController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ManagerAccountRepository repository = new ManagerAccountRepository();
            ViewModels.ManagerPageViewModel model = new ViewModels.ManagerPageViewModel();

            List<MangoData> sourceDt = repository.GetMenuList();
            List<MangoData> newDt = new List<MangoData>();
            newDt.Clear();
            One(sourceDt, newDt);
            model.ListData = newDt;
            return View(model);
        }
        private void One(List<MangoData> sourceDt, List<MangoData> newDt)
        {
            foreach (var row in sourceDt)
            {
                if (row["ParentId"].ToString() == "0")
                {
                    MangoData dr = new MangoData();
                    dr["MenuId"] = row["MenuId"];
                    dr["MenuName"] = row["MenuName"];
                    dr["AreaName"] = row["AreaName"];
                    dr["ControllerName"] = row["ControllerName"];
                    dr["ActionName"] = row["ActionName"];
                    dr["ParentId"] = row["ParentId"];
                    dr["IsPower"] = row["IsPower"];
                    newDt.Add(dr);
                    Two(sourceDt, newDt, row["MenuId"].ToString());
                }
            }
        }
        private void Two(List<MangoData> sourceDt, List<MangoData> newDt, string ParentId)
        {
            foreach (var row in sourceDt)
            {
                if (row["ParentId"].ToString() == ParentId)
                {
                    MangoData dr = new MangoData();
                    dr["MenuId"] = row["MenuId"];
                    dr["MenuName"] = row["MenuName"];
                    dr["AreaName"] = row["AreaName"];
                    dr["ControllerName"] = row["ControllerName"];
                    dr["ActionName"] = row["ActionName"];
                    dr["ParentId"] = row["ParentId"];
                    dr["IsPower"] = row["IsPower"];
                    newDt.Add(dr);
                }
            }
        }
        public IActionResult GetMenuByParent()
        {
            ManagerAccountRepository repository = new ManagerAccountRepository();
            List<MangoData> result = repository.GetMenuListByParent();
            return Json(result);
        }
        
        public bool AddMenu(Entity.m_ManagerMenu model)
        {
            model.IsPower = model.ParentId > 0 ? true : false;
            CommonRepository repository = new CommonRepository();
            return repository.Add(model);
        }
        public bool EditMenu(Entity.m_ManagerMenu model)
        {
            model.IsPower = model.ParentId > 0 ? true : false;
            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
    }
}
