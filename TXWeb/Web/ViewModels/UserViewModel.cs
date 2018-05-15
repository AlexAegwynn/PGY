using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class UserViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 确认密码
        /// </summary>
        public string ConfirmPwd { get; set; } = string.Empty;

        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode { get; set; } = string.Empty;
    }
}