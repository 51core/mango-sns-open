using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.DrawingCore;
namespace Mango.Manager.Controllers
{
    public class AuthorizationController : Controller
    {
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult ValidateCode()
        {
            string code = "";
            System.IO.MemoryStream ms = Framework.Core.ImageVerificationCode.Create(out code);
            HttpContext.Session.SetString("LoginValidateCode", code.ToLower());
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
        }
    }
}