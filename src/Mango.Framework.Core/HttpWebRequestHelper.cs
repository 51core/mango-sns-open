using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
namespace Mango.Framework.Core
{
    public class HttpWebRequestHelper
    {
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="requestBody">需要传输的参数(多个参数之间用&隔开)</param>
        /// <returns></returns>
        public string HttpPost(string url,string requestBody)
        {
            string result = "";
            try
            {

                byte[] b = Encoding.UTF8.GetBytes(requestBody);
                //请求对象
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                //处理参数
                request.Headers["ContentLength"] = b.Length.ToString();
                Stream requestStream= request.GetRequestStream();
                requestStream.Write(b, 0, b.Length);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                //获取返回的结果
                StreamReader sr = new StreamReader(responseStream);

                string resultStr = sr.ReadToEnd();

                //释放资源
                responseStream.Dispose();
                sr.Dispose();
                result = resultStr;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public string HttpGet(string url)
        {
            string result = "";
            Stream stream = null;
            try
            {
                //加载请求类
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                //获取返回的输出对象
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //获取来自服务器响应的字符流
                stream = response.GetResponseStream();
                //读取返回的结果
                StreamReader reader = new StreamReader(stream);

                result = reader.ReadToEnd();
            }
            catch
            {

            }
            finally
            {
                stream.Dispose();
            }
            return result;
        }
    }
}
