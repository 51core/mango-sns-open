using System;
using System.Collections.Generic;
using System.Text;
using Mango.Framework.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
namespace Mango.Repository
{
    public class NavigationRepository
    {
        /// <summary>
        /// 更新导航点击次数
        /// </summary>
        /// <param name="navigationId"></param>
        /// <returns></returns>
        public bool UpdateClickCount(int navigationId)
        {
            EFDbContext dbContext = new EFDbContext();
            return dbContext.MangoUpdate<Entity.m_Navigation>(m => m.ClickCount == m.ClickCount + 1, m => m.NavigationId == navigationId);
        }
        /// <summary>
        /// 分页查询导航数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public IQueryable<object> GetNavigationPageList()
        {
            EFDbContext dbContext = new EFDbContext();
            var query = from nav in dbContext.m_Navigation
                        join nc in dbContext.m_NavigationClassify
                        on nav.CId equals nc.CId

                        select new
                        {
                            nav.CId,
                            nav.ClickCount,
                            nav.IsShow,
                            nav.NavigationId,
                            nav.NavigationName,
                            nav.NavigationUrl,
                            nav.Remark,
                            nc.ClassifyName
                        }; 
            return query;
        }
        /// <summary>
        /// 获取导航数据
        /// </summary>
        /// <returns></returns>
        public List<Models.NavigationModel> GetNavigationList()
        {
            EFDbContext dbContext = new EFDbContext();
            return dbContext.m_Navigation.Select(m=>new Models.NavigationModel()
            {
                CId=m.CId.Value,
                ClickCount=m.ClickCount.Value,
                IsShow=m.IsShow.Value,
                NavigationId=m.NavigationId.Value,
                NavigationName=m.NavigationName,
                NavigationUrl=m.NavigationUrl,
                Remark=m.Remark
            }).ToList();
        }
        /// <summary>
        /// 获取导航分类
        /// </summary>
        /// <returns></returns>
        public List<Models.NavigationClassifyModel> GetClassifyList()
        {
            EFDbContext dbContext = new EFDbContext();
            return dbContext.m_NavigationClassify.OrderBy(m=>m.SortCount).Select(m=>new Models.NavigationClassifyModel()
            {
                CId=m.CId.Value,
                ClassifyName=m.ClassifyName,
                IsShow=m.IsShow.Value,
                SortCount=m.SortCount.Value
            }).ToList();
        }
        /// <summary>
        /// 获取导航分类
        /// </summary>
        /// <returns></returns>
        public List<MangoData> GetClassifyListByManager()
        {
            EFDbContext dbContext = new EFDbContext();
            return dbContext.m_NavigationClassify.ToMangoDataList();
        }
    }
}
