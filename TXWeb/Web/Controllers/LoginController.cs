using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LoginController : BaseController
    {
        private static string VerificationCode = "123";

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
            ViewModels.UserViewModel vModel = new ViewModels.UserViewModel();

            return PartialView(vModel);
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

            Model.UserList model = Logic.UserList.ExistUser(inEmail, inPassword);

            if (model != null)
            {
                ViewModels.LoginViewModel vModel = new ViewModels.LoginViewModel
                {
                    UserID = model.UserID,
                    UserName = model.UserName,
                    Email = model.Email,
                    IsAdmin = Convert.ToBoolean(model.IsAdmin)
                };

                HttpContext.Session["UserInfo"] = vModel;
                json.Data = new { result = true, msg = "" };
            }
            else
            {
                json.Data = new { result = false, msg = "用户名或密码错误！" };
            }

            return json;
        }

        [HttpPost]
        public JsonResult RegisterUser(ViewModels.LoginViewModel inModel)
        {
            JsonResult json = new JsonResult();

            if (inModel.Password != inModel.ConfirmPwd)
            {
                json.Data = new { result = false, msg = "密码不一致" }; return json;
            }

            if (inModel.VerificationCode != VerificationCode)
            {
                json.Data = new { result = false, msg = "验证码不正确" }; return json;
            }

            if (Logic.UserList.ExistUser(inModel.Email) > 0)
            {
                json.Data = new { result = false, msg = "邮箱已被注册" }; return json;
            }

            Model.UserList model = new Model.UserList
            {
                UserName = inModel.UserName,
                Email = inModel.Email,
                Password = inModel.Password,
                PhoneNumber = inModel.PhoneNumber
            };

            int result = Logic.UserList.CreateUser(model);

            if (result == 1)
            {
                Model.UserList login = Logic.UserList.ExistUser(model.Email, model.Password);
                ViewModels.LoginViewModel vModel = new ViewModels.LoginViewModel
                {
                    UserID = login.UserID,
                    UserName = login.UserName,
                    Email = login.Email,
                    IsAdmin = Convert.ToBoolean(login.IsAdmin)
                };

                HttpContext.Session["UserInfo"] = vModel;

                json.Data = new { result = true, msg = "" };
            }
            else
            {
                json.Data = new { result = false, msg = "注册失败" };
            }

            return json;
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("UserInfo");
            return RedirectToAction("Index");
        }
    }
}