using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ItemsInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long ID { set; get; } = 0;
        /// <summary>
        /// 商品编号
        /// </summary>
        public long NumIID { get; set; } = 0;        
        //private string _PID;
        ///// <summary>
        ///// 淘宝客PID
        ///// </summary>
        //public string PID { set { _PID = value; } get { return _PID; } }        
        /// <summary>
        /// 商品标题
        /// </summary>
        public string Title { get; set; } = "";
        /// <summary>
        /// 关键词字符串
        /// </summary>
        public string KeyWordStr { get; set; } = "";
        /// <summary>
        /// 商品副标题
        /// </summary>
        public string TitleSub { get; set; } = "";
        /// <summary>
        /// 是否推送过 是=1,，否=0
        /// </summary>
        public int IsPush { get; set; } = 0;
        /// <summary>
        /// 推送时间
        /// </summary>
        public long PushTime { get; set; } = 20000101;
        /// <summary>
        /// 标题描述
        /// </summary>
        public string TitleDescribe { get; set; } = "";
        /// <summary>
        /// 商品分类
        /// </summary>
        public long CatID { get; set; } = 0;
        /// <summary>
        /// 导航类编号：【1=服装】，【2=鞋包配饰】，【3=居家日用】，【4=食品】，【5=文体车品】，【6=美容护肤】，【7=母婴】，【8=3c数码配件】，【9=住宅家电】，【0=其他】
        /// </summary>
        public int Navigation { get; set; } = 0;
        ///// <summary>
        ///// 商品链接
        ///// </summary>
        //public string OrderUrl { set; get; } = "";
        /// <summary>
        /// 商品图片
        /// </summary>
        public string ImgUrl { get; set; } = "";
        /// <summary>
        /// 商品小图
        /// </summary>
        public string ImgSmall { get; set; } = "";
        /// <summary>
        /// 销售价
        /// </summary>
        public float PriceNow { get; set; } = 0;
        /// <summary>
        /// 商品来源【0=淘宝,1=天猫】
        /// </summary>
        public int IsTmall { get; set; } = 0;
        /// <summary>
        /// 销量【1=优质清单】
        /// </summary>
        public int SalesCount { get; set; } = 0;
        /// <summary>
        /// 清单
        /// </summary>
        public int IsGood { get; set; } = 0;
        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; } = 20000101;
        /// <summary>
        /// 更新时间
        /// </summary>
        public long UpdateTime { get; set; } = 20000101;
        /// <summary>
        /// 使用状态
        /// </summary>
        public int IsEnable { get; set; } = 1;
        /// <summary>
        /// 优惠券编号字符串
        /// </summary>
        public string ActivityID { get; set; } = "";
        /// <summary>
        /// 优惠券开始时间
        /// </summary>
        public long TimeStart { get; set; } = 20000101;
        /// <summary>
        /// 优惠券结束时间
        /// </summary>
        public long TimeEnd { get; set; } = 20000101;
        /// <summary>
        /// 商品优惠券推广链接【二合一】
        /// </summary>
        public string ClickUrl { get; set; } = "";
        /// <summary>
        /// 二合一的短链接
        /// </summary>
        public string UrlShort { get; set; } = "";
        /// <summary>
        /// 优惠券总量
        /// </summary>
        public int TotalCount { get; set; } = 0;
        /// <summary>
        /// 优惠券剩余量
        /// </summary>
        public int RemainCount { get; set; } = 0;
        /// <summary>
        /// 佣金比率
        /// </summary>
        public float CommissionRate { get; set; } = 0;
        /// <summary>
        /// 佣金
        /// </summary>
        public float Commission { get; set; } = 0;
        /// <summary>
        /// 优惠券面额信息
        /// </summary>
        public string CouponInfo { get; set; } = "";
        /// <summary>
        /// 优惠券面额
        /// </summary>
        public float CouponMoney { get; set; } = 0;
        /// <summary>
        /// 优惠券内容更新时间
        /// </summary>
        public long TimeUpdate { get; set; } = 20000101;
        /// <summary>
        /// 优惠券类型
        /// </summary>
        public int CouponType { get; set; } = 0;
        /// <summary>
        /// 使用次数
        /// </summary>
        public int UseCount { get; set; } = 0;
        /// <summary>
        /// 卖家昵称
        /// </summary>
        public string Nick { get; set; } = "";
        /// <summary>
        /// 卖家编号
        /// </summary>
        public long SellerID { get; set; } = 0;
        /// <summary>
        /// 折扣价【非表字段】
        /// </summary>
        public string ZKPice { get; set; } = "";

        /// <summary>
        /// 商品评价【Web_commentWeb 表字段】
        /// </summary>
        public string CommentStr { get; set; } = "";
    }
}
