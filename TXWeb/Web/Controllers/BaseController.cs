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

        protected ViewModels.UserViewModel LoginUser
        {
            get
            {
                Model.UserList model = HttpContext.Session["UserInfo"] as Model.UserList;
                if (model == null) { return null; }

                ViewModels.UserViewModel vModel = new ViewModels.UserViewModel
                {
                    Name = model.Name,
                    Email = model.Email,
                    IsAdmin = Convert.ToBoolean(model.IsAdmin)
                };

                return vModel;
            }
        }
    }
}