using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Models
{
    public class UPYunModel
    {
        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 参数策略
        /// </summary>
        public string Policy { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string Expiration { get; set; }
    }
}
