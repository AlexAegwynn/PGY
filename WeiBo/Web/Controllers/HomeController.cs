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

        public JsonResult DownLoadVideo()
        {
            JsonResult json = new JsonResult();

            System.Uri uri = new System.Uri("https://weibo.com/tv/v/Gu7ghANCE?fid=1034:4271745746560517");

            string cookies = string.Empty;

            Common.Cookies c = new Cookies();
            c.GetCookies(uri, "", ref cookies);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://weibo.com/tv/v/Gu7ghANCE?fid=1034:4271745746560517");

            if (cookies != "")
                request.Headers[HttpRequestHeader.Cookie] = cookies;

            //request.CookieContainer = new CookieContainer();
            request.Method = "Get";
            request.ContentType = "text/html;charset=UTF-8";
            //request.Host = "www.360doc.com";
            request.Host = "www.weibo.com";
            request.Referer = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();//执行请求，获取响应对象

            Stream stream = response.GetResponseStream();       //获取响应流
            StreamReader sr = new StreamReader(stream);         //创建流读取对象
            string responseHTML = sr.ReadToEnd();                   //读取响应流

            response.Close();//关闭响应流

            json.Data = false;

            return json;
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
        
        /// <summary>
		/// 视频下载
		/// </summary>
		public bool GetDown(string url, string filename)
        {
            bool isenable = true;
            try
            {
                int size = 0;
                string edocUrl = System.Configuration.ConfigurationManager.AppSettings["ApiServiceUrl"];
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)");
                    using (Stream stream = webClient.OpenRead(url))
                    {
                        using (FileStream fs = System.IO.File.Create(filename))
                        {
                            int i = 0;
                            do
                            {
                                byte[] buffer = new byte[1024];
                                i = stream.Read(buffer, 0, 1024);
                                fs.Write(buffer, 0, i);
                            } while (i > 0);
                        }
                    }
                }  
            }
            catch (Exception ex)
            {
                isenable = false;
            }
            finally
            {
                GC.Collect();
            }
            return isenable;
        }
    }
}