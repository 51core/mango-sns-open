using System;
using System.Collections.Generic;
using Mango.Framework.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Linq;
namespace Mango.Repository
{
    public class UserRepository
    {

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<object> GetUserPageList()
        {
            EFDbContext dbContext = new EFDbContext();
            var query = from u in dbContext.m_User
                        join ug in dbContext.m_UserGroup
                        on u.GroupId equals ug.GroupId
                        select new {
                            u.HeadUrl,u.Email,u.GroupId,u.IsStatus,u.LastLoginDate,u.LastLoginIP,u.NickName,u.OpenId,u.Password,u.Phone,u.RegisterDate,u.RegisterIP,u.UserId,u.UserName
                            ,ug.GroupName
                        };
            return query;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public Models.UserInfoModel GetUserInfo(int userId)
        {
            EFDbContext dbContext = new EFDbContext();
            var query = from u in dbContext.m_User
                        select new Models.UserInfoModel()
                        {
                            UserId = u.UserId.Value,
                            GroupId = u.GroupId.Value,
                            UserName = u.UserName,
                            NickName = u.NickName,
                            HeadUrl = u.HeadUrl,
                            RegisterDate = u.RegisterDate.Value,
                            LastLoginDate = u.LastLoginDate.Value,
                            LastLoginIP = u.LastLoginIP,
                            RegisterIP = u.RegisterIP,
                            IsStatus = u.IsStatus.Value,
                            Address=u.AddressInfo,
                            Birthday=u.Birthday,
                            Sex=u.Sex,
                            Tags=u.Tags
                        };
            return query.Where(m => m.UserId == userId).FirstOrDefault();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Models.UserInfoModel UserLogin(string userName,string password)
        {
            EFDbContext dbContext = new EFDbContext();
            var query = from u in dbContext.m_User
                        select new Models.UserInfoModel()
                        {
                            UserId=u.UserId.Value,
                            GroupId=u.GroupId.Value,
                            UserName=u.UserName,
                            Password=u.Password,
                            NickName=u.NickName,
                            HeadUrl = u.HeadUrl,
                            RegisterDate=u.RegisterDate.Value,
                            LastLoginDate=u.LastLoginDate.Value,
                            LastLoginIP=u.LastLoginIP,
                            RegisterIP=u.RegisterIP,
                            IsStatus=u.IsStatus.Value
                        };
            return query.Where(m => m.UserName == userName && m.Password == password).FirstOrDefault();
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="userInfoModel"></param>
        /// <returns></returns>
        public bool AddUser(Entity.m_User userModel)
        {
            EFDbContext dbContext = new EFDbContext();
            using (var tran = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.Add(userModel);
                    dbContext.SaveChanges();
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
            EFDbContext dbContext = new EFDbContext();
            var query = dbContext.m_User.Where(m => m.UserName == phone);
            if (query.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新角色权限
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="powerData"></param>
        /// <returns></returns>
        public bool UpdateGroupPower(int groupId, List<int> powerData)
        {
            EFDbContext dbContext = new EFDbContext();
            using (var tran = dbContext.Database.BeginTransaction())
            {
                try
                {

                    //移除以前的权限
                    dbContext.MangoRemove<Entity.m_UserGroupPower>(m => m.GroupId == groupId);
                    //添加新权限
                    foreach (int Id in powerData)
                    {
                        Entity.m_UserGroupPower model = new Entity.m_UserGroupPower();
                        model.MId = Id;
                        model.GroupId = groupId;
                        dbContext.Add(model);
                    }
                    dbContext.SaveChanges();
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
            EFDbContext dbContext = new EFDbContext();
            //先处理同一个IP下同一天时间只能获取5条短信验证码
            if (dbContext.m_Sms.Where(m => m.SendTime.Value.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyMMdd") && m.SendIP == userIP).Select(m => m.SmsID.Value).Count() >= 5)
            {
                return false;
            }
            //同一天同一个时间下同一个手机号只能获取3次短信验证码
            if (dbContext.m_Sms.Where(m => m.SendTime.Value.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyMMdd") && m.Phone == phone).Select(m => m.SmsID.Value).Count() >= 3)
            {
                return false;
            }
            return true;
        }
    }
}
