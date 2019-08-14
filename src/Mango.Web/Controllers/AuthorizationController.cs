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
        /// ������֤�뷢��
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
                return "�����֤����û��ͨ��!";
            }
            Regex regex = new Regex("^1[3456789]\\d{9}$");
            if (!regex.IsMatch(phone))
            {
                return "��������ȷ���ֻ�����֤��!";
            }
            if (!user.GetSendSmsState(phone, userIP))
            {
                return "���Ѿ��������Ż�ȡ��������";
            }
            //���ŷ��ʹ���
            string PhoneCode = new Random().Next(103113, 985963).ToString();
            HttpContext.Session.SetString("ValidatePhoneCode", PhoneCode);
            HttpContext.Session.SetString("ValidatePhone", phone);
            Common.Aliyun.Sms sms = new Common.Aliyun.Sms();
            var smsResult = sms.SendSmsCode(phone, PhoneCode).Result;
            //������ŷ��ͼ�¼
            Entity.m_Sms model = new Entity.m_Sms();
            model.Contents = string.Format("������֤��Ϊ:{0} ���������ؽ��:{1}", PhoneCode, smsResult.response);
            model.IsOk = smsResult.success;
            model.Phone = phone;
            model.SendIP = userIP;
            model.SendTime = DateTime.Now;
            CommonRepository repository = new CommonRepository();
            repository.Add(model);
            return smsResult.success ? "ע����֤�뷢�ͳɹ�,��ע�����!" : "ע����֤�뷢��ʧ��,���Ժ�����!";
        }
        /// <summary>
        /// ��ȡ��֤��
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
        /// ��Ѷ��֤��������֤
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