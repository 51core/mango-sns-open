using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mango.Repository;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860

namespace Mango.Manager.Controllers
{
    public class ManagerAccountController : Controller
    {
        // GET: /<controller>/
        // GET: Admin/Account
        public IActionResult Index()
        {
            ViewModels.ManagerPageViewModel model = new ViewModels.ManagerPageViewModel();
            ManagerAccountRepository repository = new ManagerAccountRepository();

            model.TotalCount = 0;
            model.ListData = repository.GetAccountList();
            return View(model);
        }
        [HttpPost]
        public bool Edit(int adminId, int roleId, string adminName, string password)
        {
            bool Result = false;
            if (!string.IsNullOrEmpty(adminName))
            {
                Entity.m_ManagerAccount model = new Entity.m_ManagerAccount();
                model.AdminId = adminId;
                model.AdminName = adminName;
                if (!string.IsNullOrEmpty(password))
                {
                    model.Password = Framework.Core.TextHelper.MD5Encrypt(password);
                }
                model.RoleId = roleId;
                CommonRepository repository = new CommonRepository();
                return repository.Update(model);
                
            }
            return Result;
        }
        [HttpPost]
        public bool Delete(int adminId)
        {
            CommonRepository repository = new CommonRepository();
            Entity.m_ManagerAccount model = new Entity.m_ManagerAccount();
            model.AdminId = adminId;
            return repository.Delete(model);
        }
        /// <summary>
        /// 添加账号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool Add(int roleId, string adminName, string password)
        {
            bool Result = false;
            if (!string.IsNullOrEmpty(adminName) && !string.IsNullOrEmpty(password))
            {
                Entity.m_ManagerAccount model = new Entity.m_ManagerAccount();
                model.AdminName = adminName;
                model.Password = Framework.Core.TextHelper.MD5Encrypt(password);
                model.RoleId = roleId;
                model.IsStatus = true;
                CommonRepository repository = new CommonRepository();
                return repository.Add(model);
            }
            return Result;
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public string GetRoleList()
        {
            ManagerAccountRepository repository = new ManagerAccountRepository();
            return Newtonsoft.Json.JsonConvert.SerializeObject(repository.GetRoleInfo());
        }
    }
}
