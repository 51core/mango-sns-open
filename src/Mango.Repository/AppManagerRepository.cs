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
            var query = from app in _dbContext.m_AppManager
                        select new Models.AppManagerModel()
                        {
                            AppendTime=app.AppendTime.Value,
                            AppId=app.AppId.Value,
                            AppKey=app.AppKey,
                            AppName=app.AppName,
                            AppSecret=app.AppSecret,
                            IsOpen=app.IsOpen.Value,
                            LastTime=app.LastTime.Value,
                            OpenIP=app.OpenIP,
                            OpenUrl=app.OpenUrl,
                            RemarkText=app.RemarkText
                        };
            return query.OrderByDescending(m=>m.AppId).ToList();
        }
    }
}
