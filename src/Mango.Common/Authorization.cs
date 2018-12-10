using System;
using System.Collections.Generic;
using System.Text;
using Mango.Repository;
using Mango.Framework.Services;
using Mango.Framework.Services.Cache;
using Newtonsoft.Json;
namespace Mango.Common
{
    public class Authorization
    {

        /// <summary>
        /// 用户组授权验证
        /// </summary>
        /// <param name="groupID">用户组ID</param>
        /// <param name="areaName">区域名称</param>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="actionName">Action名称</param>
        /// <returns></returns>
        public static bool GroupPowerAuthorization(int groupID,string areaName,string controllerName,string actionName)
        {
            bool Result = false;
            //加载权限数据
            List<Models.UserGroupPowerModel> list = GetPowerData(groupID);
            if (list != null)
            {
                //开始权限验证
                foreach (Models.UserGroupPowerModel m in list)
                {
                    if (m.GroupId == groupID && m.AreaName == areaName && m.ControllerName == controllerName && m.ActionName == actionName)
                    {
                        //当匹配到权限时返回正确结果并且跳出循环
                        Result = true;
                        break;
                    }
                }
            }
            return Result;
        }
        /// <summary>
        /// 根据用户组获取权限
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        private static List<Models.UserGroupPowerModel> GetPowerData(int groupID)
        {
            List<Models.UserGroupPowerModel> Result = null;
            ICacheService CacheService = ServiceContext.GetService<ICacheService>();
            string PowerCache = CacheService.Get(string.Format("UserGroupPowerCache{0}", groupID));
            if (string.IsNullOrEmpty(PowerCache))
            {
                AuthorizationRepository repository = new AuthorizationRepository();
                Result = repository.GetPowerData(groupID);
                PowerCache = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Result)));
                //写入缓存
                CacheService.Add(string.Format("UserGroupPowerCache{0}", groupID), PowerCache);
            }
            else
            {
                PowerCache = Encoding.UTF8.GetString(Convert.FromBase64String(PowerCache.Replace("\"", "")));
                //从缓存中获取
                Result = JsonConvert.DeserializeObject<List<Models.UserGroupPowerModel>>(PowerCache);
            }
            return Result;
        }
    }
}
