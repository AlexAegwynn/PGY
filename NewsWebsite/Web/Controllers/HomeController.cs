using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Logic;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// 所有文章列表
        /// </summary>
        private static List<Model.MContent> articleList = new List<Model.MContent>();
        /// <summary>
        /// 页面文章列表
        /// </summary>
        private static List<Model.MContent> aList = new List<Model.MContent>();

        public ActionResult Index(string search = "", int catid = 0)
        {
            articleList = LContent.GetArticles();

            //var a = articleList.Where(m => m.ArticleID == 5822665).ToList();
            //var text = GetSrc(a[0].Conten);

            var list = (from l in articleList where l.Conten.Contains("<img src=") orderby Guid.NewGuid() select l).Take(6).ToList();
            List<ViewModels.VMArticle> vList = GetVmList(list);

            ViewData["ShowList"] = vList;  //首页图片轮播

            var ywList = (from l in articleList where l.Conten.Contains("<img src=") orderby l.ReleaseTime descending select l).Take(6).ToList();
            List<ViewModels.VMArticle> vywList = GetVmList(ywList);

            ViewData["NewList"] = vywList;  //最新要闻

            aList = articleList;

            if (!string.IsNullOrEmpty(search))
            {
                aList = aList.Where(m => m.Title.Contains(search)).ToList();
            }

            if (catid != 0)
            {
                aList = aList.Where(m => m.DomainID == catid).ToList();
            }

            decimal total = aList.Count;
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
            if (aList == null || aList.Count <= 0)
            {
                aList = LContent.GetArticles();
            }

            List<Model.MContent> list = aList;

            list = list.Skip(10 * page).Take(10).ToList();

            List<ViewModels.VMArticle> vList = GetVmList(list);

            return PartialView(vList);
        }

        public ActionResult ArticleInfo(long inArticleID)
        {
            //List<Model.MContent> list = Logic.LContent.GetArticles();
            if (articleList == null || articleList.Count <= 0)
            {
                articleList = LContent.GetArticles();
            }

            List<Model.MContent> list = articleList;
            ViewModels.VMArticle vModel = new ViewModels.VMArticle();
            list = (from l in list where l.ArticleID == inArticleID select l).ToList();

            if (list.Count > 0)
            {
                vModel.ArticleID = list[0].ArticleID;
                vModel.Title = list[0].Title;
                vModel.DomainID = list[0].DomainID;
                vModel.ReleaseTime = ConvertLongToDateTime(list[0].ReleaseTime).ToShortDateString();
                vModel.Conten = list[0].Conten;

                ViewBag.IsLogin = false;
                ViewBag.Name = string.Empty;

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
                    json.Data = new { result = false, msg = "请先登录" };
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
    }
}