using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Web.ViewModels;

using Common;
using System.Text;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private static List<VMContent> vmContents = new List<VMContent>();

        public ActionResult Index(int catid = 0)
        {
            GetvList(catid);

            ViewBag.CatID = catid;

            decimal total = vmContents.Count;
            int pcount = Convert.ToInt32(Math.Ceiling(total / 10));  //页数

            ViewBag.Count = pcount;

            return View();
        }

        public PartialViewResult ContentList(int index = 0, int catid = 0)
        {
            if (vmContents == null)
            {
                GetvList(catid);
            }

            List<VMContent> vList = vmContents.Skip(10 * index).Take(10).ToList();

            return PartialView(vList);
        }

        [HttpPost]
        public JsonResult SaveContents(VMContent inModel)
        {
            JsonResult json = new JsonResult();

            inModel.Conten = inModel.Conten.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&apos;", "'");

            json.Data = true;

            return json;
        }

        public PartialViewResult ShowVideo(int articleID)
        {

            return PartialView();
        }

        public JsonResult SaveVideo()
        {
            JsonResult json = new JsonResult();

            HttpPostedFileBase file = Request.Files["file"];

            string path = @"\\STONE20\LuCao\";

            if (file.ContentLength != 0)
            {
                string filePath = path + file.FileName;

                file.SaveAs(filePath);   //保存文件
            }

            json.Data = true;

            return json;
        }

        public ActionResult DLVideo()
        {
            string url = "https://weibo.com/tv/v/Gugv0y2E7?fid=1034:4271877846162243";

            CookieContainer ccookies = new CookieContainer();
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myHttpWebRequest.Timeout = 20 * 1000; //连接超时  
            myHttpWebRequest.Accept = "*/*";
            myHttpWebRequest.Host = "weibo.com";
            myHttpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
            myHttpWebRequest.CookieContainer = new CookieContainer(); //暂存到新实例
            myHttpWebRequest.GetResponse().Close();

            ccookies = myHttpWebRequest.CookieContainer; //保存cookies
            string cookiesstr = myHttpWebRequest.CookieContainer.GetCookieHeader(myHttpWebRequest.RequestUri); //把cookies转换成字符串  

            myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myHttpWebRequest.Timeout = 20 * 1000; //连接超时  
            myHttpWebRequest.Accept = "*/*";
            myHttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0;)";
            //myHttpWebRequest.CookieContainer = ccookies; //使用已经保存的cookies 方法一
            myHttpWebRequest.Headers.Add("Cookie", cookiesstr); //使用已经保存的cookies 方法二  
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream stream = myHttpWebResponse.GetResponseStream();
            stream.ReadTimeout = 15 * 1000; //读取超时  
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("utf-8"));
            string strWebData = sr.ReadToEnd();

            return RedirectToAction("Index");
        }

        private void GetvList(int catid)
        {
            vmContents = new List<VMContent>();
            List<Model.MContent> list = Logic.LContent.GetContents(catid);

            foreach (var item in list)
            {
                List<string> strList = GetContentOrImgUrl(item.Conten);
                List<string> imgList = GetImgList(strList[1]);
                var imgStr = string.Empty;

                foreach (var img in imgList)
                {
                    if (img.Contains("video") || img.Contains("m3u8"))
                    {
                        imgStr += "<video controls style=\"width: 430px;\" preload=\"auto\" autoplay=\"autoplay\" src=\"" + img + "\" ></ video >";
                    }
                    else
                    {
                        imgStr += "<img src=\"" + img + "\" />";
                    }
                }

                VMContent vModel = new VMContent
                {
                    ArticleID = item.ArticleID,
                    DomainID = item.DomainID,
                    Conten = strList[0] + "<br />" + imgStr
                };

                vmContents.Add(vModel);
            }
        }

        private List<string> GetContentOrImgUrl(string content)
        {
            List<string> strList = new List<string>();

            strList = Regex.Split(content, "#Split#", RegexOptions.None).ToList();

            return strList;
        }

        private List<string> GetImgList(string imgStr)
        {
            List<string> imgList = new List<string>();

            imgList = Regex.Split(imgStr, "&&", RegexOptions.None).ToList();
            imgList.Remove("");

            return imgList;
        }
    }
}