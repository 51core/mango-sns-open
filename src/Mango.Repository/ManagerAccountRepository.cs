using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mango.Framework.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
namespace Mango.Repository
{
    public class ManagerAccountRepository
    {
        private EFDbContext _dbContext = null;
        public ManagerAccountRepository()
        {
            _dbContext = new EFDbContext();
        }
        /// <summary>
        /// 更新角色权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="powerData"></param>
        /// <returns></returns>
        public bool UpdateRolePower(int roleId, List<int> powerData)
        {
            using (var tran = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    //移除以前的权限
                    _dbContext.MangoRemove<Entity.m_ManagerPower>(m => m.RoleId == roleId);
                    //添加新权限
                    foreach (int Id in powerData)
                    {
                        Entity.m_ManagerPower model = new Entity.m_ManagerPower();
                        model.MenuId = Id;
                        model.RoleId = roleId;
                        _dbContext.Add(model);
                    }
                    _dbContext.SaveChanges();
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
        /// <summary>
        /// 获取管理菜单列表
        /// </summary>
        /// <returns></returns>
        public List<MangoData> GetMenuList()
        {
            return _dbContext.m_ManagerMenu.ToMangoDataList();
        }
        public List<MangoData> GetMenuListByParent()
        {
            return _dbContext.m_ManagerMenu.Where(m=>m.ParentId==0).ToMangoDataList();
        }
        /// <summary>
        /// 获取管理角色列表
        /// </summary>
        /// <returns></returns>
        public List<MangoData> GetRoleList()
        {
            return _dbContext.m_ManagerRole.ToMangoDataList();
        }
        /// <summary>
        /// 获取管理角色列表
        /// </summary>
        /// <returns></returns>
        public List<Entity.m_ManagerRole> GetRoleInfo()
        {
            return _dbContext.m_ManagerRole.ToList();
        }
        /// <summary>
        /// 获取管理账号列表
        /// </summary>
        /// <returns></returns>
        public List<MangoData> GetAccountList()
        {
            return _dbContext.m_ManagerAccount
                .Join(_dbContext.m_ManagerRole, a=> a.RoleId, r=> r.RoleId,(a,r)=> new {
                a.AdminId,a.AdminName,a.IsStatus,a.Password,a.RoleId,r.RoleName
            })
            .ToMangoDataList();
        }
        /// <summary>
        /// 根据角色Id获取权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<MangoData> GetCompetence(int roleId)
        {
            return _dbContext.m_ManagerPower.Join(_dbContext.m_ManagerMenu, p => p.MenuId, m => m.MenuId, (p, m) => new {
                p.PowerId, p.RoleId, m.MenuId, m.MenuName, m.ControllerName, m.ActionName, m.AreaName, m.IsPower, m.ParentId
            })
            .Where(m => m.RoleId == roleId)
            .ToMangoDataList();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="adminName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public List<MangoData> Login(string adminName, string password)
        {
            return _dbContext.m_ManagerAccount
                .Join(_dbContext.m_ManagerRole, a => a.RoleId, r => r.RoleId, (a, r) => new
                {
                    a.AdminId,
                    a.AdminName,
                    a.IsStatus,
                    a.Password,
                    a.RoleId,
                    r.RoleName
                })
                .Where(m => m.AdminName == adminName && m.Password == password)
                .ToMangoDataList();
        }
    }
}
