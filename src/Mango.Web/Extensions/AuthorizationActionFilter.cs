using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
namespace Mango.Web.Extensions
{
    public class AuthorizationActionFilter :  IActionFilter
    {
        /// <summary>
        /// 过滤对象列表
        /// </summary>
        private static List<string[]> _filterList { get; set; }
        public AuthorizationActionFilter()
        {
            if (_filterList == null)
            {
                _filterList = new List<string[]>();
                _filterList.Add(new string[] { "","authorization", "validatecode" });
                _filterList.Add(new string[] { "", "authorization", "sendphonevalidatecode" });
                _filterList.Add(new string[] { "", "navigation", "updateclickcount" });
                _filterList.Add(new string[] { "", "home", "message" });
                _filterList.Add(new string[] { "", "home", "error" });
                _filterList.Add(new string[] { "", "home", "index" });
            }
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Action请求执行前处理事件
            //获取权限验证所需数据
            int groupId = 0;
            if (context.HttpContext.Session.GetString("GroupId") != null)
            {
                groupId = Framework.Core.Transform.GetInt(context.HttpContext.Session.GetString("GroupId"), 0);
            }
            string areaName = context.HttpContext.Request.Path.Value.Contains("user") ? "user" : "";
            string controllerName = context.RouteData.Values["controller"].ToString().ToLower();
            string actionName = context.RouteData.Values["action"].ToString().ToLower();
            //白名单筛选验证
            bool FilterResult= QueryFilterName(areaName, controllerName, actionName);
            if (!FilterResult)
            {
                //如果不在白名单则进行权限验证
                bool Result = Common.Authorization.GroupPowerAuthorization(groupId, areaName, controllerName, actionName);
                if (Result == false)
                {
                    bool ajaxRequest = false;
                    var xreq = context.HttpContext.Request.Headers.ContainsKey("x-requested-with");
                    if (xreq)
                    {
                        ajaxRequest = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
                    }
                    if (!ajaxRequest)
                    {
                        string fromUrl = context.HttpContext.Request.GetAbsoluteUri();
                        fromUrl = fromUrl.Contains("register") ? "/posts" : fromUrl;
                        context.Result = new RedirectResult(string.Format("/home/message?fromurl={0}", fromUrl));
                    }
                    else
                    {
                        context.Result = new ContentResult()
                        {
                            Content = "您的请求未得到授权!",
                            StatusCode = 401
                        };
                    }
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }
        /// <summary>
        /// 过滤白名单
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        private bool QueryFilterName(string areaName,string controllerName, string actionName)
        {
            bool result = false;
            if (_filterList != null)
            {
                foreach (string[] str in _filterList)
                {
                    if (str[0] == areaName && str[1] == controllerName && str[2] == actionName)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
