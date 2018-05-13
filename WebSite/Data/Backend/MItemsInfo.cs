using System;

namespace CM.Model
{
	/// <summary>
	/// 实体类:商品基本信息表
	/// </summary>
	public class MItemsInfo
	{
		#region 表字段

        private long _ID = 0;
        /// <summary>
        /// 编号
        /// </summary>
        public long ID { set { _ID = value; } get { return _ID; } }

		private long _NumIID = 0;
		/// <summary>
		/// 商品编号
		/// </summary>
		public long NumIID{	get { return _NumIID; }	set { _NumIID = value; }}

		private string _Title = "";

		//private string _PID;
		///// <summary>
		///// 淘宝客PID
		///// </summary>
		//public string PID { set { _PID = value; } get { return _PID; } }

		/// <summary>
		/// 商品标题
		/// </summary>
		public string Title{ get { return _Title; }	set { _Title = value; }}

		private string _KeyWordStr = "";
		/// <summary>
		/// 关键词字符串
		/// </summary>
		public string KeyWordStr { get { return _KeyWordStr; } set { _KeyWordStr = value; } }

		private string _TitleSub = "";
		/// <summary>
		/// 商品副标题
		/// </summary>
		public string TitleSub{	get { return _TitleSub; } set { _TitleSub = value; }}

		private int _IsPush = 0;
		/// <summary>
		/// 是否推送过 是=1,，否=0
		/// </summary>
		public int IsPush { get { return _IsPush; } set { _IsPush = value; } }

		private DateTime _PushTime = new DateTime(2000, 1, 1);
		/// <summary>
		/// 推送时间
		/// </summary>
		public DateTime PushTime { get { return _PushTime; } set { _PushTime = value; } }

		private string _TitleDescribe = "";
		/// <summary>
		/// 标题描述
		/// </summary>
		public string TitleDescribe { get { return _TitleDescribe; } set { _TitleDescribe = value; } }

		private long _CatID = 0;
		/// <summary>
		/// 商品分类
		/// </summary>
		public long CatID { get { return _CatID; } set { _CatID = value; } }

        private int _Navigation = 0;
        /// <summary>
        /// 导航类编号：【1=服装】，【2=鞋包配饰】，【3=居家日用】，【4=食品】，【5=文体车品】，【6=美容护肤】，【7=母婴】，【8=3c数码配件】，【9=住宅家电】，【0=其他】
        /// </summary>
        public int Navigation { get { return _Navigation; } set { _Navigation = value; } }

		private string _OrderUrl = "";
		/// <summary>
		/// 商品链接
		/// </summary>
		public string OrderUrl { set { _OrderUrl = value; } get { return _OrderUrl; } }

		private string _ImgUrl = "";
		/// <summary>
		/// 商品图片
		/// </summary>
		public string ImgUrl{ get { return _ImgUrl; } set { _ImgUrl = value; }}

		private string _ImgSmall = "";
		/// <summary>
		/// 商品小图
		/// </summary>
		public string ImgSmall { get { return _ImgSmall; } set { _ImgSmall = value; } }

		private float _PriceNow = 0;
		/// <summary>
		/// 销售价
		/// </summary>
		public float PriceNow{ get { return _PriceNow; } set { _PriceNow = value; }	}

		private int _IsTmall = 0;
		/// <summary>
		/// 商品来源【0=淘宝,1=天猫】
		/// </summary>
		public int IsTmall{ get { return _IsTmall; } set { _IsTmall = value; }}

		private int _SalesCount = 0;
		/// <summary>
		/// 销量【1=优质清单】
		/// </summary>
		public int SalesCount{ get { return _SalesCount; } set { _SalesCount = value; }}

		private int _IsGood = 0;
		/// <summary>
		/// 清单
		/// </summary>
		public int IsGood{ get { return _IsGood; } set { _IsGood = value; }}

		private DateTime _CreateTime = new DateTime(2000, 1, 1);
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime{ get { return _CreateTime; } set { _CreateTime = value; }}

		private DateTime _UpdateTime = new DateTime(2000, 1, 1);
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime UpdateTime{ get { return _UpdateTime; } set { _UpdateTime = value; }}

		private int _IsEnable = 1;
		/// <summary>
		/// 使用状态
		/// </summary>
		public int IsEnable { get { return _IsEnable; } set { _IsEnable = value; } }
		
		private string _ActivityID = "";
		/// <summary>
		/// 优惠券编号字符串
		/// </summary>
		public string ActivityID { get { return _ActivityID; } set { _ActivityID = value; } }

		private DateTime _TimeStart = new DateTime(2000, 1, 1);
		/// <summary>
		/// 优惠券开始时间
		/// </summary>
		public DateTime TimeStart { get { return _TimeStart; } set { _TimeStart = value; } }

		private DateTime _TimeEnd = new DateTime(2000, 1, 1);
		/// <summary>
		/// 优惠券结束时间
		/// </summary>
		public DateTime TimeEnd { get { return _TimeEnd; } set { _TimeEnd = value; } }

		private string _ClickUrl = "";
		/// <summary>
		/// 商品优惠券推广链接【二合一】
		/// </summary>
		public string ClickUrl { get { return _ClickUrl; } set { _ClickUrl = value; } }

        private string _UrlShort = "";
        /// <summary>
        /// 二合一的短链接
        /// </summary>
        public string UrlShort { get { return _UrlShort; } set { _UrlShort = value; } }

		private int _TotalCount = 0;
		/// <summary>
		/// 优惠券总量
		/// </summary>
		public int TotalCount { get { return _TotalCount; } set { _TotalCount = value; } }

		private int _RemainCount = 0;
		/// <summary>
		/// 优惠券剩余量
		/// </summary>
		public int RemainCount { get { return _RemainCount; } set { _RemainCount = value; } }

		private float _CommissionRate = 0;
		/// <summary>
		/// 佣金比率
		/// </summary>
		public float CommissionRate { get { return _CommissionRate; } set { _CommissionRate = value; } }

		private float _Commission = 0;
		/// <summary>
		/// 佣金
		/// </summary>
		public float Commission { get { return _Commission; } set { _Commission = value; } }

		private string _CouponInfo = "";
		/// <summary>
		/// 优惠券面额信息
		/// </summary>
		public string CouponInfo { get { return _CouponInfo; } set { _CouponInfo = value; } }

		private float _CouponMoney = 0;
		/// <summary>
		/// 优惠券面额
		/// </summary>
		public float CouponMoney { get { return _CouponMoney; } set { _CouponMoney = value; } }

		private DateTime _TimeUpdate = new DateTime(2000, 1, 1);
		/// <summary>
		/// 优惠券内容更新时间
		/// </summary>
		public DateTime TimeUpdate { get { return _TimeUpdate; } set { _TimeUpdate = value; } }

		private int _CouponType = 0;
		/// <summary>
		/// 优惠券类型
		/// </summary>
		public int CouponType { get { return _CouponType; } set { _CouponType = value; } }

		private int _UseCount = 0;
		/// <summary>
		/// 使用次数
		/// </summary>
		public int UseCount { get { return _UseCount; } set { _UseCount = value; } }

		private string _Nick = "";
		/// <summary>
		/// 卖家昵称
		/// </summary>
		public string Nick { get { return _Nick; } set { _Nick = value; } }

		private long _SellerID = 0;
		/// <summary>
		/// 卖家编号
		/// </summary>
		public long SellerID { get { return _SellerID; } set { _SellerID = value; } }
		#endregion

		#region 非表字段

		private string _IsTmallCS = "";
		/// <summary>
		/// 商品来源【0=淘宝,1=天猫】
		/// </summary>
		public string IsTmallCS { get { return _IsTmallCS = _IsTmall == 0 ? "淘宝" : _IsTmall==1?"天猫":"不详"; } }

		private string _CouponTypeCS = "";
		/// <summary>
		/// 商品基本信息  优惠券类型
		/// </summary>
		public string CouponTypeCS { get { return _CouponTypeCS = _CouponType == 0 ? "通用" : _CouponType == 1 ? "定向" : _CouponType == 2 ? "营销" : _CouponType == 3 ? "鹊桥" : "其他"; } }

		private float _Price = 0;
		/// <summary>
		/// 劵后价
		/// </summary>
		public float Price { get { return _Price = _PriceNow - _CouponMoney; } }

		private string _PID = "";
		/// <summary>
		/// 默认PID
		/// </summary>
		public string PID { get { return _PID; } set { _PID = value; } }

		private DateTime _TimeStartPID = new DateTime(2000, 1, 1);
		/// <summary>
		/// 优惠券开始时间
		/// </summary>
		public DateTime TimeStartPID { get { return _TimeStartPID; } set { _TimeStartPID = value; } }

		private DateTime _TimeEndPID = new DateTime(2000, 1, 1);
		/// <summary>
		/// 优惠券结束时间
		/// </summary>
		public DateTime TimeEndPID { get { return _TimeEndPID; } set { _TimeEndPID = value; } }

		private string _ClickUrlPID = "";
		/// <summary>
		/// 商品优惠券推广链接【二合一】
		/// </summary>
		public string ClickUrlPID { get { return _ClickUrlPID; } set { _ClickUrlPID = value; } }

		private int _TotalCountPID = 0;
		/// <summary>
		/// 优惠券总量
		/// </summary>
		public int TotalCountPID { get { return _TotalCountPID; } set { _TotalCountPID = value; } }

		private int _RemainCountPID = 0;
		/// <summary>
		/// 优惠券剩余量
		/// </summary>
		public int RemainCountPID { get { return _RemainCountPID; } set { _RemainCountPID = value; } }

		private float _CommissionRatePID = 0;
		/// <summary>
		/// 佣金比率
		/// </summary>
		public float CommissionRatePID { get { return _CommissionRatePID; } set { _CommissionRatePID = value; } }

		private float _CommissionPID = 0;
		/// <summary>
		/// 佣金
		/// </summary>
		public float CommissionPID { get { return _CommissionPID; } set { _CommissionPID = value; } }

		private string _CouponInfoPID = "";
		/// <summary>
		/// 优惠券面额信息
		/// </summary>
		public string CouponInfoPID { get { return _CouponInfoPID; } set { _CouponInfoPID = value; } }

		private float _CouponMoneyPID = 0;
		/// <summary>
		/// 优惠券面额
		/// </summary>
		public float CouponMoneyPID { get { return _CouponMoneyPID; } set { _CouponMoneyPID = value; } }

		private DateTime _TimeUpdatePID = new DateTime(2000, 1, 1);
		/// <summary>
		/// 优惠券内容更新时间
		/// </summary>
		public DateTime TimeUpdatePID { get { return _TimeUpdatePID; } set { _TimeUpdatePID = value; } }

		private int _CouponTypePID = 0;
		/// <summary>
		/// 优惠券类型
		/// </summary>
		public int CouponTypePID { get { return _CouponTypePID; } set { _CouponTypePID = value; } }

		private string _TpwdPID = "";
		/// <summary>
		/// 淘口令
		/// </summary>
		public string TpwdPID { get { return _TpwdPID; } set { _TpwdPID = value; } }

		private DateTime _TpwdTimePID = new DateTime(2000, 1, 1);
		/// <summary>
		/// 淘口令生成时间
		/// </summary>
		public DateTime TpwdTimePID { get { return _TpwdTimePID; } set { _TpwdTimePID = value; } }

		private string _CouponTypeBS = "";
		/// <summary>
		/// 商品与PID对应优惠券  优惠券类型
		/// </summary>
		public string CouponTypeBS { get { return _CouponTypeBS = _CouponTypePID == 0 ? "通用" : _CouponTypePID == 1 ? "定向" : _CouponTypePID == 2 ? "营销" : _CouponTypePID == 3 ? "鹊桥" : "其他"; } }

		private string _CatName = "";
		/// <summary>
		/// 类目名称
		/// </summary>
		public string CatName { get { return _CatName; } set { _CatName = value; } }

		private float _ZKPice = 0;
		/// <summary>
		/// 折扣价
		/// </summary>
		public float ZKPice { get { return _ZKPice; } set { _ZKPice = value; } }
		#endregion
	}
}