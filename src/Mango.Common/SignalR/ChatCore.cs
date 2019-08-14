using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
namespace Mango.Common.SignalR
{
    public class ChatCore
    {
        /// <summary>
        /// 发送给新访问用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="hub"></param>
        public static void PushCurrentUserMessage(string userId, ChatHub chatHub, MessageData messageData)
        {

            //获取需要发送的数据
            Repository.MessageRepository repository = new Repository.MessageRepository();
            int count = repository.GetUserMessageByCount(int.Parse(userId));
            //
            var userList = ConnectionManager.ConnectionUsers.Where(x => x.UserId == userId).ToList();

            messageData.MessageBody = Convert.ToString(count);
            var sendMsg = JsonConvert.SerializeObject(messageData);
            foreach (ConnectionUser user in userList)
            {
                chatHub.Clients.Client(user.ConnectionId).SendAsync("ReceiveMessage", sendMsg);
            }
        }
        /// <summary>
        /// 发送给指定用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="context"></param>
        public static void PushUserMessage(string userId, IHubContext<ChatHub> chatHub)
        {
            Repository.MessageRepository repository = new Repository.MessageRepository();
            int count = repository.GetUserMessageByCount(int.Parse(userId));

            //处理发送消息
            MessageData messageData = new MessageData();
            messageData.MessageBody = "";
            messageData.MessageId = Guid.NewGuid().ToString().Replace("-", "");
            messageData.MessageType = MessageType.LineReceipt;
            messageData.MessageSource = MessageSource.PlatformMessage;
            messageData.SendUserId = "0";
            messageData.SendUserHeadUrl = "";
            messageData.SendUserNickName = "";
            //
            var userList = ConnectionManager.ConnectionUsers.Where(x => x.UserId == userId).ToList();
            messageData.MessageBody = Convert.ToString(count);
            var sendMsg = JsonConvert.SerializeObject(messageData);
            foreach (ConnectionUser user in userList)
            {
                messageData.ReceiveUserId = user.UserId;
                chatHub.Clients.Client(user.ConnectionId).SendAsync("ReceiveMessage", sendMsg);
            }
        }
    }
}
