using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using Mango.Framework.EFCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Mango.Repository
{
    public class CommonRepository
    {
        private EFDbContext _dbContext = null;
        public CommonRepository()
        {
            _dbContext = new EFDbContext();
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add<TEntity>(TEntity entity) where TEntity:class
        {
            _dbContext.Add(entity);
            return _dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 根据Id获取指定记录
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TEntity Find<TEntity>(int Id) where TEntity : class
        {
            return _dbContext.Find<TEntity>(Id);
        }
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="IsFind"></param>
        /// <returns></returns>
        public bool Update<TEntity>(TEntity entity, bool isFind) where TEntity : class
        {
            _dbContext.Update(entity);

            return _dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 更新记录(修改指定的列)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public bool Update<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Entry(entity).State = EntityState.Unchanged;
            //
            Type type= entity.GetType();
            //处理实体类属性
            PropertyInfo[] properties = type.GetProperties();
            foreach (var property in properties)
            {
                object value = property.GetValue(entity, null);
                var key = property.GetCustomAttribute<KeyAttribute>();
                if (value != null&& key==null)
                {
                    _dbContext.Entry(entity).Property(property.Name).IsModified = true;
                }
            }
            return _dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Remove(entity);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
