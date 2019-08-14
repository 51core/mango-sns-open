using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Mango.Framework.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
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
            return _dbContext.m_Posts
                .Join(_dbContext.m_User, p => p.UserId, u => u.UserId, (p, u) => new { p, u })
                .Join(_dbContext.m_PostsChannel, pu => pu.p.ChannelId, c => c.ChannelId, (pu, c) => new Models.PostsModel()
                {
                    PostsId = pu.p.PostsId.Value,
                    AnswerCount = pu.p.AnswerCount.Value,
                    HeadUrl = pu.u.HeadUrl,
                    IsReply = pu.p.IsReply.Value,
                    IsShow = pu.p.IsShow.Value,
                    LastDate = pu.p.LastDate.Value,
                    PlusCount = pu.p.PlusCount.Value,
                    NickName = pu.u.NickName,
                    PostDate = pu.p.PostDate.Value,
                    ReadCount = pu.p.ReadCount.Value,
                    Title = pu.p.Title,
                    Tags = pu.p.Tags,
                    UserId = pu.p.UserId.Value,
                    ChannelId = pu.p.ChannelId.Value,
                    ChannelName = c.ChannelName
                });
        }
        /// <summary>
        /// 根据帖子Id获取帖子信息
        /// </summary>
        /// <param name="postsId"></param>
        /// <returns></returns>
        public List<Models.PostsModel> GetPosts(int postsId)
        {
            List<Models.PostsModel> queryResult= _dbContext.m_Posts
                .Join(_dbContext.m_User,p=>p.UserId,u=>u.UserId,(p,u) => new Models.PostsModel()
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
                })
                .Where(m => m.PostsId == postsId)
                .ToList();
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
            return _dbContext.m_Posts
                .Join(_dbContext.m_User, p => p.UserId, u => u.UserId, (p, u) => new { p, u })
                .Join(_dbContext.m_PostsChannel, pu => pu.p.ChannelId, c => c.ChannelId, (pu, c) => new Models.PostsModel()
                {
                    PostsId = pu.p.PostsId.Value,
                    AnswerCount = pu.p.AnswerCount.Value,
                    HeadUrl = pu.u.HeadUrl,
                    IsReply = pu.p.IsReply.Value,
                    IsShow = pu.p.IsShow.Value,
                    LastDate = pu.p.LastDate.Value,
                    PlusCount = pu.p.PlusCount.Value,
                    NickName = pu.u.NickName,
                    PostDate = pu.p.PostDate.Value,
                    ReadCount = pu.p.ReadCount.Value,
                    Title = pu.p.Title,
                    Tags = pu.p.Tags,
                    UserId = pu.p.UserId.Value,
                    ChannelId = pu.p.ChannelId.Value,
                    ChannelName = c.ChannelName
                })
                .OrderByDescending(q=>q.PostsId);
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
            return _dbContext.m_PostsAnswer
                .Join(_dbContext.m_User, a => a.UserId, u => u.UserId, (a, u) => new Models.PostsAnswerModel()
                {
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
                });
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
            return _dbContext.m_PostsComments
                .Join(_dbContext.m_User, cmt => cmt.UserId, u => u.UserId, (cmt, u) => new { cmt, u })
                .Join(_dbContext.m_User, cmtu => cmtu.cmt.ToUserId, tu => tu.UserId, (cmtu, tu) => new Models.PostsAnswerCommentsModel()
                {
                    AnswerId = cmtu.cmt.AnswerId.Value,
                    HeadUrl = cmtu.u.HeadUrl,
                    CommentId = cmtu.cmt.CommentId.Value,
                    Contents = cmtu.cmt.Contents,
                    IsShow = cmtu.cmt.IsShow.Value,
                    NickName = cmtu.u.NickName,
                    Plus = cmtu.cmt.Plus.Value,
                    PostDate = cmtu.cmt.PostDate.Value,
                    ToUserHeadUrl = tu.HeadUrl,
                    ToUserId = tu.UserId.GetValueOrDefault(),
                    ToUserNickName = tu.NickName,
                    UserId = cmtu.u.UserId.Value
                })
                .DefaultIfEmpty();
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
            return _dbContext.m_PostsComments
                .Join(_dbContext.m_PostsAnswer, cmt => cmt.AnswerId, a => a.AnswerId, (cmt, u) => cmt)
                .Where(cmt => cmt.CommentId==commentId)
                .Select(cmt => cmt.UserId.Value)
                .FirstOrDefault();
        }

        #endregion
    }
}
