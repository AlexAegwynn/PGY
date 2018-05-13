using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
	public class Commodity
	{
        /// <summary>
        /// 获取商品列表，inWebID 网站ID， page 页码， inSearch 搜索关键字，rows 每页商品数
        /// </summary>
        /// <returns></returns>
        public static List<Model.ItemsInfo> GetItemsInfos(int inWebID, int page = 1, string inSearch = "", int rows = 30)
        {
            page = (page - 1) * rows;
            return Data.Commodity.GetItemsInfos(rows, page, inWebID, inSearch);
        }

        /// <summary>
        /// 获取商品总数
        /// </summary>
        /// <param name="inWebSiteID"></param>
        /// <param name="inSearch"></param>
        /// <returns></returns>
        public static int GetTotal(int inWebSiteID, string inSearch = "")
        {
            return Data.Commodity.GetTotal(inWebSiteID, inSearch);
        }
    }
}