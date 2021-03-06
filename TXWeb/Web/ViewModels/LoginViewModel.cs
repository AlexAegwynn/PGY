﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class LoginViewModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; } = 0;

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

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

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; } = false;
    }
}