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
            var query = from doc in _dbContext.m_Docs
                        orderby doc.DocsId descending
                        select new Models.DocsListModel()
                        {
                            DocsId = doc.DocsId.Value,
                            ShortTitle= doc.ShortTitle,
                            Title = doc.Title,
                            ThemeId=doc.ThemeId.Value,
                            IsShow=doc.IsShow.Value
                        };
            return query.Where(q => q.ThemeId == themeId && q.IsShow == true).ToList();
        }
        /// <summary>
        /// 根据文档ID获取文档数据
        /// </summary>
        /// <param name="docsId"></param>
        /// <returns></returns>
        public Models.DocsModel GetDocsById(int docsId)
        {
            var query = from doc in _dbContext.m_Docs
                        join u in _dbContext.m_User
                        on doc.UserId equals u.UserId
                        orderby doc.DocsId descending
                        select new Models.DocsModel()
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
                            ShortTitle= doc.ShortTitle,
                            ThemeId=doc.ThemeId.Value,
                            Contents=doc.Contents,
                            IsAudit=doc.IsAudit.Value
                        };
            var queryResult = query.Where(q => q.DocsId == docsId).FirstOrDefault();
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
            var query = from doc in _dbContext.m_Docs
                        select new Models.DocsModel()
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
                            Contents = doc.Contents,
                            IsAudit = doc.IsAudit.Value
                        };
            return query.Where(q => q.DocsId == docsId&&q.UserId==userId).FirstOrDefault();
        }
        /// <summary>
        /// 获取文档列表集合
        /// </summary>
        /// <returns></returns>
        public  IQueryable<Models.DocsModel> GetDocsByPage()
        {
            var query = from doc in _dbContext.m_Docs
                        join u in _dbContext.m_User
                        on doc.UserId equals u.UserId
                        select new Models.DocsModel()
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
                            Contents = doc.Contents,
                            IsAudit = doc.IsAudit.Value,
                            NickName=u.NickName,
                            HeadUrl=u.HeadUrl
                        };
            return query;
        }
        /// <summary>
        /// 根据文档主题ID获取数据
        /// </summary>
        /// <param name="themeId">主题ID</param>
        /// <returns></returns>
        public Models.DocsThemeModel GetDocsThemeById(int themeId)
        {
            var query = from doc in _dbContext.m_DocsTheme
                        join u in _dbContext.m_User
                        on doc.UserId equals u.UserId
                        orderby doc.ThemeId descending
                        select new Models.DocsThemeModel()
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
                            Contents= doc.Contents
                        };
            var queryResult= query.Where(q=>q.ThemeId == themeId).FirstOrDefault();
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
            var query = from doc in _dbContext.m_DocsTheme
                        select new Models.DocsThemeModel()
                        {
                            ThemeId = doc.ThemeId.Value,
                            IsShow = doc.IsShow.Value,
                            LastTime = doc.LastTime.Value,
                            PlusCount = doc.PlusCount.Value,
                            AppendTime = doc.AppendTime.Value,
                            ReadCount = doc.ReadCount.Value,
                            Title = doc.Title,
                            Tags = doc.Tags,
                            UserId = doc.UserId.Value,
                            Contents = doc.Contents
                        };
            return query.Where(q => q.ThemeId == themeId&& q.UserId==userId).FirstOrDefault();
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
            var query = from doc in _dbContext.m_DocsTheme
                        join u in _dbContext.m_User
                        on doc.UserId equals u.UserId
                        orderby doc.ThemeId descending
                        select new Models.DocsThemeModel()
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
                        };
            return query;
        }
    }
}
