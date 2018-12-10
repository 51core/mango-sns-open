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
    public class ManagerRoleController : Controller
    {
        // GET: Admin/Role
        public IActionResult Index()
        {
            ManagerAccountRepository repository = new ManagerAccountRepository();
            ViewModels.ManagerPageViewModel model = new ViewModels.ManagerPageViewModel();
            model.TotalCount = 0;
            model.ListData = repository.GetRoleList();
            return View(model);
        }
        [HttpPost]
        public bool Edit(int roleId, string roleName)
        {
            Entity.m_ManagerRole model = new Entity.m_ManagerRole();
            model.RoleId = roleId;
            model.RoleName = roleName;
            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
        [HttpPost]
        public bool Delete(int roleId)
        {
            Entity.m_ManagerRole model = new Entity.m_ManagerRole();
            model.RoleId = roleId;
            CommonRepository repository = new CommonRepository();
            return repository.Delete(model);
        }
        [HttpPost]
        public bool Add(string roleName)
        {
            bool Result = false;
            if (!string.IsNullOrEmpty(roleName))
            {
                Entity.m_ManagerRole model = new Entity.m_ManagerRole();
                model.RoleName = roleName;

                CommonRepository repository = new CommonRepository();
                return repository.Add(model);

            }
            return Result;
        }
        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="powerData"></param>
        /// <returns></returns>
        [HttpPost]

        public bool SetRolePower(int roleId, List<int> powerData)
        {
            ManagerAccountRepository repository = new ManagerAccountRepository();
            return repository.UpdateRolePower(roleId, powerData);
        }
        public IActionResult GetMenuByAll()
        {
            ManagerAccountRepository repository = new ManagerAccountRepository();
            List<MangoData> datas = repository.GetMenuList();
            return Json(datas);
        }
        public IActionResult GetRolePower(int roleId)
        {
            ManagerAccountRepository repository = new ManagerAccountRepository();
            return Json(repository.GetCompetence(roleId));
        }
    }
}
