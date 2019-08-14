using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Mango.Repository;
using Mango.Framework.Core;

namespace Mango.Common.Aliyun
{
    public class Sms
    {
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <returns></returns>
        public async Task<(bool success, string response)> SendSmsCode(string phone, string code)
        {
            try
            {
                IDictionary<string, string> data = new Dictionary<string, string>();
                data.Add("code", code);
                var sms = new SmsObject
                {
                    Mobile = phone,
                    Signature = Configuration.GetItem("Aliyun_SmsSignature"),
                    TempletKey = Configuration.GetItem("Aliyun_SmsTempletKey"),
                    Data = data,
                    OutId = "OutId"
                };

                return await new AliyunSms(Configuration.GetItem("Aliyun_AccessKeyId"), Configuration.GetItem("Aliyun_AccessKeySecret")).Send(sms);
                
            }
            catch
            {
                return (false, response: "");
            }
        }
    }
}
