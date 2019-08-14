using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Mango.Repository;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860
using Mango.Models;
namespace Mango.Web.Areas.User.Controllers
{
    [Area("User")]
    public class InfoController : Controller
    {
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            int UserId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
            UserRepository repository = new UserRepository();
            UserInfoModel model = repository.GetUserInfo(UserId);
            return View(model);
        }
        /// <summary>
        /// 设置用户资料
        /// </summary>
        /// <param name="infoType">资料信息类型(1.用户昵称 2.用户头像 3.用户标签 4.用户所在地 5.性别)</param>
        /// <param name="infoValue">信息值</param>
        /// <returns></returns>
        [HttpPost]
        public bool SetUserInfo(int infoType, string infoValue)
        {
            if (string.IsNullOrEmpty(infoValue))
            {
                return false;
            }
            int userId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);
            Entity.m_User model = new Entity.m_User();
            model.UserId = userId;
            switch (infoType)
            {
                case 3:
                    model.Tags = infoValue;
                    break;
                case 4:
                    model.AddressInfo = infoValue;
                    break;
                case 5:
                    model.Sex = infoValue;
                    break;
                case 1:
                    model.NickName = infoValue;
                    break;
                case 2:
                    model.HeadUrl = infoValue;
                    break;
            }
            CommonRepository repository = new CommonRepository();
            return repository.Update(model);
        }
        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        public bool Password(string oldPassword,string newPassword)
        {
            if (!string.IsNullOrEmpty(oldPassword) && !string.IsNullOrEmpty(newPassword))
            {
                int UserId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);

                Entity.m_User model = new Entity.m_User();
                model.UserId = UserId;
                model.Password = Framework.Core.TextHelper.MD5Encrypt(newPassword);

                CommonRepository repository = new CommonRepository();
                return repository.Update(model);
            }
            else
            {
                return false;
            }
        }
    }
}
