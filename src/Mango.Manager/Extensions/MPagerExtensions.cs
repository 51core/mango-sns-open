using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;
namespace Microsoft.AspNetCore.Mvc.ViewFeatures
{ 
    public static class MPagerExtensions
    {
        /// <summary>
        /// 自定义分页(Ayong:2016.10.20)
        /// </summary>
        /// <param name="HtmlHelper"></param>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">每页记录条数</param>
        /// <param name="TotalCount">总条数</param>
        /// <returns></returns>
        public static IHtmlContent MPager(this IHtmlHelper HtmlHelper, HttpRequest Request, int TotalCount, int PageSize=10)
        {
            StringBuilder ResultBuilder = new StringBuilder();//输出结果
            try
            {
                int PageIndex = 1;
                string url = Request.Path;
                //拼接除页码参数以外的所有参数
                string param = string.Empty;
                foreach (string key in Request.Query.Keys)
                {
                    if (key != "p")
                    {
                        param += string.Format("&{0}={1}", key, Request.Query[key]);
                    }
                }
                param = param.TrimStart('&');
                url = string.Format("{0}?{1}", url, param);
                //获取当前页码
                if (Request.Query["p"].Count >0)
                {
                    PageIndex = int.Parse(Request.Query["p"]);
                }

                int PageCount = 0;
                //得到总页码数
                if (TotalCount % PageSize == 0)
                {
                    PageCount = TotalCount / PageSize;
                }
                else
                {
                    PageCount = TotalCount / PageSize + 1;
                }
                //处理分页样式
                //ResultBuilder.Append("<ul>");
                ResultBuilder.AppendFormat("<li  class=\"page-item\"><a  class=\"page-link\" href=\"{0}\">首页</a></li>", url);

                ResultBuilder.AppendFormat("<li  class=\"page-item\"><a class=\"page-link\"  href=\"{0}&p={1}\">上页</a></li>", url, PageIndex == 1 ? 1 : PageIndex - 1);

                //中间页码计算
                int BeginCount = 1;//开始页码
                if (PageIndex > 5)
                {
                    BeginCount = PageIndex - 5;
                }
                int EndCount = PageCount;
                if (PageCount - PageIndex > 5)
                {
                    EndCount = PageIndex + 5;
                }

                for (int i = BeginCount; i <= EndCount; i++)
                {
                    if (PageIndex == i)
                    {
                        ResultBuilder.Append(string.Format("<li  class=\"page-item  active  disabled\"><a class=\"page-link\" >{0}</a></li>", i));
                    }
                    else
                    {
                        ResultBuilder.Append(string.Format("<li  class=\"page-item\"><a  class=\"page-link\"  href=\"{0}&p={1}\">{2}</a></li>", url, i, i));
                    }
                }
                //
                ResultBuilder.AppendFormat("<li  class=\"page-item\"><a  class=\"page-link\"  href=\"{0}&p={1}\">下页</a></li>", url, PageIndex == PageCount ? PageCount : PageIndex + 1);
                ResultBuilder.AppendFormat("<li  class=\"page-item\"><a  class=\"page-link\"  href=\"{0}&p={1}\">尾页</a></li>", url, PageCount);
                //ResultBuilder.Append("</ul>");
            }
            catch
            {
                ResultBuilder.Append("分页出现异常...");
            }
            return new HtmlString(ResultBuilder.ToString());
        }
    }
}