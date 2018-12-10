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
    public class MessageRepository
    {
        /// <summary>
        /// 更新消息阅读状态
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UpdateMessageReadState(int userId)
        {
            EFDbContext dbContext = new EFDbContext();
            return dbContext.MangoUpdate<Entity.m_Message>(m => m.IsRead == true, m => m.UserId == userId && m.IsRead == false);
        }
        /// <summary>
        /// 根据用户ID获取该用户未读消息数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetUserMessageByCount(int userId)
        {
            EFDbContext dbContext = new EFDbContext();
            return dbContext.m_Message.Where(m => m.UserId == userId&&m.IsRead==false).Select(m => m.MessageId).Count();
        }
        /// <summary>
        /// 分页查询消息数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public IQueryable<Models.MessageModel>  GetMessageList()
        {
            EFDbContext dbContext = new EFDbContext();
            var query = from msg in dbContext.m_Message
                        join u in dbContext.m_User
                        on msg.UserId equals u.UserId
                        orderby msg.MessageId descending
                        select new Models.MessageModel(){
                            AppendUserId= msg.AppendUserId.Value
                            ,Contents= msg.Contents
                            ,IsRead= msg.IsRead.Value
                            ,MessageId= msg.MessageId.Value
                            ,MessageType= msg.MessageType.Value
                            ,ObjId= msg.ObjId.Value
                            ,PostDate= msg.PostDate.Value
                            ,UserId= msg.UserId.Value
                            ,
                            HeadUrl = u.HeadUrl
                            ,
                            NickName= u.NickName
                        };
            return query;
        }
    }
}
