using System;
using System.Collections.Generic;
using System.Text;
using Mango.Framework.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
namespace Mango.Repository
{
    public class ManagerRepository
    {
        // GET: /<controller>/
        /// <summary>
        /// 设置帖子属性标签
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SetProperty(Entity.m_Posts model)
        {
            EFDbContext dbContext = new EFDbContext();
            dbContext.Add(model);
            return dbContext.SaveChanges() > 0;
        }
    }
}
