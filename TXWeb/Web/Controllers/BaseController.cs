using System;
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

        protected ViewModels.LoginViewModel LoginUser
        {
            get
            {
                ViewModels.LoginViewModel vModel = HttpContext.Session["UserInfo"] as ViewModels.LoginViewModel;
                if (vModel == null) { return null; }

                return vModel;
            }
        }
    }
}