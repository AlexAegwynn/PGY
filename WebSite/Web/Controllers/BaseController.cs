﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Web.ViewModels;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        private static string DomainName = string.Empty;
        private static string ID = string.Empty;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DomainName = filterContext.RequestContext.HttpContext.Request.Url.Host;
            State = filterContext.RequestContext.HttpContext.Request.Browser.IsMobileDevice;

            //var port = filterContext.RequestContext.HttpContext.Request.Url.Port;
            //if (port == 8001)
            //{
            //    ID = "7";
            //}
            //else if (port == 8002)
            //{
            //    ID = "5";
            //}
            //else
            //{
            //    ID = "5";
            //}

            string isLocal = DomainName == "localhost" ? "Development" : "Release";
            //string isLocal = DomainName = "Release";
            //string isLocal = DomainName = "Development";
            Common.Keys.ConnectionString(isLocal);

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 网站ID
        /// </summary>
        protected static string WebSiteID
        {
            get
            {
                GetWebSiteID();

                //return ID = "5";
                return ID;
            }
        }

        /// <summary>
        /// 访问数量
        /// </summary>
        protected static Model.AccessStatistics ASList
        {
            get
            {
                Model.AccessStatistics model = Logic.AccessStatistics.GetAS(Convert.ToInt32(ID));

                if (model == null)
                {
                    model = new Model.AccessStatistics();
                }

                return model;
            }
        }

        /// <summary>
        /// 是否移动设备
        /// </summary>
        protected static bool State { get; private set; } = false;

        private static void GetWebSiteID()
        {
            string result = string.Empty;
            string path = @"/Content/JsonFiles/WebSiteInfo.json";
            string jsonPath = System.Web.HttpContext.Current.Server.MapPath("~" + path);

            if (!System.IO.File.Exists(jsonPath))
            {
                System.IO.File.Create(jsonPath);
            }

            using (StreamReader sr = new StreamReader(jsonPath))
            {
                try
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Converters.Add(new JavaScriptDateTimeConverter());
                    serializer.NullValueHandling = NullValueHandling.Ignore;

                    JsonReader reader = new JsonTextReader(sr);
                    List<WebSiteInfo> list = serializer.Deserialize<List<WebSiteInfo>>(reader);
                    list = (from l in list where l.DomainName == DomainName select l).ToList();

                    ID = list.Count > 0 ? list[0].WebSiteID : "2";
                }
                catch (Exception e)
                {
                    string msg = e.Message.ToString();
                }
            }
        }

        /// <summary>
        /// 网站信息
        /// </summary>
        class WebSiteInfo
        {
            public string DomainName { get; set; }
            public string WebSiteID { get; set; }
        }
    }
}