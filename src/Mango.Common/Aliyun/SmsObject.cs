using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Common.Aliyun
{
    public class SmsObject
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { set; get; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 模板Key
        /// </summary>
        public string TempletKey { set; get; }

        /// <summary>
        /// 短信数据
        /// </summary>
        public IDictionary<string, string> Data { set; get; }

        /// <summary>
        /// 业务ID
        /// </summary>
        public string OutId { set; get; }
    }
}
