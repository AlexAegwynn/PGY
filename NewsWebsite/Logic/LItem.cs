using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LItem
    {
        /// <summary>
        /// 根据类目ID获取商品列表
        /// </summary>
        /// <param name="catID"></param>
        /// <returns></returns>
        public static List<Model.MItem> GetItemsByCatID(long catID)
        {
            return Data.DItem.GetItemsByCatID(catID);
        }
    }
}
