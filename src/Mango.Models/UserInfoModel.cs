using System;

namespace Mango.Models
{
    public class UserInfoModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 所属用户组
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadUrl { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime LastLoginDate { get; set; }

        /// <summary>
        /// 最后登陆IP
        /// </summary>
        public string LastLoginIP { get; set; }

        /// <summary>
        /// 注册IP地址
        /// </summary>
        public string RegisterIP { get; set; }

        /// <summary>
        /// 用户状态(true:正常,false:禁止)
        /// </summary>
        public bool IsStatus { get; set; }

        /// <summary>
        /// 所在地
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 用户标签
        /// </summary>
        public string Tags { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
    }
}
