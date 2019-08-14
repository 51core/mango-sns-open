using System;
using System.Collections.Generic;
using Mango.Framework.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using System.Linq;
namespace Mango.Repository
{
    public class UserRepository
    {
        private EFDbContext _dbContext = null;
        public UserRepository()
        {
            _dbContext = new EFDbContext();
        }
        /// <summary>
        /// 添加用户点赞消息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public int AddUserPlusRecords(Entity.m_UserPlusRecords model, Entity.m_Message message)
        {
            //记录类型 1 帖子点赞 2 帖子回答点赞 3 帖子评论点赞 4 文档主题点赞 5 文档点赞
            int result = 0;
            //加载是否已经存在点赞记录
            var queryCount = _dbContext.m_UserPlusRecords.Where(m => m.ObjectId == model.ObjectId && m.UserId == model.UserId && m.RecordsType == model.RecordsType).Count();
            using (var tran = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (queryCount > 0)
                    {
                        //存在则撤回点赞记录
                        _dbContext.MangoRemove<Entity.m_UserPlusRecords>(m => m.ObjectId == model.ObjectId && m.UserId == model.UserId && m.RecordsType == model.RecordsType);
                        //
                        switch (model.RecordsType.Value)
                        {
                            case 1:
                                _dbContext.MangoUpdate<Entity.m_Posts>(m => m.PlusCount == m.PlusCount - 1, m => m.PostsId == model.ObjectId);
                                break;
                            case 2:
                                _dbContext.MangoUpdate<Entity.m_PostsAnswer>(m => m.Plus == m.Plus - 1, m => m.AnswerId == model.ObjectId);
                                break;
                            case 3:
                                _dbContext.MangoUpdate<Entity.m_PostsComments>(m => m.Plus == m.Plus - 1, m => m.CommentId == model.ObjectId);
                                break;
                            case 4:
                                _dbContext.MangoUpdate<Entity.m_DocsTheme>(m => m.PlusCount == m.PlusCount - 1, m => m.ThemeId == model.ObjectId);
                                break;
                            case 5:
                                _dbContext.MangoUpdate<Entity.m_Docs>(m => m.PlusCount == m.PlusCount - 1, m => m.DocsId == model.ObjectId);
                                break;
                        }
                        tran.Commit();
                        result = -1;
                    }
                    else
                    {
                        //不存在则新增点赞记录
                        _dbContext.Add(message);
                        _dbContext.SaveChanges();
                        //
                        _dbContext.Add(model);
                        _dbContext.SaveChanges();
                        //
                        switch (model.RecordsType.Value)
                        {
                            case 1:
                                _dbContext.MangoUpdate<Entity.m_Posts>(m => m.PlusCount == m.PlusCount + 1, m => m.PostsId == model.ObjectId);
                                break;
                            case 2:
                                _dbContext.MangoUpdate<Entity.m_PostsAnswer>(m => m.Plus == m.Plus + 1, m => m.AnswerId == model.ObjectId);
                                break;
                            case 3:
                                _dbContext.MangoUpdate<Entity.m_PostsComments>(m => m.Plus == m.Plus + 1, m => m.CommentId == model.ObjectId);
                                break;
                            case 4:
                                _dbContext.MangoUpdate<Entity.m_DocsTheme>(m => m.PlusCount == m.PlusCount + 1, m => m.ThemeId == model.ObjectId);
                                break;
                            case 5:
                                _dbContext.MangoUpdate<Entity.m_Docs>(m => m.PlusCount == m.PlusCount + 1, m => m.DocsId == model.ObjectId);
                                break;
                        }
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
        /// 获取用户信息列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<object> GetUserPageList()
        {
            return _dbContext.m_User
                .Join(_dbContext.m_UserGroup, u => u.GroupId, ug => ug.GroupId, (u, ug) => new
                {
                    u.HeadUrl,u.Email,u.GroupId,u.IsStatus,u.LastLoginDate,u.LastLoginIP,u.NickName,u.OpenId,u.Password,u.Phone,u.RegisterDate,
                    u.RegisterIP,u.UserId,u.AccountName,ug.GroupName
                });
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public Models.UserInfoModel GetUserInfo(int userId)
        {
            return _dbContext.m_User
                .Where(u => u.UserId == userId)
                .Select(u => new Models.UserInfoModel()
                {
                    UserId = u.UserId.Value,
                    GroupId = u.GroupId.Value,
                    AccountName = u.AccountName,
                    NickName = u.NickName,
                    HeadUrl = u.HeadUrl,
                    RegisterDate = u.RegisterDate.Value,
                    LastLoginDate = u.LastLoginDate.Value,
                    LastLoginIP = u.LastLoginIP,
                    RegisterIP = u.RegisterIP,
                    IsStatus = u.IsStatus.Value,
                    Address = u.AddressInfo,
                    Birthday = u.Birthday,
                    Sex = u.Sex,
                    Tags = u.Tags
                })
                .FirstOrDefault();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Models.UserInfoModel UserLogin(string accountName,string password)
        {
            return _dbContext.m_User
                .Where(m => m.AccountName == accountName && m.Password == password)
                .Select(u => new Models.UserInfoModel()
                {
                    UserId = u.UserId.Value,
                    GroupId = u.GroupId.Value,
                    AccountName = u.AccountName,
                    NickName = u.NickName,
                    HeadUrl = u.HeadUrl,
                    RegisterDate = u.RegisterDate.Value,
                    LastLoginDate = u.LastLoginDate.Value,
                    LastLoginIP = u.LastLoginIP,
                    RegisterIP = u.RegisterIP,
                    IsStatus = u.IsStatus.Value,
                    Address = u.AddressInfo,
                    Birthday = u.Birthday,
                    Sex = u.Sex,
                    Tags = u.Tags
                })
                .FirstOrDefault();
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="userInfoModel"></param>
        /// <returns></returns>
        public bool AddUser(Entity.m_User userModel)
        {
            using (var tran = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Add(userModel);
                    _dbContext.SaveChanges();
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
        /// 检测邮箱是否已经注册过
        /// </summary>
        /// <param name="phone">注册手机号</param>
        /// <returns>true 表示已经注册过,false 表示未注册过</returns>
        public bool IsExistUser(string phone)
        {
            var query = _dbContext.m_User.Where(m => m.AccountName == phone);
            return query.Count() > 0 ? true : false;
        }
        /// <summary>
        /// 更新角色权限
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="powerData"></param>
        /// <returns></returns>
        public bool UpdateGroupPower(int groupId, List<int> powerData)
        {
            using (var tran = _dbContext.Database.BeginTransaction())
            {
                try
                {

                    //移除以前的权限
                    _dbContext.MangoRemove<Entity.m_UserGroupPower>(m => m.GroupId == groupId);
                    //添加新权限
                    foreach (int Id in powerData)
                    {
                        Entity.m_UserGroupPower model = new Entity.m_UserGroupPower();
                        model.MId = Id;
                        model.GroupId = groupId;
                        _dbContext.Add(model);
                    }
                    _dbContext.SaveChanges();
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
        /// 获取是否允许发送短信的状态
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="userIP">短信</param>
        /// <returns></returns>
        public bool GetSendSmsState(string phone, string userIP)
        {
            //先处理同一个IP下同一天时间只能获取5条短信验证码
            if (_dbContext.m_Sms.Where(m => m.SendTime.Value.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyMMdd") && m.SendIP == userIP).Select(m => m.SmsID.Value).Count() >= 5)
            {
                return false;
            }
            //同一天同一个时间下同一个手机号只能获取3次短信验证码
            if (_dbContext.m_Sms.Where(m => m.SendTime.Value.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyMMdd") && m.Phone == phone).Select(m => m.SmsID.Value).Count() >= 3)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 根据用户ID获取昵称信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserNickName(int userId)
        {
            return _dbContext.m_User.Where(m => m.UserId == userId).Select(m => m.NickName).FirstOrDefault();
        }
    }
}
