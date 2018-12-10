using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860
using Mango.Common.Upyun;
using Mango.Framework.Core;
namespace Mango.Web.Controllers
{
    public class FileController : Controller
    {
        /// <summary>
        /// 获取服务器签名信息
        /// </summary>
        /// <returns></returns>
        // GET: /<controller>/
        public string UPYun(string fileName)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                Models.UPYunModel model = new Models.UPYunModel();

                model.Expiration = UPYunHelper.GetTimeStamp();
                model.Path = string.Format("/{0}/{1}/{2}/{3}{4}", HttpContext.Session.GetString("UserId"),DateTime.Now.Year,DateTime.Now.Month,System.Guid.NewGuid().ToString().Replace("-",""),Path.GetExtension(fileName));//文件存储地址
                model.Policy = UPYunHelper.GetPolicy(model.Path, model.Expiration);
                model.Signature = string.Format("UPYUN {0}:{1}", Configuration.GetItem("Upyun_BucketName"), UPYunHelper.GetSignature(model.Path, model.Policy));
                return JsonConvert.SerializeObject(model);
            }
            return string.Empty;
        }
    }
}
