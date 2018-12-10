using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Mango.Repository;
using Microsoft.AspNetCore.Http;
using Mango.Framework.EFCore;
namespace Mango.Manager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("RoleId") == null)
            {
                Response.Redirect("/Home/Login");
            }
            //加载权限
            int RoleId = Framework.Core.Transform.GetInt(HttpContext.Session.GetString("RoleId"), 0);
            ManagerAccountRepository repository = new ManagerAccountRepository();
            ViewModels.ManagerPageViewModel model = new ViewModels.ManagerPageViewModel();

            model.ListData = repository.GetCompetence(RoleId);
            //
            return View(model);
        }
        //
        // GET: /Home/
        public ActionResult Login()
        {
            return View();
        }
        public void LoginOut()
        {
            HttpContext.Session.Clear();
            Response.Redirect("/Home/Login");
        }
        [HttpPost]
        public string Login(string adminName, string password,string code)
        {
            if (string.IsNullOrEmpty(adminName) || string.IsNullOrEmpty(password))
            {
                return "用户名和密码不能为空";
            }
            if (HttpContext.Session.GetString("LoginValidateCode") != code.ToLower())
            {
                return "请输入正确的验证码";
            }
            ManagerAccountRepository repository = new ManagerAccountRepository();
            List<MangoData> datas = repository.Login(adminName, Framework.Core.TextHelper.MD5Encrypt(password));
            if (datas.Count ==0)
            {
                return "用户名或者密码错误";
            }
            HttpContext.Session.SetString("AdminName", datas[0]["AdminName"].ToString());
            HttpContext.Session.SetString("AdminId", datas[0]["AdminId"].ToString());
            HttpContext.Session.SetString("RoleId", datas[0]["RoleId"].ToString());
            //登录后跳转到首页
            return "ok";
        }
        public ActionResult Right()
        {
            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
