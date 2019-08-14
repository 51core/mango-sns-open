using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Common.SignalR
{
    public class MessageData
    {
        /// <summary>
        /// 消息ID由客户端自己生成唯一ID(GUID)
        /// </summary>
        public string MessageId { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; set; }
        /// <summary>
        /// 发送用户(0表示系统消息发送用户)
        /// </summary>
        public string SendUserId { get; set; }
        /// <summary>
        /// 发送人昵称
        /// </summary>
        public string SendUserNickName { get; set; }
        /// <summary>
        /// 发送人头像
        /// </summary>
        public string SendUserHeadUrl { get; set; }
        /// <summary>
        /// 接收用户
        /// </summary>
        public string ReceiveUserId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageBody { get; set; }
        /// <summary>
        /// 消息来源(判断来自客服消息还是用户消息)
        /// </summary>
        public MessageSource MessageSource { get; set; }
    }
}
