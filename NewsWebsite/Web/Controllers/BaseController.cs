using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            State = filterContext.RequestContext.HttpContext.Request.Browser.IsMobileDevice;
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 是否移动设备
        /// </summary>
        protected static bool State { get; private set; } = false;
    }
}