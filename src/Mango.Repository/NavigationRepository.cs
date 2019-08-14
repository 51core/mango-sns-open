using System;
using System.Collections.Generic;
using System.Text;
using Mango.Framework.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
namespace Mango.Repository
{
    public class NavigationRepository
    {
        private EFDbContext _dbContext = null;
        public NavigationRepository()
        {
            _dbContext = new EFDbContext();
        }
        /// <summary>
        /// 更新导航点击次数
        /// </summary>
        /// <param name="navigationId"></param>
        /// <returns></returns>
        public bool UpdateClickCount(int navigationId)
        {
            return _dbContext.MangoUpdate<Entity.m_Navigation>(m => m.ClickCount == m.ClickCount + 1, m => m.NavigationId == navigationId);
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
            return _dbContext.m_Navigation
                .Join(_dbContext.m_NavigationClassify, nav => nav.CId, nc => nc.CId, (nav, nc) => new
                {
                    nav.CId,nav.ClickCount,nav.IsShow,nav.NavigationId,nav.NavigationName,nav.NavigationUrl,nav.Remark,nc.ClassifyName
                });
        }
        /// <summary>
        /// 获取导航数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<Models.NavigationModel> GetNavigationQuery()
        {
            return _dbContext.m_Navigation.Select(m => new Models.NavigationModel()
            {
                CId = m.CId.Value,
                ClickCount = m.ClickCount.Value,
                IsShow = m.IsShow.Value,
                NavigationId = m.NavigationId.Value,
                NavigationName = m.NavigationName,
                NavigationUrl = m.NavigationUrl,
                Remark = m.Remark
            });
        }
        /// <summary>
        /// 获取导航数据
        /// </summary>
        /// <returns></returns>
        public List<Models.NavigationModel> GetNavigationList()
        {
            return _dbContext.m_Navigation.Select(m=>new Models.NavigationModel()
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
            return _dbContext.m_NavigationClassify.OrderBy(m=>m.SortCount).Select(m=>new Models.NavigationClassifyModel()
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
            return _dbContext.m_NavigationClassify.ToMangoDataList();
        }
    }
}
