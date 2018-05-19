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

        /// <summary>
        /// 价格区间，小
        /// </summary>
        public string SPrice { get; set; } = string.Empty;

        /// <summary>
        /// 价格区间，大
        /// </summary>
        public string HPrice { get; set; } = string.Empty;

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Search { get; set; } = string.Empty;

        /// <summary>
        /// 分类
        /// </summary>
        public string CatID { get; set; } = string.Empty;

        public List<Catagory> CateList { get; set; } = new List<Catagory>();
    }

    public class Catagory
    {
        public int CategoryID { get; set; } = 0;

        public string CategoryStr { get; set; } = string.Empty;
    }
}