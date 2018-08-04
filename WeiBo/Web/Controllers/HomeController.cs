﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Web.ViewModels;

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

        private void GetvList(int catid)
        {
            vmContents = new List<VMContent>();
            List<Model.MContent> list = Logic.LContent.GetContents(catid);

            foreach (var item in list)
            {
                List<string> strList = GetContentOrImgUrl(item.Conten);

                VMContent vModel = new VMContent
                {
                    ArticleID = item.ArticleID,
                    DomainID = item.DomainID,
                    Conten = strList[0],
                    ImgUrl = strList[1]
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
    }
}