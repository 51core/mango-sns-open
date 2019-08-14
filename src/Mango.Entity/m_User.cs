using System;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Mango.Entity
{
    public partial class m_User
    {
		
        /// <summary>
        /// 用户Id
        /// </summary>
        [Key]
        public int? UserId { get; set; }
		
        /// <summary>
        /// 用户账号名
        /// </summary>
        
        public string AccountName { get; set; }
		
        /// <summary>
        /// 登陆密码
        /// </summary>
        
        public string Password { get; set; }
		
        /// <summary>
        /// 昵称
        /// </summary>
        
        public string NickName { get; set; }
		
        /// <summary>
        /// 注册时间
        /// </summary>
        
        public DateTime? RegisterDate { get; set; }
		
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        
        public DateTime? LastLoginDate { get; set; }
		
        /// <summary>
        /// 最后登陆IP
        /// </summary>
        
        public string LastLoginIP { get; set; }
		
        /// <summary>
        /// 注册IP地址
        /// </summary>
        
        public string RegisterIP { get; set; }
		
        /// <summary>
        /// 用户状态(true:正常
        /// </summary>
        
        public bool? IsStatus { get; set; }
		
        /// <summary>
        /// 用户头像地址
        /// </summary>
        
        public string HeadUrl { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public int? GroupId { get; set; }
		
        /// <summary>
        /// 手机号
        /// </summary>
        
        public string Phone { get; set; }
		
        /// <summary>
        /// 开放平台Id
        /// </summary>
        
        public string OpenId { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        
        public string Email { get; set; }
		
        /// <summary>
        /// 地区信息
        /// </summary>
        
        public string AddressInfo { get; set; }
		
        /// <summary>
        /// 生日
        /// </summary>
        
        public string Birthday { get; set; }
		
        /// <summary>
        /// 个人标签
        /// </summary>
        
        public string Tags { get; set; }
		
        /// <summary>
        /// 性别
        /// </summary>
        
        public string Sex { get; set; }
		
    }
}