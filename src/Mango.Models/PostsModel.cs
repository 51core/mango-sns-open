using System;

namespace Mango.Models
{
    public class PostsModel
    {
        /// <summary>
        /// 帖子Id
        /// </summary>
        public int PostsId { get; set; }
        /// <summary>
        /// 帖子标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 帖子内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PostDate { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastDate { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// +1数
        /// </summary>
        public int PlusCount { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ReadCount { get; set; }

        /// <summary>
        /// 属性(多个属性用逗号分开)
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 所属频道
        /// </summary>

        public int ChannelId { get; set; }
        /// <summary>
        /// 所属频道名称
        /// </summary>

        public string ChannelName{ get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>
        /// 回复数
        /// </summary>
        public int AnswerCount { get; set; }
        /// <summary>
        /// 是否允许回复
        /// </summary>
        public bool IsReply { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadUrl { get; set; }

        /// <summary>
        /// 工作
        /// </summary>
        public string Jobs { get; set; }
        /// <summary>
        /// 所在地
        /// </summary>
        public string Address { get; set; }
    }
}
