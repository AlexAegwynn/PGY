using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class UserViewModel
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public Model.UserList User { get; set; } = new Model.UserList();
    }
}