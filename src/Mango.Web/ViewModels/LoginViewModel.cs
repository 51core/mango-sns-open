using System.ComponentModel.DataAnnotations;
namespace Mango.Web.ViewModels
{
    public class LoginViewModel
    {
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }
    }
}
