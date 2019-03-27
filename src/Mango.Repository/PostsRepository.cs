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
        private EFDbContext _dbContext = null;
        public PostsRepository()
        {
            _dbContext = new EFDbContext();
        }
        /// <summary>
        /// 根据帖子ID获取帖子数据
        /// </summary>
        /// <param name="postsId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>

        public Entity.m_Posts GetPostsByEdit(int postsId,int userId)
        {
            return _dbContext.m_Posts.Where(m => m.PostsId == postsId && m.UserId == userId).FirstOrDefault();
        }
        /// <summary>
        /// 获取热门帖子列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<Models.PostsModel> GetPostsListByHot()
        {
            var query = from p in _dbContext.m_Posts
                        join u in _dbContext.m_User
                        on p.UserId equals u.UserId
                        join c in _dbContext.m_PostsChannel
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
            var query = from p in _dbContext.m_Posts
                        join u in _dbContext.m_User
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
                Entity.m_Posts model = _dbContext.m_Posts.Find(postsId);
                model.ReadCount = model.ReadCount + 1;
                _dbContext.SaveChanges();
            }
            return queryResult;
        }
        /// <summary>
        /// 分页查询帖子数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<Models.PostsModel> GetPostsPageList()
        {
            var query = from p in _dbContext.m_Posts
                        join u in _dbContext.m_User
                        on p.UserId equals u.UserId
                        join c in _dbContext.m_PostsChannel
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
            using (var tran = _dbContext.Database.BeginTransaction())
            {
                try
                {

                    _dbContext.Add(model);
                    _dbContext.SaveChanges();
                    //添加通知信息
                    _dbContext.Add(message);
                    _dbContext.SaveChanges();
                    //更新回答数量
                    _dbContext.MangoUpdate<Entity.m_Posts>(m => m.AnswerCount == m.AnswerCount + 1, m => m.PostsId == model.PostsId);
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
            var query = from a in _dbContext.m_PostsAnswer
                        join u in _dbContext.m_User
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
            using (var tran = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Add(model);
                    _dbContext.SaveChanges();
                    //
                    _dbContext.Add(message);
                    _dbContext.SaveChanges();
                    //
                    _dbContext.MangoUpdate<Entity.m_PostsAnswer>(m => m.CommentsCount == m.CommentsCount + 1, m => m.AnswerId == model.AnswerId);
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
            var query = from cmt in _dbContext.m_PostsComments
                        join u in _dbContext.m_User
                        on cmt.UserId equals u.UserId
                        join tu in _dbContext.m_User
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

        #endregion
        #region 获取帖子相关的发布人ID
        public int GetPostsByUserId(int postsId)
        {
            return _dbContext.m_Posts.Where(m => m.PostsId == postsId).Select(m =>  m.UserId.Value).FirstOrDefault();
        }
        public int GetPostsAnswerByUserId(int answerId)
        {
            return _dbContext.m_PostsAnswer.Where(m => m.AnswerId == answerId).Select(m => m.UserId.Value).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public int GetPostsCommentsByUserId(int commentId)
        {
            var query = from cmt in _dbContext.m_PostsComments
                        join a in _dbContext.m_PostsAnswer
                        on cmt.AnswerId equals a.AnswerId
                        where cmt.CommentId==commentId
                        select a.UserId.Value;
            return query.FirstOrDefault();
        }

        #endregion
    }
}
