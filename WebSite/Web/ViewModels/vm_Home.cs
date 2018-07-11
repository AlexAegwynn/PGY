using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class vm_Home
    {
        ///// <summary>
        ///// 商品列表
        ///// </summary>
        //public CommodityViewModel Commodity { get; set; }

        /// <summary>
        /// 有优惠券商品列表
        /// </summary>
        public List<Model.ItemsInfo> CouponsList { get; set; }

        /// <summary>
        /// 文章列表
        /// </summary>
        public List<vm_Article> ArticleList { get; set; }

        /// <summary>
        /// 图集列表
        /// </summary>
        public List<vm_Picture> PictureList { get; set; }
    }
}