using System;

namespace Mango.Common.SignalR
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 心跳包消息
        /// </summary>
        Heartbeat = 0,
        /// <summary>
        /// 连接消息
        /// </summary>
        Line=1,
        /// <summary>
        /// 文字消息
        /// </summary>
        Text=2,
        /// <summary>
        /// 连接回执消息
        /// </summary>
        LineReceipt = 98,
        /// <summary>
        /// 连接通知消息
        /// </summary>
        LineNotice = 99
    }
}
