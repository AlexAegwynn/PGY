﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;
using Web.ViewModels;

namespace Web.Controllers
{
    /// <summary>
    /// 站点控制器
    /// </summary>
    public class HomeController : BaseController
    {
        #region 全局变量
        ///// <summary>
        ///// 站点信息
        ///// </summary>
        //private static Model.WebSiteInfo WebSiteInfo = null;

        public static List<vm_AccessStatistics> asList = new List<vm_AccessStatistics>();
        #endregion

        ///// <summary>
        ///// 首页
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult Index()
        //{
        //    WebSiteInfo = Logic.WebSite.GetWebSite(WebSiteID);

        //    //List<Model.ItemsInfo> list = Logic.Commodity.GetItemsInfos(Convert.ToInt32(WebSiteID), 1, "", 5);
        //    List<Model.ItemsInfo> list = Logic.Commodity.GetItemsInfos2(Convert.ToInt32(WebSiteID), 5);

        //    HomeViewModel vModel = new HomeViewModel()
        //    {
        //        ArticleList = GetArticleList().Take(6).ToList(),
        //        //PictureList = GetPictureList(),
        //        CouponsList = list
        //    };

        //    ViewData["WebSite"] = WebSiteInfo;

        //    //if (State)
        //    //{
        //    //    return View("/Views/Home/MobileView/Index.cshtml", vModel);
        //    //}

        //    return View(vModel);
        //}

        public ActionResult Index(int page = 1, string coupon = "", string search = "")
        {
            //WebSiteInfo = Logic.WebSite.GetWebSite(WebSiteID);

            ViewData["WebSite"] = Logic.WebSite.GetWebSite(WebSiteID);
            var msg = string.Empty;

            List<Model.ItemsInfo> list = Logic.Commodity.GetItemsInfos2(Convert.ToInt32(WebSiteID),0);

            if (search != "")
            {
                list = (from l in list where l.Title.ToUpper().IndexOf(search.ToUpper()) >= 0 select l).ToList();
            }

            decimal total = list.Count;
            int pageCode = Convert.ToInt32(Math.Ceiling(total / 20));

            list = list.Skip((page - 1) * 20).Take(20).ToList();

            try
            {
                string numIIDs = string.Empty;

                foreach (var item in list)
                {
                    numIIDs += item.NumIID + ",";
                }

                List<Top.Api.Domain.NTbkItem> zklist = Common.TBApi.GetZKPice(numIIDs);

                msg = "<p>" + zklist.Count.ToString() + "_" + list.Count + "</p><br /><br />" + numIIDs;

                foreach (var item in list)
                {
                    foreach (var zk in zklist)
                    {
                        if (item.NumIID == zk.NumIid)
                        {
                            item.ZKPice = zk.ZkFinalPrice;
                            item.ImgUrl = item.ImgUrl + "_220x220.jpg";
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return Content("<p>" + e.Message + "</p><br /><br />" + msg);
            }

            vm_Commodity vModel = new vm_Commodity();
            vModel.PageCount = pageCode;
            vModel.ItemsInfos = list;
            vModel.PageCode = page; //当前页数
            vModel.Search = search;

            ViewBag.Total = ASList.ASCount;

            return View("~/Views/Home/Commodity.cshtml", vModel);
        }

        ///// <summary>
        ///// 商品列表
        ///// </summary>
        ///// <param name="inPageRows">行数</param>
        ///// <param name="page">页码</param>
        ///// <param name="coupon">分类条件</param>
        ///// <param name="search">搜索条件</param>
        ///// <returns></returns>
        //public ActionResult Commodity(/*int inPageRows, */int page = 1, string coupon = "", string search = "")
        //{
        //    //if (WebSiteInfo == null)
        //    //{
        //    //    WebSiteInfo = Logic.WebSite.GetWebSite(WebSiteID);
        //    //}

        //    WebSiteInfo = Logic.WebSite.GetWebSite(WebSiteID);

        //    ViewData["WebSite"] = WebSiteInfo;

        //    List<Model.ItemsInfo> list = Logic.Commodity.GetItemsInfos2(Convert.ToInt32(WebSiteID));
        //    if (search != "")
        //    {
        //        list = (from l in list where l.Title.ToUpper().IndexOf(search.ToUpper()) >= 0 select l).ToList();
        //    }

        //    decimal total = list.Count;
        //    int pageCount = Convert.ToInt32(Math.Ceiling(total / 20));

        //    list = list.Skip((page - 1) * 20).Take(20).ToList();

        //    CommodityViewModel vModel = new CommodityViewModel();
        //    vModel.PageCount = pageCount;
        //    vModel.ItemsInfos = list;
        //    //vModel.ItemsInfos = Logic.Commodity.GetItemsInfos(Convert.ToInt32(WebSiteID), page, search);

        //    //switch (coupon)
        //    //{
        //    //    case "coupon":
        //    //        list = (from l in list where l.RemainCount > 0 select l).ToList(); break;
        //    //    case "nocoupon":
        //    //        list = (from l in list where l.RemainCount <= 0 select l).ToList(); break;
        //    //    default: break;
        //    //}

        //    //ViewBag.Count = vModel.PageCode; //总页数
        //    vModel.PageCode = page; //当前页数
        //    //ViewBag.Coupon = coupon;
        //    vModel.Search = search;

        //    //ViewData["ArticleList"] = GetArticleList().Take(6).ToList();

        //    //if (State)
        //    //{
        //    //    return View("/Views/Home/MobileView/Commodity.cshtml", vModel);
        //    //}

        //    return View(vModel);
        //}

        ///// <summary>
        ///// 文章列表页
        ///// </summary>
        ///// <param name="category">分类条件</param>
        ///// <param name="search">搜索条件</param>
        ///// <returns></returns>
        //public ActionResult Article(string category = "", string search = "")
        //{
        //    if (WebSiteInfo == null)
        //    {
        //        WebSiteInfo = Logic.WebSite.GetWebSite(WebSiteID);
        //    }

        //    ViewData["WebSite"] = WebSiteInfo;

        //    List<ArticleViewModel> list = GetArticleList();

        //    //if (list.Count == 0)
        //    //{
        //    //    list = GetArticleList();
        //    //}

        //    List<string> categoryList = (from l in list select l.DomainName).ToList();
        //    categoryList = categoryList.Distinct().ToList();

        //    if (search != "")
        //    {
        //        list = (from a in list where a.Title.Contains(search) || a.RAdminName.Contains(search) select a).ToList();
        //    }

        //    if (category != "")
        //    {
        //        list = (from a in list where a.DomainName.Contains(category) select a).ToList();
        //    }

        //    ViewData["CategoryList"] = categoryList;
        //    ViewBag.Search = search;

        //    if (State)
        //    {
        //        return View("/Views/Home/MobileView/Article.cshtml", list);
        //    }

        //    return View(list);
        //}

        public PartialViewResult ArticlePartial(int page = 1)
        {
            //if (WebSiteInfo == null)
            //{
            //    WebSiteInfo = Logic.WebSite.GetWebSite(WebSiteID);
            //}

            ViewData["WebSite"] = Logic.WebSite.GetWebSite(WebSiteID);

            vm_Article vModel = GetArticleList(page);
            ViewBag.PageCode = page;
            ViewBag.PageCount = vModel.ToPag;

            return PartialView(vModel);
        }

        /// <summary>
        /// 文章
        /// </summary>
        /// <param name="inArticleID">文章ID</param>
        /// <returns></returns>
        public ActionResult ArticleInfo(string inArticleID, int page = 1)
        {
            if (string.IsNullOrEmpty(inArticleID))
            {
                return RedirectToAction("Index");
            }
            vm_Article vModel = GetArticleList(page);

            vModel.Article = (from a in vModel.Article where a.ArticleID == inArticleID select a).ToList();
            vModel.Article[0].Conten = vModel.Article[0].Conten.Trim('\'');

            return View(vModel.Article[0]);
        }

        /// <summary>
        /// 添加访问IP
        /// </summary>
        /// <returns></returns>
        public JsonResult ASAdd(string IP = "")
        {
            vm_AccessStatistics model = new vm_AccessStatistics();

            if (asList.Any(x => x.IP == IP && x.DateTime == GetTimeByDateTime(DateTime.Now)))
            {
                return null;
            }

            model.IP = IP;
            model.WebSiteID = WebSiteID;
            model.DateTime = GetTimeByDateTime(DateTime.Now);

            asList.Add(model);

            return null;
        }

        ///// <summary>
        ///// 图集列表页
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult Pictures()
        //{
        //    if (WebSiteInfo == null)
        //    {
        //        WebSiteInfo = Logic.WebSite.GetWebSite(WebSiteID);
        //    }

        //    ViewData["WebSite"] = WebSiteInfo;

        //    List<PictureViewModel> list = GetPictureList();

        //    //if (list.Count == 0)
        //    //{
        //    //    list = GetPictureList();
        //    //}

        //    if (State)
        //    {
        //        return View("/Views/Home/MobileView/Pictures.cshtml", list);
        //    }

        //    return View(list);
        //}

        ///// <summary>
        ///// 图集
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult PictureInfo(string inPicID)
        //{
        //    if (string.IsNullOrEmpty(inPicID))
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    List<PictureViewModel> list = (from p in GetPictureList() where p.PictureID == inPicID select p).ToList();

        //    if (Request.Browser.IsMobileDevice)
        //    {
        //        return View("/Views/Home/MobileView/PictureInfo.cshtml", list[0]);
        //    }

        //    return View(list[0]);
        //}

        /// <summary>
        /// 私有方法，获取文章列表
        /// </summary>
        /// <returns></returns>
        private static vm_Article GetArticleList(int page = 1, int rows = 6)
        {
            string url = string.Format("http://121.10.200.52:54321/GetArticleWeb.ashx?WID={0}&&pizetop={1}&&pizenum={2}", 5, page, rows);

            var data = new WebClient().DownloadData(url);
            var jsonData = Encoding.UTF8.GetString(data);

            vm_Article vModel = new vm_Article();

            if (jsonData != "{}")
            {
                vModel = JsonConvert.DeserializeObject<vm_Article>(jsonData);
                foreach (var item in vModel.Article)
                {
                    item.ArticleID = vModel.Article.IndexOf(item).ToString();
                }
            }

            return vModel;
        }

        ///// <summary>
        ///// 私有方法，获取图片列表
        ///// </summary>
        ///// <returns></returns>
        //private static List<PictureViewModel> GetPictureList()
        //{
        //    string url = string.Format("http://121.10.200.52:54321/GetAtlasWeb.ashx?WID={0}", WebSiteID);

        //    var data = new WebClient().DownloadData(url);
        //    var jsonData = Encoding.UTF8.GetString(data);
        //    List<PictureViewModel> list = new List<PictureViewModel>();

        //    if (jsonData != "{}")
        //    {
        //        list = JsonConvert.DeserializeObject<List<PictureViewModel>>(jsonData);
        //        foreach (var item in list)
        //        {
        //            item.PictureID = list.IndexOf(item).ToString();
        //        }
        //    }

        //    return list;
        //}

        ///// <summary>
        ///// 私有方法，获取商品列表
        ///// </summary>
        ///// <param name="rows">每页商品数</param>
        ///// <param name="page">页码</param>
        ///// <param name="search"></param>
        ///// <returns></returns>
        //private static CommodityViewModel GetCommodityList(int rows = 30, int page = 1, string search = "")
        //{
        //    string str = "NumIID,Title,TitleSub,UrlShort,PriceNow,SalesCount,ClickUrl,CommissionRate,CouponMoney," +
        //                                                "IsTmallCS,ImgUrl,ImgSmall,CatID,RemainCount,TotalCount,CouponInfo,IsEnable,TimeStart,TimeEnd,ZKPice";

        //    string url = string.Format("http://121.10.200.52:54321/ItemWeb.ashx?WID={0}&&str={1}&&pizetop={2}&&pizenum={3}", WebSiteID, str, page, rows);

        //    var data = new WebClient().DownloadData(url);
        //    var jsonData = Encoding.UTF8.GetString(data);
        //    CommodityViewModel model = new CommodityViewModel();
        //    model.ItemInfo = new List<Commodity>();

        //    if (jsonData != "{}")
        //    {
        //        model = JsonConvert.DeserializeObject<CommodityViewModel>(jsonData);
        //    }

        //    return model;
        //}

        /// <summary>
        /// DateTime To Long
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static long GetTimeByDateTime(DateTime dateTime)
        {
            long lTime = 0;
            dateTime = Convert.ToDateTime(dateTime.ToString("yyyy-MM-dd") + " 00:00:00");
            return lTime = (dateTime.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        /// <summary>
        /// Long To DateTime
        /// </summary>
        /// <param name="longTime"></param>
        /// <returns></returns>
        private static DateTime GetDateTimeByTime(long longTime)
        {
            DateTime dateTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            longTime = long.Parse(longTime.ToString().PadRight(17, '0'));
            TimeSpan toNow = new TimeSpan(longTime);
            return dateTime.Add(toNow);
        }
    }
}