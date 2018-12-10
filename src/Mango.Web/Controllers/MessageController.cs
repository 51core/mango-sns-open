using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Mango.Repository;
using Microsoft.AspNetCore.SignalR;
namespace Mango.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHubContext<Extensions.MessageHub> _messageHubContext;
        public MessageController(IHubContext<Extensions.MessageHub> messageHubContext)
        {
            _messageHubContext = messageHubContext;
        }
        /// <summary>
        /// 获取当前用户未读消息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetMessageList()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                int userId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"),0);
                MessageRepository repository = new MessageRepository();
                var query= repository.GetMessageList();
                var queryResult = query.Where(m => m.UserId == userId && m.IsRead == false).ToList();
                return Json(queryResult);
            }
            return Json(new { });
        }

        [HttpPost]
        public bool UpdateReadState()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                int userId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
                MessageRepository repository = new MessageRepository();
                bool result= repository.UpdateMessageReadState(userId);
                if (result)
                {
                    //消息推送
                    Extensions.SignalRCore.PushUserMessage(userId.ToString(), _messageHubContext);
                }
                return result;
            }
            return false;
        }
    }
}