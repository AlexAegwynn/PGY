﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        private static string DomainName = string.Empty;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DomainName = filterContext.RequestContext.HttpContext.Request.Url.Host;
            
            string isLocal = DomainName == "localhost" ? "Development" : "Release";
            Common.Keys.ConnectionString(isLocal);

            base.OnActionExecuting(filterContext);
        }

        protected Model.UserList LoginUser
        {
            get
            {
                return HttpContext.Session["UserInfo"] as Model.UserList;
            }
        }
    }
}