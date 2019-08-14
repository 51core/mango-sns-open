using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Common.SignalR
{
    public class ConnectionUser
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadUrl { get; set; }
        /// <summary>
        /// SignalR连接ID
        /// </summary>
        public string ConnectionId { get; set; }
    }
}
