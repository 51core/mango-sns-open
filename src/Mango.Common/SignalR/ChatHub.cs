using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
namespace Mango.Common.SignalR
{
    public class ChatHub : Hub
    {
        /// <summary>
        /// 服务器端中转消息处理方法
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task ServerTransferMessage(string message)
        {
            MessageData data = JsonConvert.DeserializeObject<MessageData>(message);
            await MessageDealWidth.DealWidth(message, this);
        }
        /// <summary>
        /// 用户连接方法重写
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        /// <summary>
        /// 用户断开连接方法重写
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var item = ConnectionManager.ConnectionUsers.Where(m => m.ConnectionId == Context.ConnectionId).FirstOrDefault();
                //移除相关联用户
                ConnectionManager.ConnectionUsers.Remove(item);
            }
            catch (Exception ex)
            {

            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}
