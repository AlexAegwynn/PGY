using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class CommodityList
    {
        /// <summary>
        /// 获取所有商品列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.CommodityList> GetCommodityList()
        {
            return Data.CommodityList.GetCommodityList();
        }

        /// <summary>
        /// 获取用户商品列表
        /// </summary>
        /// <param name="inUserID"></param>
        /// <returns></returns>
        public static List<Model.CommodityList> GetUserCommodityList(int inUserID)
        {
            return Data.CommodityList.GetUserCommodityList(inUserID);
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="inCommodityID"></param>
        /// <returns></returns>
        public static Model.CommodityList GetCommodity(int inCommodityID)
        {
            return Data.CommodityList.GetCommodity(inCommodityID);
        }

        /// <summary>
        /// 根据商品ID获取图片路径
        /// </summary>
        /// <param name="inCommodityID"></param>
        /// <returns></returns>
        public static string GetImgUrl(int inCommodityID)
        {
            return Data.CommodityList.GetImgUrl(inCommodityID);
        }

        /// <summary>
        /// 创建商品
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int CreateCommodity(Model.CommodityList inModel)
        {
            return Data.CommodityList.CreateCommodity(inModel);
        }

        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int UpdateCommodity(Model.CommodityList inModel)
        {
            return Data.CommodityList.UpdateCommodity(inModel);
        }
    }
}
