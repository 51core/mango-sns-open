using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Models
{
    public class AppManagerModel
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>

        public string AppName { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>

        public string RemarkText { get; set; }

        /// <summary>
        /// 是否已关闭
        /// </summary>

        public bool IsOpen { get; set; }

        /// <summary>
        /// 允许请求API的开放地址
        /// </summary>

        public string OpenUrl { get; set; }

        /// <summary>
        /// 允许请求API的IP地址白名单
        /// </summary>

        public string OpenIP { get; set; }

        /// <summary>
        /// APP秘钥键
        /// </summary>

        public string AppKey { get; set; }

        /// <summary>
        /// APP秘钥
        /// </summary>

        public string AppSecret { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>

        public DateTime AppendTime { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>

        public DateTime LastTime { get; set; }
    }
}
