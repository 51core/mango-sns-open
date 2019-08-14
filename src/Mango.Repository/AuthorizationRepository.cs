using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mango.Framework.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
namespace Mango.Repository
{
    public class AuthorizationRepository
    {
        private EFDbContext _dbContext = null;
        public AuthorizationRepository()
        {
            _dbContext = new EFDbContext();
        }

        /// <summary>
        /// 根据用户组获取权限
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public List<Models.UserGroupPowerModel> GetPowerData(int groupId)
        {
            return _dbContext.m_UserGroupPower
                .Join(_dbContext.m_UserGroupMenu, p => p.MId, m => m.MId, (p, m) => new Models.UserGroupPowerModel()
                {
                    GroupId = p.GroupId.Value,
                    MId = m.MId.Value,
                    MName = m.MName,
                    AreaName = m.AreaName,
                    ControllerName = m.ControllerName,
                    ActionName = m.ActionName
                })
                .Where(q=>q.GroupId==groupId)
                .ToList();
        }
    }
}
