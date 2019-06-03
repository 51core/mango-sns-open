using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Mango.Framework.Services.Cache;
using Newtonsoft.Json;
using System.DrawingCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using Mango.Repository;
namespace Mango.Web.Controllers
{
    public class PassportController : Controller
    {
        /// <summary>
        /// �˳���¼
        /// </summary>
        /// <returns></returns>
        public void OutLogin()
        {
            //����Ự��Ϣ
            HttpContext.Session.Clear();
            Response.Redirect("/passport/login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public string Login(ViewModels.LoginViewModel model)
        {
            UserRepository repository = new UserRepository();
            Mango.Models.UserInfoModel _UserInfoViewModel = repository.UserLogin(model.UserName.Trim(),Framework.Core.TextHelper.MD5Encrypt(model.Password.Trim()));
            if (_UserInfoViewModel == null)
            {
                return "��������ȷ���˺�������!";
            }
            if (!_UserInfoViewModel.IsStatus)
            {
                return "���˺��Ѿ�����ֹ��½!";
            }
            //����½���û�Id�洢���Ự��
            HttpContext.Session.SetString("UserId", _UserInfoViewModel.UserId.ToString());
            HttpContext.Session.SetString("GroupId", _UserInfoViewModel.GroupId.ToString());
            HttpContext.Session.SetString("UserName", _UserInfoViewModel.AccountName);
            HttpContext.Session.SetString("NickName", _UserInfoViewModel.NickName);
            HttpContext.Session.SetString("HeadUrl", _UserInfoViewModel.HeadUrl);
            HttpContext.Session.SetString("UserLogin", JsonConvert.SerializeObject(_UserInfoViewModel));
            return "ok";
        }
        /// <summary>
        /// �û�ע��
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public string Register(ViewModels.RegisterViewModel model)
        {
            string Result = string.Empty;
            UserRepository repository = new UserRepository();
            if (!(model.ValidateCode == HttpContext.Session.GetString("ValidatePhoneCode")))
            {
                return "��������ȷ��ע����֤��!";
            }
            if (!(model.UserName == HttpContext.Session.GetString("ValidatePhone")))
            {
                return "���ֻ�����ͨ��������֤���ֻ��Ų�һ��";
            }
            if (repository.IsExistUser(model.UserName))
            {
               return "���ֻ����Ѿ�ע���!";
            }
            //ע�����û�
            Entity.m_User userModel = new Entity.m_User();
            userModel.HeadUrl = "/images/avatar.png";
            userModel.GroupId = 1;
            userModel.IsStatus = true;
            userModel.LastLoginDate = DateTime.Now;
            userModel.LastLoginIP = string.Empty;
            userModel.NickName = model.NickName;
            userModel.OpenId = "";
            userModel.Password = Framework.Core.TextHelper.MD5Encrypt(model.Password);
            userModel.Phone = "";
            userModel.RegisterDate = DateTime.Now;
            userModel.RegisterIP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            userModel.AccountName = model.UserName;
            userModel.Email = "";
            userModel.AddressInfo = "";
            userModel.Birthday = "";
            userModel.Sex = "��";
            userModel.Tags = "";
            return repository.AddUser(userModel) ? "ok" : "";
        }
    }
}