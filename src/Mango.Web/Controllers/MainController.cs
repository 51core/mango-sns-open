using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Mango.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.SignalR;
namespace Mango.Web.Controllers
{
    public class MainController : Controller
    {
        private readonly IHubContext<Extensions.MessageHub> _messageHubContext;
        public MainController(IHubContext<Extensions.MessageHub> messageHubContext)
        {
            _messageHubContext = messageHubContext;
        }
        /// <summary>
        /// 用户点赞处理
        /// </summary>
        /// <param name="objectId">点赞对象ID</param>
        /// <param name="title">点赞标题</param>
        /// <param name="plusType">点赞类型(1 帖子点赞 2 帖子回答点赞 3 帖子评论点赞 4 文档主题点赞 5 文档点赞)</param>
        /// <param name="toUserId">点赞接收用户ID</param>
        /// <param name="id">额外补充ID(文档点赞时为文档主题ID)</param>
        /// <returns></returns>
        [HttpPost]
        public int AddUserPlus(int objectId, string title,int plusType,int toUserId,int id=0)
        {
            UserRepository repository = new UserRepository();

            Entity.m_UserPlusRecords model = new Entity.m_UserPlusRecords();
            model.ObjectId = objectId;
            model.AppendTime = DateTime.Now;
            model.RecordsType = plusType;
            model.UserId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
            //
            int messageType = 0;
            switch (model.RecordsType.Value)
            {
                case 1:
                    messageType = 12;
                    break;
                case 2:
                    messageType = 13;
                    break;
                case 3:
                    messageType = 14;
                    break;
                case 4:
                    messageType = 20;
                    break;
                case 5:
                    messageType = 21;
                    break;
            }
            //消息通知
            Entity.m_Message message = new Entity.m_Message();
            message.AppendUserId = model.UserId;
            message.Contents = Common.MessageHtml.GetMessageContent(HttpContext.Session.GetString("NickName"), objectId, title, messageType, id);
            message.IsRead = false;
            message.MessageType = messageType;
            message.ObjId = objectId;
            message.PostDate = DateTime.Now;
            message.UserId = toUserId;
            //消息推送
            int resultCount = repository.AddUserPlusRecords(model, message);
            if (resultCount > 0)
            {
                Extensions.SignalRCore.PushUserMessage(message.UserId.ToString(), _messageHubContext);
            }
            return resultCount;
        }
    }
}