using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 网站实体类
    /// </summary>
    public class WebSiteInfo
    {
        /// <summary>
        /// 站点主键
        /// </summary>
        public Guid WebSiteKey { get; set; } = Guid.Empty;

        /// <summary>
        /// 站点名称
        /// </summary>
        public string WebSiteName { get; set; } = string.Empty;

        /// <summary>
        /// 站点ID
        /// </summary>
        public string WebSiteID { get; set; } = string.Empty;

        /// <summary>
        /// 域名
        /// </summary>
        public string DomainName { get; set; } = string.Empty;

        /// <summary>
        /// Logo图片Url
        /// </summary>
        public string LogoImgUrl { get; set; } = string.Empty;

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// 公司地址
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; } = string.Empty;

        /// <summary>
        /// 微信
        /// </summary>
        public string WeChat { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 二维码图片Url
        /// </summary>
        public string QRCodeUrl { get; set; } = string.Empty;

        /// <summary>
        /// 备案号
        /// </summary>
        public string RecordNumber { get; set; } = string.Empty;

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keywords { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 商品类目
        /// </summary>
        public string Category { get; set; } = string.Empty;

        public string BackgroundImg { get; set; } = string.Empty;
    }
}