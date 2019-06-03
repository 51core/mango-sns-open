using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Mango.Common.Upyun;
using Mango.Framework.Core;
using Mango.Models;
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
                UPYunModel model = new UPYunModel();

                model.Expiration = UPYunHelper.GetTimeStamp();
                model.Path = string.Format("/{0}/{1}/{2}/{3}{4}", HttpContext.Session.GetString("UserId"),DateTime.Now.Year,DateTime.Now.Month,System.Guid.NewGuid().ToString().Replace("-",""),Path.GetExtension(fileName));//文件存储地址
                model.Policy = UPYunHelper.GetPolicy(model.Path, model.Expiration);
                model.Signature = string.Format("UPYUN {0}:{1}", Configuration.GetItem("Upyun_BucketName"), UPYunHelper.GetSignature(model.Path, model.Policy));
                return JsonConvert.SerializeObject(model);
            }
            return string.Empty;
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="env"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public string Upload([FromServices]IWebHostEnvironment env, IFormFile file)
        {
            string result = string.Empty;
            if (file.Length > 0)
            {
                string filename = Path.Combine("upload/", string.Format("{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(file.FileName)));
                string filePath = Path.Combine(env.WebRootPath, filename);
                using (var stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    file.CopyTo(stream);
                }
                result = filename;
            }
            return string.Format("/{0}", result);
        }
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="_hostingEnvironment"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public IActionResult Download([FromServices]IWebHostEnvironment _hostingEnvironment, string file)
        {
            string DownLoadFilePath = Path.Combine(_hostingEnvironment.WebRootPath, file);
            try
            {
                //文件读取
                var stream = new FileStream(DownLoadFilePath, FileMode.Open);
                return File(stream, "application/octet-stream", Path.GetFileName(file));
            }
            catch (Exception)
            {
                return Content("抱歉,您要下载的文件不存在!!!");
            }
        }
    }
}
