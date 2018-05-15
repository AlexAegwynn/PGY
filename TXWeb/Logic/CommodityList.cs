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
        /// 获取商品信息
        /// </summary>
        /// <param name="inCommodityID"></param>
        /// <returns></returns>
        public static Model.CommodityList GetCommodity(int inCommodityID)
        {
            return Data.CommodityList.GetCommodity(inCommodityID);
        }
    }
}
