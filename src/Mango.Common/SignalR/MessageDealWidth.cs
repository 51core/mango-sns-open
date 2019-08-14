using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
namespace Mango.Common.SignalR
{
    /// <summary>
    /// 消息处理
    /// </summary>
    public class MessageDealWidth
    {
        public static async Task DealWidth(string message,ChatHub chatHub)
        {
            await Task.Run(() => {
                try
                {
                    MessageData data = JsonConvert.DeserializeObject<MessageData>(message);
                    if (data != null)
                    {
                        ConnectionUser connectionUser = null;
                        MessageData sendMsg = null;
                        switch (data.MessageType)
                        {
                            case MessageType.Heartbeat:
                                break;
                            case MessageType.Line:
                                connectionUser = ConnectionManager.ConnectionUsers.Where(m => m.ConnectionId == chatHub.Context.ConnectionId).FirstOrDefault();
                                //处理连接消息
                                if (connectionUser == null)
                                {
                                    connectionUser = new ConnectionUser();
                                    connectionUser.ConnectionId = chatHub.Context.ConnectionId;
                                    connectionUser.HeadUrl = data.SendUserHeadUrl;
                                    connectionUser.NickName = data.SendUserNickName;
                                    connectionUser.UserId = data.SendUserId;
                                    ConnectionManager.ConnectionUsers.Add(connectionUser);
                                }
                                //处理发送回执消息
                                sendMsg = new MessageData();
                                sendMsg.MessageBody = "";
                                sendMsg.MessageId = Guid.NewGuid().ToString().Replace("-", "");
                                sendMsg.MessageType = MessageType.LineReceipt;
                                sendMsg.MessageSource = MessageSource.PlatformMessage;
                                sendMsg.ReceiveUserId = data.SendUserId;
                                sendMsg.SendUserId = "0";
                                sendMsg.SendUserHeadUrl = "";
                                sendMsg.SendUserNickName = "";

                                ChatCore.PushCurrentUserMessage(data.SendUserId, chatHub, sendMsg);
                               
                                break;
                            case MessageType.Text:
                                connectionUser = ConnectionManager.ConnectionUsers.Where(m => m.UserId == data.ReceiveUserId).FirstOrDefault();
                                chatHub.Clients.Client(connectionUser.ConnectionId).SendAsync("ReceiveMessage", message);
                                break;

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }
    }
}
