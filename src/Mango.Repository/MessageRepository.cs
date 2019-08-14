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
    public class MessageRepository
    {
        private EFDbContext _dbContext = null;
        public MessageRepository()
        {
            _dbContext = new EFDbContext();
        }
        /// <summary>
        /// 更新消息阅读状态
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UpdateMessageReadState(int userId)
        {
            return _dbContext.MangoUpdate<Entity.m_Message>(m => m.IsRead == true, m => m.UserId == userId && m.IsRead == false);
        }
        /// <summary>
        /// 根据用户ID获取该用户未读消息数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetUserMessageByCount(int userId)
        {
            return _dbContext.m_Message.Where(m => m.UserId == userId&&m.IsRead==false).Select(m => m.MessageId).Count();
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
            return _dbContext.m_Message
                .Join(_dbContext.m_User, msg => msg.UserId, u => u.UserId, (msg, u) => new Models.MessageModel()
                {
                    AppendUserId = msg.AppendUserId.Value,
                    Contents = msg.Contents,
                    IsRead = msg.IsRead.Value,
                    MessageId = msg.MessageId.Value,
                    MessageType = msg.MessageType.Value,
                    ObjId = msg.ObjId.Value,
                    PostDate = msg.PostDate.Value,
                    UserId = msg.UserId.Value,
                    HeadUrl = u.HeadUrl,
                    NickName = u.NickName
                })
                .OrderByDescending(q => q.MessageId);
        }
    }
}
