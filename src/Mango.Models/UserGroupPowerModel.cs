using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Models
{
    public class UserGroupPowerModel
    {
        /// <summary>
        /// 所属用户组
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// 权限菜单Id
        /// </summary>
        public int MId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string MName { get; set; }

        /// <summary>
        /// 区域名
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// Action名称
        /// </summary>
        public string ActionName { get; set; }
    }
}
