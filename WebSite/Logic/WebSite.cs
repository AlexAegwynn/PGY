using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    /// <summary>
    /// 网站逻辑类
    /// </summary>
    public class WebSite
    {
        /// <summary>
        /// 根据网站ID获取网站实体
        /// </summary>
        /// <param name="inWebSiteID">网站ID</param>
        /// <returns></returns>
        public static Model.WebSiteInfo GetWebSite(string inWebSiteID)
        {
            return Data.WebSite.GetWebSite(inWebSiteID);
        }

        /// <summary>
        /// 根据站点Key获取网站实体
        /// </summary>
        /// <param name="inWebSiteID">站点Key</param>
        /// <returns></returns>
        public static Model.WebSiteInfo GetWebSiteByKey(Guid inKey)
        {
            return Data.WebSite.GetWebSiteByKey(inKey);
        }

        /// <summary>
        /// 获取站点列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.WebSiteInfo> GetWebSiteList()
        {
            return Data.WebSite.GetWebSiteList();
        }

        /// <summary>
        /// 根据站点Key获取图片地址
        /// </summary>
        /// <param name="inKey">站点主键</param>
        /// <returns></returns>
        public static List<string> GetImgList(Guid inKey)
        {
            return Data.WebSite.GetImgList(inKey);
        }

        /// <summary>
        /// 添加站点
        /// </summary>
        /// <param name="inModel">网站实体</param>
        /// <returns></returns>
        public static int AddWebSite(Model.WebSiteInfo inModel)
        {
            return Data.WebSite.AddWebSite(inModel);
        }

        /// <summary>
        /// 更新站点
        /// </summary>
        /// <param name="inModel">网站实体</param>
        /// <returns></returns>
        public static int UpdateWebSite(Model.WebSiteInfo inModel)
        {
            return Data.WebSite.UpdateWebSite(inModel);
        }

        /// <summary>
        /// 根据站点Key删除站点
        /// </summary>
        /// <param name="inKey"></param>
        /// <returns></returns>
        public static int DeleteWebSite(Guid inKey)
        {
            return Data.WebSite.DeleteWebSite(inKey);
        }

        /// <summary>
        /// 保存Logo
        /// </summary>
        /// <param name="inLogoImgUrl"></param>
        /// <param name="inWebSiteID"></param>
        /// <returns></returns>
        public static int SaveLogo(string inLogoImgUrl, Guid inKey)
        {
            return Data.WebSite.SaveLogo(inLogoImgUrl, inKey);
        }

        /// <summary>
        /// 删除Logo
        /// </summary>
        /// <param name="inWebSiteID"></param>
        /// <returns></returns>
        public static int DeleteLogo(Guid inKey)
        {
            return Data.WebSite.DeleteLogo(inKey);
        }

        /// <summary>
        /// 保存二维码
        /// </summary>
        /// <param name="inQRCodeUrl"></param>
        /// <param name="inWebSiteID"></param>
        /// <returns></returns>
        public static int SaveQRCode(string inQRCodeUrl, Guid inKey)
        {
            return Data.WebSite.SaveQRCode(inQRCodeUrl, inKey);
        }

        /// <summary>
        /// 删除二维码
        /// </summary>
        /// <returns></returns>
        public static int DeleteQRCode(Guid inKey)
        {
            return Data.WebSite.DeleteQRCode(inKey);
        }

        /// <summary>
        /// 保存背景图片
        /// </summary>
        /// <param name="inBackgroundImg"></param>
        /// <param name="inKey"></param>
        /// <returns></returns>
        public static int SaveBackgroundImg(string inBackgroundImg, Guid inKey)
        {
            return Data.WebSite.SaveBackgroundImg(inBackgroundImg, inKey);
        }

        /// <summary>
        /// 删除背景图片
        /// </summary>
        /// <param name="inKey"></param>
        /// <returns></returns>
        public static int DeleteBackgroundImg(Guid inKey)
        {
            return Data.WebSite.DeleteBackgroundImg(inKey);
        }
    }
}