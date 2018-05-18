using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class CommodityViewModel
    {
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<Model.CommodityList> CommodityList { get; set; } = new List<Model.CommodityList>();

        /// <summary>
        /// 页码
        /// </summary>
        public int PageCode { get; set; } = 1;

        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount { get; set; } = 0;
    }
}