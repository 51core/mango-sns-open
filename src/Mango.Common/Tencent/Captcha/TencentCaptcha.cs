using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace Mango.Common.Tencent.Captcha
{
    public class TencentCaptcha
    {
        /// <summary>
        /// 腾讯验证码服务端验证结果查询
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="randstr"></param>
        /// <param name="userIP"></param>
        /// <returns></returns>
        public bool QueryTencentCaptcha(string ticket, string randstr, string userIP)
        {
            string aid = Framework.Core.Configuration.GetItem("Tencent_VerificationAppId");
            string appSecretKey = Framework.Core.Configuration.GetItem("Tencent_VerificationAppSecretKey");
            string url = "https://ssl.captcha.qq.com/ticket/verify";
            string p = string.Format("aid={0}&AppSecretKey={1}&Ticket={2}&Randstr={3}&UserIP={4}", aid, appSecretKey, ticket, randstr, userIP);
            Framework.Core.HttpWebRequestHelper http = new Framework.Core.HttpWebRequestHelper();
            string httpResult = http.HttpGet(string.Format("{0}?{1}", url, p));
            TencentCaptchaResult tencentCaptchaResult = JsonConvert.DeserializeObject<TencentCaptchaResult>(httpResult);
            return tencentCaptchaResult.response == 1 ? true : false;
        }
    }
}
