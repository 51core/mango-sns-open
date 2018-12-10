using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;
using Mango.Framework.Services;
namespace Mango.Web.Extensions
{
    public class MessageHub:Hub
    {
        public MessageHub()
        {
           
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        /// <summary>
        /// 用户连接方法重写
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            IHttpContextAccessor httpContextAccessor = ServiceContext.GetService<IHttpContextAccessor>();
            string userId = string.Empty;
            if (httpContextAccessor.HttpContext.Session.GetString("UserId")!=null)
            {
                userId = httpContextAccessor.HttpContext.Session.GetString("UserId");
                
            }
            SignalRCore.AddUser(new SignalRUser()
            {
                UserId = userId,
                ConnectionId = Context.ConnectionId
            });
            //表示当前用户已经登录
            if (userId != string.Empty)
            {
                //推送指定消息记录
                SignalRCore.PushCurrentUserMessage(userId, this);
            }
            return base.OnConnectedAsync();
        }
        /// <summary>
        /// 用户断开连接方法重写
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            SignalRCore.RemoveUser(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
