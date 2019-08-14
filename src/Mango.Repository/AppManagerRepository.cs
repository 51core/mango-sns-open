using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mango.Framework.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Mango.Repository
{
    public class AppManagerRepository
    {
        private EFDbContext _dbContext = null;
        public AppManagerRepository()
        {
            _dbContext = new EFDbContext();
        }
        /// <summary>
        /// 获取应用管理信息
        /// </summary>
        /// <returns></returns>
        public List<Models.AppManagerModel> GetAppManagers()
        {
            return _dbContext.m_AppManager
                .OrderByDescending(q => q.AppId)
                .Select(q => new Models.AppManagerModel()
                {
                    AppendTime = q.AppendTime.Value,
                    AppId = q.AppId.Value,
                    AppKey = q.AppKey,
                    AppName = q.AppName,
                    AppSecret = q.AppSecret,
                    IsOpen = q.IsOpen.Value,
                    LastTime = q.LastTime.Value,
                    OpenIP = q.OpenIP,
                    OpenUrl = q.OpenUrl,
                    RemarkText = q.RemarkText
                })
                .ToList();
        }
    }
}
