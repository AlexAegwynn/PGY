using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TB.HttpAPI;
using Top.Api;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        ///// <summary>
        ///// 所有文章列表
        ///// </summary>
        //private static List<Model.MContent> articleList = new List<Model.MContent>();
        ///// <summary>
        ///// 页面文章列表
        ///// </summary>
        //private static List<Model.MContent> aList = new List<Model.MContent>();
        private static int categoryId = 0;
        private static string searchStr = string.Empty;

        public ActionResult Index(string search = "", int catid = 0)
        {
            List<Model.MContent> articleList = LContent.GetRandomArticles(10, 0);
            categoryId = catid;
            searchStr = search;

            var list = (from l in articleList where l.Conten.Contains("<img src=") orderby Guid.NewGuid() select l).Take(6).ToList();
            List<ViewModels.VMArticle> vList = GetVmList(list);

            ViewData["ShowList"] = vList;  //首页图片轮播

            var ywList = (from l in articleList where l.Conten.Contains("<img src=") orderby l.ReleaseTime descending select l).Take(4).ToList();
            List<ViewModels.VMArticle> vywList = GetVmList(ywList);

            ViewData["NewList"] = vywList;  //最新要闻

            decimal total = LContent.GetArticleTotal();
            int pcount = Convert.ToInt32(Math.Ceiling(total / 10));  //页数

            ViewBag.PageCount = pcount;

            ViewBag.Search = search;
            ViewBag.CatID = catid;

            ViewModels.VMUser vUser = Session["LoginUser"] as ViewModels.VMUser;  //获取session

            ViewBag.IsLogin = vUser == null;
            ViewBag.Name = vUser == null ? "" : vUser.UserName;

            return View();
        }

        public PartialViewResult ArticleList(int page = 0)
        {
            List<Model.MContent> list = LContent.GetArticles(page, categoryId, searchStr);
            List<ViewModels.VMArticle> vList = GetVmList(list);

            return PartialView(vList);
        }

        public ActionResult ArticleInfo(long inArticleID)
        {
            Model.MContent model = LContent.GetArticle(inArticleID);
            ViewModels.VMArticle vModel = new ViewModels.VMArticle();

            if (model != null)
            {
                vModel.ArticleID = model.ArticleID;
                vModel.Title = model.Title;
                vModel.DomainID = model.DomainID;
                vModel.ReleaseTime = ConvertLongToDateTime(model.ReleaseTime).ToShortDateString();
                vModel.Conten = model.Conten;

                ViewBag.IsLogin = false;
                ViewBag.Name = string.Empty;

                ViewBag.CatName = GetCatName(vModel.DomainID);

                //获取session,判断是否为空
                if (Session["LoginUser"] is ViewModels.VMUser vUser)
                {
                    vModel.IsFavorites = LFavorites.ExistFavorites(vModel.ArticleID, vUser.UID);

                    Model.MFootmarks footmarks = new Model.MFootmarks
                    {
                        UID = vUser.UID,
                        ArticleID = vModel.ArticleID,
                        MarkTime = DateTime.Now,
                        FmTitle = vModel.Title
                    };

                    string fmID = LFootmarks.ExistFootmark(vUser.UID, vModel.ArticleID);
                    if (string.IsNullOrEmpty(fmID))
                    {
                        LFootmarks.CreateFootmark(footmarks);
                    }
                    else
                    {
                        footmarks.FmID = Convert.ToInt32(fmID);
                        LFootmarks.UpdateFootmark(footmarks);
                    }

                    ViewBag.IsLogin = true;
                    ViewBag.Name = vUser.UserName;
                }
            }

            #region 推荐文章&商品
            var raList = LContent.GetRandomArticles(3, vModel.DomainID);
            List<ViewModels.VMArticle> vRaList = new List<ViewModels.VMArticle>();
            foreach (var item in raList)
            {
                ViewModels.VMArticle ar = new ViewModels.VMArticle
                {
                    ArticleID = item.ArticleID,
                    Title = item.Title,
                    DomainID = item.DomainID,
                    ReleaseTime = ConvertLongToDateTime(item.ReleaseTime).ToShortDateString(),
                    ImgSrc = GetSrc(item.Conten)
                };
                vRaList.Add(ar);
            }
            ViewData["RelatedArticle"] = vRaList;
            ViewData["Items"] = TuiJian(vModel.Title);
            #endregion

            return View(vModel);
        }

        public ActionResult FootmarkList(string search = "")
        {
            List<ViewModels.VMFootmark> vList = new List<ViewModels.VMFootmark>();
            ViewBag.Name = string.Empty;
            ViewBag.IsLogin = false;
            ViewBag.Search = search;

            if (Session["LoginUser"] is ViewModels.VMUser vUser)
            {
                var list = string.IsNullOrEmpty(search) ? LFootmarks.GetFootmarks(vUser.UID) : LFootmarks.SearchMarks(search, vUser.UID);

                foreach (var item in list)
                {
                    ViewModels.VMFootmark vModel = new ViewModels.VMFootmark
                    {
                        FmID = item.FmID,
                        ArticleID = item.ArticleID,
                        MarkTime = item.MarkTime.ToShortDateString(),
                        FmTitle = item.FmTitle
                    };
                    vList.Add(vModel);
                }

                ViewBag.Name = vUser.UserName;
                ViewBag.IsLogin = true;
            }

            return View(vList);
        }

        public JsonResult DeleteFootmark(int fmid)
        {
            JsonResult json = new JsonResult();

            bool result = LFootmarks.DeleteFootmark(fmid) > 0;

            json.Data = new { result };

            return json;
        }

        public ActionResult FavoritesList(string search = "")
        {
            List<ViewModels.VMFavorites> vList = new List<ViewModels.VMFavorites>();
            ViewBag.Name = string.Empty;
            ViewBag.IsLogin = false;
            ViewBag.Search = search;

            if (Session["LoginUser"] is ViewModels.VMUser vUser)
            {
                var list = string.IsNullOrEmpty(search) ? LFavorites.GetFavorites(vUser.UID) : LFavorites.SearchFav(search, vUser.UID);

                foreach (var item in list)
                {
                    ViewModels.VMFavorites vModel = new ViewModels.VMFavorites
                    {
                        FaID = item.FaID,
                        ArticleID = item.ArticleID,
                        FavoritesTime = item.FavoritesTime.ToShortDateString(),
                        FaTitle = item.FaTitle
                    };
                    vList.Add(vModel);
                }

                ViewBag.Name = vUser.UserName;
                ViewBag.IsLogin = true;
            }

            return View(vList);
        }

        public JsonResult AddFavorites(string inArticleID, string inTitle)
        {
            JsonResult json = new JsonResult();

            try
            {
                if (string.IsNullOrEmpty(inArticleID))
                {
                    json.Data = new { result = false, msg = "收藏失败" };
                    return json;
                }

                if (Session["LoginUser"] is ViewModels.VMUser vUser)
                {
                    Model.MFavorites model = new Model.MFavorites
                    {
                        ArticleID = Convert.ToInt64(inArticleID),
                        UID = vUser.UID,
                        FaTitle = inTitle,
                        FavoritesTime = DateTime.Now
                    };

                    bool result = LFavorites.CreateFavorite(model) > 0;
                    string msg = result ? "" : "收藏失败";

                    json.Data = new { result, msg };
                }
                else
                {
                    json.Data = new { result = false, msg = "notlogin" };
                }
            }
            catch (Exception ex)
            {
                json.Data = new { result = false, msg = "收藏失败" };
            }

            return json;
        }

        public JsonResult DelFavorites(string inArticleID)
        {
            JsonResult json = new JsonResult();

            if (string.IsNullOrEmpty(inArticleID))
            {
                json.Data = new { result = false, msg = "取消收藏失败" };
                return json;
            }

            try
            {
                if (Session["LoginUser"] is ViewModels.VMUser vUser)
                {
                    bool result = LFavorites.DeleteFavorite(Convert.ToInt64(inArticleID), vUser.UID) > 0;
                    string msg = result ? "" : "取消收藏失败";

                    json.Data = new { result, msg };
                }
            }
            catch (Exception ex)
            {
                json.Data = new { result = false, msg = "取消收藏失败" };
            }

            return json;
        }

        /// <summary>
        /// 推荐商品
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private List<ViewModels.VMItem> TuiJian(string title)
        {
            var str = RemovePunctuation(title).Distinct().ToList();
            var qv = from sf in str from ss in str select sf + ss.ToString();

            List<Model.MCategory> list = LCategory.GetCatsList();
            List<Model.MItem> iList = new List<Model.MItem>();

            foreach (var item in qv)
            {
                var cats = (from i in list where i.CatName.Contains(item) select i).ToList();
                foreach (var cat in cats)
                {
                    iList = LItem.GetItemsByCatID(Convert.ToInt64(cat.CatID.ToString())).Take(3).ToList();
                    if (iList.Count >= 3 && title.Contains(item))
                    {
                        break;
                    }
                }
                if (iList.Count >= 3 && title.Contains(item)) { break; }
            }

            string numIIDs = string.Empty;
            foreach (var item in iList)
            {
                numIIDs += item.NumIID + ",";
            }

            List<Top.Api.Domain.NTbkItem> zklist = GetZKPice(numIIDs);
            List<ViewModels.VMItem> vIList = new List<ViewModels.VMItem>();
            foreach (var item in iList)
            {
                ViewModels.VMItem i = new ViewModels.VMItem
                {
                    ID = item.ID,
                    Title = item.Title,
                    CatID = item.CatID,
                    PriceNow = item.PriceNow,
                    ImgSmall = item.ImgSmall,
                    ClickUrl = item.ClickUrl,
                    TitleDescribe = item.TitleDescribe
                };
                foreach (var zk in zklist)
                {
                    if (item.NumIID == zk.NumIid)
                    {
                        i.Price = Convert.ToDecimal(zk.ZkFinalPrice).ToString("0.00");
                        break;
                    }
                }

                vIList.Add(i);
            }

            return vIList;
        }

        /// <summary>
        /// 获取折扣后的价格
        /// </summary>
        /// <param name="numIIDs"></param>
        /// <returns></returns>
        public static List<Top.Api.Domain.NTbkItem> GetZKPice(string numIIDs)
        {
            TBTbk tkbll = new TBTbk();
            TopResponse topRsp = null;
            var strModel = tkbll.GetItemInfoByID(2, numIIDs.TrimEnd(','), ref topRsp);

            return strModel;
        }

        /// <summary>
        /// DateTime To long
        /// </summary>
        private static long ConvertDataTimeToLong(DateTime dt)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = dt.Subtract(dtStart);
            long timeStamp = toNow.Ticks;
            timeStamp = long.Parse(timeStamp.ToString().Substring(0, timeStamp.ToString().Length - 4));
            return timeStamp;
        }

        /// <summary>
        /// long To DateTime
        /// </summary>
        private static DateTime ConvertLongToDateTime(long d)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(d + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }

        /// <summary>
        /// 获取文章ViewModel列表
        /// </summary>
        /// <param name="inList"></param>
        /// <returns></returns>
        private static List<ViewModels.VMArticle> GetVmList(List<Model.MContent> inList)
        {
            List<ViewModels.VMArticle> vList = new List<ViewModels.VMArticle>();

            foreach (var item in inList)
            {
                ViewModels.VMArticle vModel = new ViewModels.VMArticle
                {
                    ArticleID = item.ArticleID,
                    DomainID = item.DomainID,
                    Title = item.Title,
                    ReleaseTime = ConvertLongToDateTime(item.ReleaseTime).ToShortDateString(),
                    Conten = item.Conten,
                    ImgSrc = GetSrc(item.Conten)
                };

                vList.Add(vModel);
            }

            return vList;
        }

        /// <summary>
        /// 获取图片地址
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private static string GetSrc(string str)
        {
            string src = string.Empty;

            int fstr = str.IndexOf("<img src=");
            if (fstr >= 0)
            {
                string content = str.Substring(fstr + 10);
                int lstr = content.IndexOf('"');
                if (lstr >= 0)
                {
                    src = content.Substring(0, lstr);
                }
            }

            return src;
        }

        /// <summary>
        /// 获取分类名称
        /// </summary>
        /// <param name="domainID"></param>
        /// <returns></returns>
        private static string GetCatName(int domainID)
        {
            string catName = string.Empty;

            switch (domainID)
            {
                case 8: catName = " 科技"; break;
                case 12: catName = "时尚"; break;
                case 16: catName = "生活"; break;
                case 21: catName = "女人"; break;
                case 6: catName = "财经"; break;
                case 4: catName = "娱乐"; break;
                case 13: catName = "游戏"; break;
                case 15: catName = "旅游"; break;
                case 14: catName = "军事"; break;
                case 19: catName = "搞笑"; break;
                default: catName = "综合"; break;
            }

            return catName;
        }

        /// <summary>
        /// 删除标点符号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string RemovePunctuation(string str)
        {
            str = str.Replace(",", "")
                              .Replace("，", "")
                              .Replace(".", "")
                              .Replace("。", "")
                              .Replace("!", "")
                              .Replace("！", "")
                              .Replace("?", "")
                              .Replace("？", "")
                              .Replace(":", "")
                              .Replace("：", "")
                              .Replace(";", "")
                              .Replace("；", "")
                              .Replace("～", "")
                              .Replace("-", "")
                              .Replace("_", "")
                              .Replace("——", "")
                              .Replace("—", "")
                              .Replace("--", "")
                              .Replace("【", "")
                              .Replace("】", "")
                              .Replace("\\", "")
                              .Replace("(", "")
                              .Replace(")", "")
                              .Replace("（", "")
                              .Replace("）", "")
                              .Replace("#", "")
                              .Replace("$", "");
            return str;
        }
    }
}