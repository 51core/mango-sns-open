using System.ComponentModel.DataAnnotations;
namespace Mango.Web.ViewModels
{
    public class RegisterViewModel
    {

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 邮箱注册验证码
        /// </summary>
        public string ValidateCode { get; set; }
    }
}
