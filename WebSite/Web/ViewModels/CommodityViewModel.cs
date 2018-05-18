using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class CommodityViewModel
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCode { get; set; }

        /// <summary>
        /// 商品列表
        /// </summary>
        public List<Model.ItemsInfo> ItemsInfos { get; set; }
    }

    //public class Commodity
    //{
    //    /// <summary>
    //    /// 商品编号
    //    /// </summary>
    //    public string NumIID { get; set; }

    //    /// <summary>
    //    /// 商品标题
    //    /// </summary>
    //    public string Title { get; set; }

    //    /// <summary>
    //    /// 商品副标题
    //    /// </summary>
    //    public string TitleSub { get; set; }

    //    /// <summary>
    //    /// 短链接
    //    /// </summary>
    //    public string UrlShort { get; set; }

    //    /// <summary>
    //    /// 销售价
    //    /// </summary>
    //    public decimal PriceNow { get; set; }

    //    /// <summary>
    //    /// 销量
    //    /// </summary>
    //    public int SalesCount { get; set; }

    //    /// <summary>
    //    /// 商品优惠券推广链接
    //    /// </summary>
    //    public string ClickUrl { get; set; }

    //    /// <summary>
    //    /// 佣金比率
    //    /// </summary>
    //    public string CommissionRate { get; set; }

    //    /// <summary>
    //    /// 优惠券面额
    //    /// </summary>
    //    public decimal CouponMoney { get; set; }

    //    /// <summary>
    //    /// 商品来源
    //    /// </summary>
    //    public string IsTmallCS { get; set; }

    //    /// <summary>
    //    /// 商品图片
    //    /// </summary>
    //    public string ImgUrl { get; set; }

    //    /// <summary>
    //    /// 商品小图
    //    /// </summary>
    //    public string ImgSmall { get; set; }

    //    /// <summary>
    //    /// 商品分类
    //    /// </summary>
    //    public string CatID { get; set; }

    //    /// <summary>
    //    /// 优惠券总量
    //    /// </summary>
    //    public int TotalCount { get; set; }

    //    /// <summary>
    //    /// 优惠券剩余量
    //    /// </summary>
    //    public int RemainCount { get; set; }

    //    /// <summary>
    //    /// 优惠券面额信息
    //    /// </summary>
    //    public string CouponInfo { get; set; }

    //    /// <summary>
    //    /// 使用状态，1 = 正常，0 = 停用
    //    /// </summary>
    //    public int IsEnable { get; set; }

    //    /// <summary>
    //    /// 优惠券开始时间
    //    /// </summary>
    //    public DateTime TimeStart { get; set; }

    //    /// <summary>
    //    /// 优惠券结束时间
    //    /// </summary>
    //    public DateTime TimeEnd { get; set; }

    //    /// <summary>
    //    /// 折扣价
    //    /// </summary>
    //    public decimal ZKPice { get; set; }
    //}
}