using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Common.SignalR
{
    public enum MessageSource
    {
        /// <summary>
        /// 平台消息
        /// </summary>
        PlatformMessage = 0,
        /// <summary>
        /// 用户消息
        /// </summary>
        UserMessage = 1
    }
}
