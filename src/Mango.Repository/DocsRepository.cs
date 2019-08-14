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
    public class DocsRepository
    {
        private EFDbContext _dbContext = null;
        public DocsRepository()
        {
            _dbContext = new EFDbContext();
        }
        /// <summary>
        /// 获取文档列表
        /// </summary>
        /// <returns></returns>
        public List<Models.DocsListModel> GetDocsListById(int themeId)
        {
            return _dbContext.m_Docs
                .Where(q => q.ThemeId == themeId && q.IsShow == true)
                .OrderByDescending(q => q.DocsId)
                .Select(q => new Models.DocsListModel()
                {
                    DocsId = q.DocsId.Value,
                    ShortTitle = q.ShortTitle,
                    Title = q.Title,
                    ThemeId = q.ThemeId.Value,
                    IsShow = q.IsShow.Value
                })
                .ToList();
        }
        /// <summary>
        /// 根据文档ID获取文档数据
        /// </summary>
        /// <param name="docsId"></param>
        /// <returns></returns>
        public Models.DocsModel GetDocsById(int docsId)
        {
            var queryResult = _dbContext.m_Docs
                .Join(_dbContext.m_User, doc => doc.UserId, u => u.UserId, (doc, u) => new Models.DocsModel()
                {
                    DocsId = doc.DocsId.Value,
                    HeadUrl = u.HeadUrl,
                    IsShow = doc.IsShow.Value,
                    LastTime = doc.LastTime.Value,
                    PlusCount = doc.PlusCount.Value,
                    NickName = u.NickName,
                    AppendTime = doc.AppendTime.Value,
                    ReadCount = doc.ReadCount.Value,
                    Title = doc.Title,
                    Tags = doc.Tags,
                    UserId = doc.UserId.Value,
                    ShortTitle = doc.ShortTitle,
                    ThemeId = doc.ThemeId.Value,
                    Contents = doc.Contents,
                    IsAudit = doc.IsAudit.Value
                })
                .Where(q => q.DocsId == docsId)
                .FirstOrDefault();
            if (queryResult != null)
            {
                //更新浏览次数
                _dbContext.MangoUpdate<Entity.m_Docs>(q => q.ReadCount == q.ReadCount + 1, q => q.DocsId == docsId);
            }
            return queryResult;
        }
        /// <summary>
        /// 获取文档数据信息
        /// </summary>
        /// <param name="docsId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Models.DocsModel GetDocsById(int docsId,int userId)
        {
            return _dbContext.m_Docs
                .Where(q => q.DocsId == docsId && q.UserId == userId)
                .Select(q => new Models.DocsModel()
                {
                    DocsId = q.DocsId.Value,
                    IsShow = q.IsShow.Value,
                    LastTime = q.LastTime.Value,
                    PlusCount = q.PlusCount.Value,
                    AppendTime = q.AppendTime.Value,
                    ReadCount = q.ReadCount.Value,
                    Title = q.Title,
                    Tags = q.Tags,
                    UserId = q.UserId.Value,
                    ShortTitle = q.ShortTitle,
                    ThemeId = q.ThemeId.Value,
                    Contents = q.Contents,
                    IsAudit = q.IsAudit.Value
                })
                .FirstOrDefault();
        }
        /// <summary>
        /// 获取文档列表集合
        /// </summary>
        /// <returns></returns>
        public  IQueryable<Models.DocsModel> GetDocsByPage()
        {
           return _dbContext.m_Docs
                .Join(_dbContext.m_User,doc=>doc.UserId,u=>u.UserId,(doc,u) => new Models.DocsModel()
                {
                    DocsId = doc.DocsId.Value,
                    IsShow = doc.IsShow.Value,
                    LastTime = doc.LastTime.Value,
                    PlusCount = doc.PlusCount.Value,
                    AppendTime = doc.AppendTime.Value,
                    ReadCount = doc.ReadCount.Value,
                    Title = doc.Title,
                    Tags = doc.Tags,
                    UserId = doc.UserId.Value,
                    ShortTitle = doc.ShortTitle,
                    ThemeId = doc.ThemeId.Value,
                    IsAudit = doc.IsAudit.Value,
                    NickName = u.NickName,
                    HeadUrl = u.HeadUrl
                });
        }
        /// <summary>
        /// 根据文档主题ID获取数据
        /// </summary>
        /// <param name="themeId">主题ID</param>
        /// <returns></returns>
        public Models.DocsThemeModel GetDocsThemeById(int themeId)
        {
            var queryResult = _dbContext.m_DocsTheme
                .Join(_dbContext.m_User, doc => doc.UserId, u => u.UserId, (doc, u) => new Models.DocsThemeModel()
                {
                    ThemeId = doc.ThemeId.Value,
                    HeadUrl = u.HeadUrl,
                    IsShow = doc.IsShow.Value,
                    LastTime = doc.LastTime.Value,
                    PlusCount = doc.PlusCount.Value,
                    NickName = u.NickName,
                    AppendTime = doc.AppendTime.Value,
                    ReadCount = doc.ReadCount.Value,
                    Title = doc.Title,
                    Tags = doc.Tags,
                    UserId = doc.UserId.Value,
                    Contents = doc.Contents
                })
               .Where(q => q.ThemeId == themeId)
               .OrderByDescending(q => q.ThemeId)
               .FirstOrDefault();
            if (queryResult != null)
            {
                //更新浏览次数
                _dbContext.MangoUpdate<Entity.m_DocsTheme>(q => q.ReadCount == q.ReadCount + 1, q => q.ThemeId == themeId);
            }
            return queryResult;
        }
        /// <summary>
        /// 获取用户指定文档主题信息
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Models.DocsThemeModel GetDocsThemeById(int themeId,int userId)
        {
            return _dbContext.m_DocsTheme
                 .Where(q => q.ThemeId == themeId && q.UserId == userId)
                 .Select(q => new Models.DocsThemeModel()
                 {
                     ThemeId = q.ThemeId.Value,
                     IsShow = q.IsShow.Value,
                     LastTime = q.LastTime.Value,
                     PlusCount = q.PlusCount.Value,
                     AppendTime = q.AppendTime.Value,
                     ReadCount = q.ReadCount.Value,
                     Title = q.Title,
                     Tags = q.Tags,
                     UserId = q.UserId.Value,
                     Contents = q.Contents
                 })
                 .FirstOrDefault();
        }
        /// <summary>
        /// 获取文档列表数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<Entity.m_Docs> GetDocsList()
        {
            return _dbContext.m_Docs.OrderByDescending(q=>q.DocsId);
        }
        /// <summary>
        /// 查询文档主题数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<Models.DocsThemeModel> GetDocsThemeList()
        {
            return _dbContext.m_DocsTheme
                .Join(_dbContext.m_User, doc => doc.UserId, u => u.UserId, (doc, u) => new Models.DocsThemeModel()
                {
                    ThemeId = doc.ThemeId.Value,
                    HeadUrl = u.HeadUrl,
                    IsShow = doc.IsShow.Value,
                    LastTime = doc.LastTime.Value,
                    PlusCount = doc.PlusCount.Value,
                    NickName = u.NickName,
                    AppendTime = doc.AppendTime.Value,
                    ReadCount = doc.ReadCount.Value,
                    Title = doc.Title,
                    Tags = doc.Tags,
                    UserId = doc.UserId.Value
                })
                .OrderByDescending(q => q.ThemeId);
        }
    }
}
