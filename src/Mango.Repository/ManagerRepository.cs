using System;
using System.Collections.Generic;
using System.Text;
using Mango.Framework.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
namespace Mango.Repository
{
    public class ManagerRepository
    {
        private EFDbContext _dbContext = null;
        public ManagerRepository()
        {
            _dbContext = new EFDbContext();
        }
        // GET: /<controller>/
        /// <summary>
        /// 设置帖子属性标签
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SetProperty(Entity.m_Posts model)
        {
            _dbContext.Add(model);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
