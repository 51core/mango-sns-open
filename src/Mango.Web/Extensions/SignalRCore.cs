using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace Mango.Web.Extensions
{
    public class SignalRCore
    {
        //public static SignalRGlobalHub<MessageHub> GlobalHub { get; set; }
        /// <summary>
        /// 存储在线用户信息
        /// </summary>
        private static Lazy<List<SignalRUser>> _signalrUser = new Lazy<List<SignalRUser>>();
        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        public static List<SignalRUser> SignalRUsers
        {
            get { return _signalrUser.Value; }
        }
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="connectionId"></param>
        public static void AddUser(SignalRUser signalRUser)
        {
            var queryResult = _signalrUser.Value.Where(x => x.ConnectionId == signalRUser.ConnectionId).FirstOrDefault();
            if (queryResult == null)
            {
                _signalrUser.Value.Add(signalRUser);
            }
        }
        /// <summary>
        /// 移除在线用户
        /// </summary>
        /// <param name="userId"></param>
        public static void RemoveUser(string connectionId)
        {
            var queryResult = _signalrUser.Value.Where(x => x.ConnectionId == connectionId).FirstOrDefault();
            if (queryResult != null)
            {
                _signalrUser.Value.Remove(queryResult);
            }
        }
        /// <summary>
        /// 发送给新访问用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="hub"></param>
        public static void PushCurrentUserMessage(string userId, MessageHub hub)
        {
            //获取需要发送的数据
            Repository.MessageRepository repository = new Repository.MessageRepository();
            int count = repository.GetUserMessageByCount(int.Parse(userId));
            //
            var userList = SignalRUsers.Where(x => x.UserId == userId).ToList();
            foreach (SignalRUser user in userList)
            {
                hub.Clients.Client(user.ConnectionId).SendAsync("ReceiveMessage", count);
            }
        }
        /// <summary>
        /// 发送给指定用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="context"></param>
        public static void PushUserMessage(string userId, IHubContext<MessageHub> context)
        {
            Repository.MessageRepository repository = new Repository.MessageRepository();
            int count = repository.GetUserMessageByCount(int.Parse(userId));
            //
            var userList = SignalRUsers.Where(x => x.UserId == userId).ToList();
            foreach (SignalRUser user in userList)
            {
                context.Clients.Client(user.ConnectionId).SendAsync("ReceiveMessage", count);
            }
        }
    }
    public class SignalRUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// SignalR连接ID
        /// </summary>
        public string ConnectionId { get; set; }
    }
}
