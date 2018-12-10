using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using QRCoder;
using System.DrawingCore;
using System.Text.RegularExpressions;
using Mango.Repository;
using Newtonsoft.Json;
namespace Mango.Web.Controllers
{
    public class AuthorizationController : Controller
    {
        /// <summary>
        /// 短信验证码发送
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpPost]
        public string SendPhoneValidateCode(string phone, string ticket, string randstr)
        {
            UserRepository user = new UserRepository();

            string userIP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            bool tencentCaptchaResult= TencentCaptcha(ticket, randstr, userIP);
            if (!tencentCaptchaResult)
            {
                return "你的验证操作没有通过!";
            }
            Regex regex = new Regex("^1[3456789]\\d{9}$");
            if (!regex.IsMatch(phone))
            {
                return "请输入正确的手机号验证码!";
            }
            if (!user.GetSendSmsState(phone, userIP))
            {
                return "你已经超过短信获取次数限制";
            }
            //短信发送处理
            string PhoneCode = new Random().Next(103113, 985963).ToString();
            HttpContext.Session.SetString("ValidatePhoneCode", PhoneCode);
            HttpContext.Session.SetString("ValidatePhone", phone);
            Common.Aliyun.Sms sms = new Common.Aliyun.Sms();
            var smsResult = sms.SendSmsCode(phone, PhoneCode).Result;
            //插入短信发送记录
            Entity.m_Sms model = new Entity.m_Sms();
            model.Contents = string.Format("短信验证码为:{0} 服务器返回结果:{1}", PhoneCode, smsResult.response);
            model.IsOk = smsResult.success;
            model.Phone = phone;
            model.SendIP = userIP;
            model.SendTime = DateTime.Now;
            CommonRepository repository = new CommonRepository();
            repository.Add(model);
            return smsResult.success ? "注册验证码发送成功,请注意查收!" : "注册验证码发送失败,请稍后再试!";
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult ValidateCode()
        {
            string code = "";
            System.IO.MemoryStream ms = Framework.Core.ImageVerificationCode.Create(out code);
            HttpContext.Session.SetString("LoginValidateCode", code);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
        }
        /// <summary>
        /// 腾讯验证码服务端验证
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="randstr"></param>
        /// <param name="userIP"></param>
        /// <returns></returns>
        private bool TencentCaptcha(string ticket,string randstr,string userIP)
        {
            string aid = Framework.Core.Configuration.GetItem("Tencent_VerificationAppId");
            string appSecretKey = Framework.Core.Configuration.GetItem("Tencent_VerificationAppSecretKey");
            string url = "https://ssl.captcha.qq.com/ticket/verify";
            string p = string.Format("aid={0}&AppSecretKey={1}&Ticket={2}&Randstr={3}&UserIP={4}",aid,appSecretKey, ticket, randstr, userIP);
            Framework.Core.HttpWebRequestHelper http = new Framework.Core.HttpWebRequestHelper();
            string httpResult= http.HttpGet(string.Format("{0}?{1}", url, p));
            Extensions.TencentCaptchaResult tencentCaptchaResult = JsonConvert.DeserializeObject<Extensions.TencentCaptchaResult>(httpResult);
            return tencentCaptchaResult.response == 1 ? true : false;
        }
    }
}