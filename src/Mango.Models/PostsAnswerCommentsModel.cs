using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Models
{
    public class PostsAnswerCommentsModel
    {
        /// <summary>
        /// 点评Id
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// 点评内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 点评时间
        /// </summary>
        public DateTime PostDate { get; set; }

        /// <summary>
        /// 点评用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 点评用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 点评用户头像
        /// </summary>
        public string HeadUrl { get; set; }

        /// <summary>
        /// 点评对象(没有则0)
        /// </summary>
        public int ToUserId { get; set; }
        /// <summary>
        /// 被回复用户昵称
        /// </summary>
        public string ToUserNickName { get; set; }
        /// <summary>
        /// 被回复用户头像
        /// </summary>
        public string ToUserHeadUrl { get; set; }

        /// <summary>
        /// 回答Id
        /// </summary>
        public int AnswerId { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// +1数
        /// </summary>
        public int Plus { get; set; }
    }
}
