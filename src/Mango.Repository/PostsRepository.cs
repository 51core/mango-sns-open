using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mango.Framework.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
namespace Mango.Repository
{
    public class PostsRepository
    {
        /// <summary>
        /// 获取热门帖子列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<Models.PostsModel> GetPostsListByHot()
        {
            EFDbContext dbContext = new EFDbContext();
            var query = from p in dbContext.m_Posts
                        join u in dbContext.m_User
                        on p.UserId equals u.UserId
                        join c in dbContext.m_PostsChannel
                        on p.ChannelId equals c.ChannelId
                        select new Models.PostsModel()
                        {
                            PostsId = p.PostsId.Value,
                            AnswerCount = p.AnswerCount.Value,
                            HeadUrl = u.HeadUrl,
                            IsReply = p.IsReply.Value,
                            IsShow = p.IsShow.Value,
                            LastDate = p.LastDate.Value,
                            PlusCount = p.PlusCount.Value,
                            NickName = u.NickName,
                            PostDate = p.PostDate.Value,
                            ReadCount = p.ReadCount.Value,
                            Title = p.Title,
                            Tags = p.Tags,
                            UserId = p.UserId.Value,
                            ChannelId = p.ChannelId.Value,
                            ChannelName = c.ChannelName
                        };
            
            return query;
        }
        /// <summary>
        /// 根据帖子Id获取帖子信息
        /// </summary>
        /// <param name="postsId"></param>
        /// <returns></returns>
        public List<Models.PostsModel> GetPosts(int postsId)
        {
            EFDbContext dbContext = new EFDbContext();
            var query = from p in dbContext.m_Posts
                        join u in dbContext.m_User
                        on p.UserId equals u.UserId
                        select new Models.PostsModel()
                        {
                            PostsId = p.PostsId.Value,
                            AnswerCount = p.AnswerCount.Value,
                            HeadUrl = u.HeadUrl,
                            IsReply = p.IsReply.Value,
                            IsShow = p.IsShow.Value,
                            LastDate = p.LastDate.Value,
                            PlusCount = p.PlusCount.Value,
                            NickName = u.NickName,
                            PostDate = p.PostDate.Value,
                            ReadCount = p.ReadCount.Value,
                            Title = p.Title,
                            Contents = p.Contents,
                            Tags = p.Tags,
                            UserId = p.UserId.Value
                        };
            List<Models.PostsModel> queryResult = query.Where(m => m.PostsId == postsId).ToList();
            if (queryResult.Count > 0)
            {
                Entity.m_Posts model = dbContext.m_Posts.Find(postsId);
                model.ReadCount = model.ReadCount + 1;
                dbContext.SaveChanges();
            }
            return queryResult;
        }
        /// <summary>
        /// 分页查询帖子数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<Models.PostsModel> GetPostsPageList()
        {
            //string sql = GetPostsSql();
            EFDbContext dbContext = new EFDbContext();
            var query = from p in dbContext.m_Posts
                        join u in dbContext.m_User
                        on p.UserId equals u.UserId
                        join c in dbContext.m_PostsChannel
                        on p.ChannelId equals c.ChannelId
                        orderby p.PostsId descending
                        select new Models.PostsModel()
                        {
                            PostsId = p.PostsId.Value,
                            AnswerCount = p.AnswerCount.Value,
                            HeadUrl = u.HeadUrl,
                            IsReply = p.IsReply.Value,
                            IsShow = p.IsShow.Value,
                            LastDate = p.LastDate.Value,
                            PlusCount = p.PlusCount.Value,
                            NickName = u.NickName,
                            PostDate = p.PostDate.Value,
                            ReadCount = p.ReadCount.Value,
                            Title = p.Title,
                            Tags = p.Tags,
                            UserId = p.UserId.Value,
                            ChannelId = p.ChannelId.Value,
                            ChannelName = c.ChannelName
                        };
            return query;
        }
        #region 帖子回答
        public bool AddAnswer(Entity.m_PostsAnswer model, Entity.m_Message message)
        {
            EFDbContext dbContext = new EFDbContext();
            using (var tran = dbContext.Database.BeginTransaction())
            {
                try
                {

                    dbContext.Add(model);
                    dbContext.SaveChanges();
                    //添加通知信息
                    dbContext.Add(message);
                    dbContext.SaveChanges();
                    //更新回答数量
                    dbContext.MangoUpdate<Entity.m_Posts>(m => m.AnswerCount == m.AnswerCount + 1, m => m.PostsId == model.PostsId);
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
        /// <summary>
        /// 分页查询回答数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<Models.PostsAnswerModel> GetAnswerPageList()
        {
            EFDbContext dbContext = new EFDbContext();
            var query = from a in dbContext.m_PostsAnswer
                        join u in dbContext.m_User
                        on a.UserId equals u.UserId
                        select new Models.PostsAnswerModel() {
                            AnswerId = a.AnswerId.Value,
                            HeadUrl = u.HeadUrl,
                            CommentsCount = a.CommentsCount.Value,
                            Contents = a.Contents,
                            IsShow = a.IsShow.Value,
                            NickName = u.NickName,
                            Plus = a.Plus.Value,
                            PostDate = a.PostDate.Value,
                            PostsId = a.PostsId.Value,
                            UserId = a.UserId.Value
                        };
            return query;
        }
        /// <summary>
        /// 添加回答评论
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddAnswerComments(Entity.m_PostsComments model,Entity.m_Message message)
        {
            EFDbContext dbContext = new EFDbContext();
            using (var tran = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.Add(model);
                    dbContext.SaveChanges();
                    //
                    dbContext.Add(message);
                    dbContext.SaveChanges();
                    //
                    dbContext.MangoUpdate<Entity.m_PostsAnswer>(m => m.CommentsCount == m.CommentsCount + 1, m => m.AnswerId == model.AnswerId);
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
        /// <summary>
        /// 分页查询回复数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<Models.PostsAnswerCommentsModel> GetCommentsPageList()
        {
            EFDbContext dbContext = new EFDbContext();
            var query = from cmt in dbContext.m_PostsComments
                        join u in dbContext.m_User
                        on cmt.UserId equals u.UserId
                        join tu in dbContext.m_User
                        on cmt.ToUserId equals tu.UserId into tul
                        from tuleft in tul.DefaultIfEmpty()
                        select new Models.PostsAnswerCommentsModel() {
                            AnswerId = cmt.AnswerId.Value,
                            HeadUrl = u.HeadUrl,
                            CommentId = cmt.CommentId.Value,
                            Contents = cmt.Contents,
                            IsShow = cmt.IsShow.Value,
                            NickName = u.NickName,
                            Plus = cmt.Plus.Value,
                            PostDate = cmt.PostDate.Value,
                            ToUserHeadUrl = tuleft.HeadUrl,
                            ToUserId = tuleft.UserId.GetValueOrDefault(),
                            ToUserNickName = tuleft.NickName,
                            UserId = u.UserId.Value
                        };
            return query;
        }
        /// <summary>
        /// 添加帖子回答点赞
        /// </summary>
        /// <param name="model"></param>
        /// <returns>-1 表示取消点赞 1表示增加点赞 0表示异常</returns>
        public int AddAnswerRecordsByPlus(Entity.m_PostsAnswerRecords model,Entity.m_Message message)
        {
            int result = 0;
            EFDbContext dbContext = new EFDbContext();
            //加载是否已经存在点赞记录
            int queryCount = dbContext.m_PostsAnswerRecords.Where(m => m.AnswerId == model.AnswerId && m.UserId == model.UserId).Count();
            using (var tran = dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (queryCount > 0)
                    {
                        //
                        dbContext.MangoRemove<Entity.m_PostsAnswerRecords>(m => m.AnswerId == model.AnswerId && m.UserId == model.UserId);
                        //
                        dbContext.MangoUpdate<Entity.m_PostsAnswer>(m => m.Plus == m.Plus - 1, m => m.AnswerId == model.AnswerId);
                        tran.Commit();
                        result = -1;
                    }
                    else
                    {
                        dbContext.Add(message);
                        dbContext.SaveChanges();
                        //
                        dbContext.Add(model);
                        dbContext.SaveChanges();
                        //
                        dbContext.MangoUpdate<Entity.m_PostsAnswer>(m => m.Plus == m.Plus + 1, m => m.AnswerId == model.AnswerId);
                        tran.Commit();
                        result = 1;
                    }
                }
                catch
                {
                    tran.Rollback();
                    result = 0;
                }
            }
            return result;
        }
        /// <summary>
        /// 添加评论点赞
        /// </summary>
        /// <param name="model"></param>
        /// <returns>-1 表示取消点赞 1表示增加点赞 0表示异常</returns>
        public int AddCommentsRecordsByPlus(Entity.m_PostsCommentsRecords model,Entity.m_Message message)
        {
            int result = 0;
            EFDbContext dbContext = new EFDbContext();
            //加载是否已经存在点赞记录
            int queryCount = dbContext.m_PostsCommentsRecords.Where(m => m.CommentId == model.CommentId && m.UserId == model.UserId).Count();
            using (var tran = dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (queryCount > 0)
                    {
                        dbContext.MangoRemove<Entity.m_PostsCommentsRecords>(m => m.CommentId == model.CommentId && m.UserId == model.UserId);
                        //
                        dbContext.MangoUpdate<Entity.m_PostsComments>(m => m.Plus == m.Plus - 1, m => m.CommentId == model.CommentId);
                        tran.Commit();
                        result = -1;
                    }
                    else
                    {
                        //不存在则新增点赞记录
                        dbContext.Add(message);
                        dbContext.SaveChanges();
                        //
                        dbContext.Add(model);
                        dbContext.SaveChanges();
                        //
                        dbContext.MangoUpdate<Entity.m_PostsComments>(m => m.Plus == m.Plus + 1, m => m.CommentId == model.CommentId);
                        tran.Commit();
                        result = 1;
                    }
                }
                catch
                {
                    tran.Rollback();
                    result = 0;
                }
            }
            
            return result;
        }
        /// <summary>
        /// 添加帖子内容点赞
        /// </summary>
        /// <param name="model"></param>
        /// <returns>-1 表示取消点赞 1表示增加点赞 0表示异常</returns>
        public int AddPostsRecordsByPlus(Entity.m_PostsRecords model, Entity.m_Message message)
        {
            int result = 0;
            EFDbContext dbContext = new EFDbContext();
            //加载是否已经存在点赞记录
            var queryCount = dbContext.m_PostsRecords.Where(m => m.PostsId == model.PostsId && m.UserId == model.UserId).Count();
            using (var tran = dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (queryCount > 0)
                    {
                        //存在则撤回点赞记录
                        dbContext.MangoRemove<Entity.m_PostsRecords>(m => m.PostsId == model.PostsId && m.UserId == model.UserId);
                        //
                        dbContext.MangoUpdate<Entity.m_Posts>(m => m.PlusCount == m.PlusCount - 1, m => m.PostsId == model.PostsId);
                        tran.Commit();
                        result = -1;
                    }
                    else
                    {
                        //不存在则新增点赞记录
                        dbContext.Add(message);
                        dbContext.SaveChanges();
                        //
                        dbContext.Add(model);
                        dbContext.SaveChanges();
                        //
                        dbContext.MangoUpdate<Entity.m_Posts>(m => m.PlusCount == m.PlusCount + 1, m => m.PostsId == model.PostsId);
                        tran.Commit();
                        result = 1;
                    }
                }
                catch
                {
                    tran.Rollback();
                    result = 0;
                }
            }
            
            return result;
        }

        #endregion
        #region 获取帖子相关的发布人ID
        public int GetPostsByUserId(int postsId)
        {
            EFDbContext dbContext = new EFDbContext();
            return dbContext.m_Posts.Where(m => m.PostsId == postsId).Select(m =>  m.UserId.Value).FirstOrDefault();
        }
        public int GetPostsAnswerByUserId(int answerId)
        {
            EFDbContext dbContext = new EFDbContext();
            return dbContext.m_PostsAnswer.Where(m => m.AnswerId == answerId).Select(m => m.UserId.Value).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public int GetPostsCommentsByUserId(int commentId)
        {
            EFDbContext dbContext = new EFDbContext();
            var query = from cmt in dbContext.m_PostsComments
                        join a in dbContext.m_PostsAnswer
                        on cmt.AnswerId equals a.AnswerId
                        where cmt.CommentId==commentId
                        select a.UserId.Value;
            return query.FirstOrDefault();
        }

        #endregion
    }
}
