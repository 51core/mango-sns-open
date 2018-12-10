using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Mango.Repository;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860

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
            Models.UserInfoModel model = repository.GetUserInfo(UserId);
            return View(model);
        }
        /// <summary>
        /// 编辑资料
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="FieldValue"></param>
        /// <returns></returns>
        public bool Edit(string fieldName,string fieldValue)
        {
            if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(fieldValue))
            {
                int userId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("UserId"), 0);

                Entity.m_User model = new Entity.m_User();
                model.UserId = userId;
                switch (fieldName)
                {
                    case "Tags":
                        model.Tags = fieldValue;
                        break;
                    case "Address":
                        model.AddressInfo = fieldValue;
                        break;
                    case "Sex":
                        model.Sex = fieldValue;
                        break;
                    case "NickName":
                        model.NickName = fieldValue;
                        break;
                    case "avatar":
                        model.HeadUrl= fieldValue;
                        break;
                }
                CommonRepository repository = new CommonRepository();
                return repository.Update(model);
            }
            else
            {
                return false;
            }
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
