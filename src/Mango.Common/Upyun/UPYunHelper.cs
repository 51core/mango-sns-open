using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Mango.Framework.Core;
namespace Mango.Common.Upyun
{
    /// <summary>
    /// 又拍云文件上传
    /// </summary>
    public class UPYunHelper
    {
        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="path"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        public static string GetSignature(string path,string policy)
        {
            string SignatureText = string.Format("POST&/{0}&{1}", Configuration.GetItem("Upyun_BucketName"), policy);
            return HmacSha1Sign(SignatureText);
        }
        /// <summary>
        /// 加密签名计算
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string HmacSha1Sign(string text)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(text);

            HMACSHA1 hmac = new HMACSHA1(Encoding.UTF8.GetBytes(TextHelper.MD5Encrypt(Configuration.GetItem("Upyun_BucketPassword"))));
            return Convert.ToBase64String(hmac.ComputeHash(byteData));
        }
        /// <summary>
        /// 获取参数策略
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetPolicy(string path, string expiration)
        {
            //时间戳
            string parm = "{\"bucket\":\""+ Configuration.GetItem("Upyun_BucketName") + "\",\"expiration\":\"" + expiration + "\",\"save-key\":\"" + path + "\"}";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(parm));
        }
        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow.AddMinutes(30) - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}
