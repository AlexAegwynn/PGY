using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserList
    {
        /// <summary>
        /// 用户ID，主键，自增
        /// </summary>
        public int UserID { get; set; } = 0;

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 班级
        /// </summary>
        public string Class { get; set; } = string.Empty;

        /// <summary>
        /// 性别， 0为男性，1为女性，默认0
        /// </summary>
        public int Sex { get; set; } = 0;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// 微信
        /// </summary>
        public string WeChat { get; set; } = string.Empty;

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; } = string.Empty;

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 企业信息
        /// </summary>
        public string EnterpriseInfo { get; set; } = string.Empty;

        /// <summary>
        /// 是否管理员，0为普通用户，1为管理员，默认0
        /// </summary>
        public int IsAdmin { get; set; } = 0;
    }
}