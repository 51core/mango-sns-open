using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using System;
using System.Collections.Generic;

namespace Mango.Manager.Extensions
{
    public class AuthorizationFilter:IActionFilter
    {
        /// <summary>
        /// 过滤对象列表
        /// </summary>
        private static List<string[]> _filterList { get; set; }
        public AuthorizationFilter()
        {
            if (_filterList == null)
            {
                _filterList = new List<string[]>();
                _filterList.Add(new string[] { "home", "login" });
                _filterList.Add(new string[] { "home", "loginout" });
                _filterList.Add(new string[] { "authorization", "validatecode" });
            }
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Action请求执行前处理事件
            //获取权限验证所需数据
            try
            {
                int roleId = 0;
                if (context.HttpContext.Session.GetString("RoleId") != null)
                {
                    roleId = Framework.Core.Transform.GetInt(context.HttpContext.Session.GetString("RoleId"), 0);
                }
                else
                {
                    string controllerName = context.RouteData.Values["controller"].ToString().ToLower();
                    string actionName = context.RouteData.Values["action"].ToString().ToLower();
                    if (!QueryFilterName(controllerName, actionName))
                    {
                        context.Result = new RedirectResult("/Home/Login");
                    }
                }
            }
            catch (Exception ex)
            {
                ContentResult contentResult = new ContentResult();
                contentResult.Content = ex.Message;
                contentResult.StatusCode = 500;
                context.Result = contentResult;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Action请求执行后处理事件
        }
        
        /// <summary>
        /// 过滤白名单
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        private bool QueryFilterName(string controllerName,string actionName)
        {
            bool result = false;
            if (_filterList != null)
            {
                foreach (string[] str in _filterList)
                {
                    if (str[0] == controllerName && str[1] == actionName)
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
