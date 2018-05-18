using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CommodityList
    {
        /// <summary>
        /// 商品ID，主键，自增
        /// </summary>
        public int CommodityID { get; set; } = 0;

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// 价格
        /// </summary>
        public int Price { get; set; } = 0;

        /// <summary>
        /// 同学价
        /// </summary>
        public int TxPrice { get; set; } = 0;

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; } = string.Empty;

        /// <summary>
        /// 商品描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// 用户ID【Tx_UserList】
        /// </summary>
        public int UserID { get; set; } = 0;

        /// <summary>
        /// 用户名【Tx_UserList】
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
