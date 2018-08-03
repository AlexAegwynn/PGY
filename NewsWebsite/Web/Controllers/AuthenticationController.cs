using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public JsonResult Login(ViewModels.VMUser inModel)
        {
            JsonResult json = new JsonResult();

            if (string.IsNullOrEmpty(inModel.UserName) || string.IsNullOrEmpty(inModel.Password))
            {
                json.Data = new { result = false, msg = "ERROR" };
            }

            if (inModel.UID == 0)
            {
                Model.MUsers model = new Model.MUsers
                {
                    UserName = inModel.UserName,
                    Password = inModel.Password,
                    Email = inModel.Email ?? ""
                };
                bool result = Logic.LUsers.CreateUser(model) > 0;

                string msg = result ? "" : "注册失败";

                if (result)
                {
                    Model.MUsers user = Logic.LUsers.GetUsers(inModel.UserName, inModel.Password);

                    SetSession(user);
                }

                json.Data = new { result, msg };
            }
            else
            {
                Model.MUsers model = Logic.LUsers.GetUsers(inModel.UserName, inModel.Password);

                bool result = model != null;

                if (result)
                {
                    SetSession(model);
                }

                json.Data = new { result, msg = "用户名或密码错误" };
            }

            return json;
        }

        public ActionResult Logout()
        {
            Session.Remove("LoginUser");

            return RedirectToAction("Index", "Home");
        }

        private void SetSession(Model.MUsers inUser)
        {
            ViewModels.VMUser vModel = new ViewModels.VMUser
            {
                UID = inUser.UID,
                UserName = inUser.UserName,
                Password = inUser.Password,
                Email = inUser.Email
            };

            Session["LoginUser"] = vModel;
        }
    }
}