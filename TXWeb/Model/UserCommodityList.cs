using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserCommodityList
    {
        /// <summary>
        /// 用户商品ID，主键，自增
        /// </summary>
        public int UserCommodityID { get; set; } = 0;

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; } = 0;

        /// <summary>
        /// 商品ID
        /// </summary>
        public int CommodityID { get; set; } = 0;
    }
}
