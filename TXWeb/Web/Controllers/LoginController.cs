using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult LoginPartial()
        {
            return PartialView();
        }

        public PartialViewResult RegisterPartial()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult ExistUser(string inEmail, string inPassword)
        {
            JsonResult json = new JsonResult();

            if (string.IsNullOrEmpty(inEmail) || string.IsNullOrEmpty(inPassword))
            {
                json.Data = new { result = false, msg = "用户名或密码不能为空！" };
                return json;
            }

            Model.UserList model = Logic.UserList.GetUser(inEmail, inPassword);

            if (model != null)
            {
                HttpContext.Session["UserInfo"] = model;
                json.Data = new { result = true, msg = "" };
            }
            else
            {
                json.Data = new { result = false, msg = "用户名或密码错误！" };
            }

            return json;
        }
    }
}