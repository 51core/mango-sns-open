using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mango.Repository;
using Newtonsoft.Json;
using Mango.Framework.EFCore;
using System.Linq;
using Mango.Models;
namespace Mango.Manager.Controllers
{
    public class UserController : Controller
    {
        #region 用户菜单管理
        public IActionResult Menu()
        {
            ViewModels.ManagerPageViewModel model = new ViewModels.ManagerPageViewModel();
            EFDbContext dbContext = new EFDbContext();

            List<MangoData> sourceDt = dbContext.m_UserGroupMenu.ToMangoDataList();

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
                    dr["MId"] = row["MId"];
                    dr["MName"] = row["MName"];
                    dr["AreaName"] = row["AreaName"];
                    dr["ControllerName"] = row["ControllerName"];
                    dr["ActionName"] = row["ActionName"];
                    dr["ParentId"] = row["ParentId"];
                    dr["IsPower"] = row["IsPower"];
                    newDt.Add(dr);
                    Two(sourceDt, newDt, row["MId"].ToString());
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
                    dr["MId"] = row["MId"];
                    dr["MName"] = row["MName"];
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
            EFDbContext dbContext = new EFDbContext();
            List<MangoData> datas = dbContext.m_UserGroupMenu.Where(m => m.ParentId == 0).ToMangoDataList();
            return Json(datas);
        }
        public IActionResult GetMenuByAll()
        {
            EFDbContext dbContext = new EFDbContext();
            List<MangoData> datas = dbContext.m_UserGroupMenu.ToMangoDataList();
            return Json(datas);
        }
        public bool AddMenu(Entity.m_UserGroupMenu model)
        {
            model.AreaName = model.AreaName == null ? "" : model.AreaName;
            model.ControllerName= model.ControllerName == null ? "" : model.ControllerName;
            model.ActionName = model.ActionName == null ? "" : model.ActionName;
            model.IsPower = model.ControllerName == null || model.ControllerName == "" ? false : true;
            CommonRepository repository = new CommonRepository();
            return repository.Add(model);
        }
        public bool EditMenu(Entity.m_UserGroupMenu model)
        {
            model.AreaName = model.AreaName == null ? "" : model.AreaName;
            model.ControllerName = model.ControllerName == null ? "" : model.ControllerName;
            model.ActionName = model.ActionName == null ? "" : model.ActionName;

            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
        #endregion
        #region 用户组管理
        //
        public IActionResult Group()
        {
            int PageIndex = Framework.Core.Transform.GetInt(Request.Query["p"], 1);
            EFDbContext dbContext = new EFDbContext();

            ViewModels.ManagerPageViewModel model = new ViewModels.ManagerPageViewModel();
            model.ListData = dbContext.m_UserGroup.ToMangoDataList();
            model.TotalCount = dbContext.m_UserGroup.Count();
            return View(model);
        }
        public bool AddGroup(Entity.m_UserGroup model)
        {
            
            model.IsDefault = false;
            CommonRepository repository = new CommonRepository();
            return repository.Add(model);
        }
        public bool EditGroup(Entity.m_UserGroup model)
        {
            model.IsDefault = false;
            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
        public IActionResult GetGroupPower(int groupId)
        {
            AuthorizationRepository repository = new AuthorizationRepository();
            List<UserGroupPowerModel> result= repository.GetPowerData(groupId);
            return Json(result);
        }
        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="PowerData"></param>
        /// <returns></returns>
        [HttpPost]

        public bool SetGroupPower(int groupId, List<int> powerData)
        {
            UserRepository repository = new UserRepository();
            return repository.UpdateGroupPower(groupId, powerData);
        }
        /// <summary>
        /// 设置用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="stateCode"></param>
        /// <returns></returns>
        [HttpPost]
        public bool SetUserState(int userId, bool stateCode)
        {
            Entity.m_User model = new Entity.m_User();
            model.UserId = userId;
            model.IsStatus = stateCode;
            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
        #endregion
        // GET: /<controller>/
        public IActionResult Index()
        {
            int pageIndex = 1;
            if (Request.Query["p"].Count > 0)
            {
                pageIndex = Framework.Core.Transform.GetInt(Request.Query["p"], 1);
            }
            int pageSize = 10;
            UserRepository repository = new UserRepository();
            var query = repository.GetUserPageList();
            ViewModels.ManagerPageViewModel model = new ViewModels.ManagerPageViewModel();
            model.ListData = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToMangoDataList();
            model.TotalCount = query.Count();
            return View(model);
        }
        
    }
}
