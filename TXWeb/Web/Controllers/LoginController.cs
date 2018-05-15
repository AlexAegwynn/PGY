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

        [HttpPost]
        public JsonResult RegisterUser(ViewModels.UserViewModel inModel)
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

            Model.UserList model = new Model.UserList
            {
                Name = inModel.Name,
                Email = inModel.Email,
                Password = inModel.Password,
                PhoneNumber = inModel.PhoneNumber
            };

            int result = Logic.UserList.CreateUser(model);

            if (result == 1)
            {
                Model.UserList login = Logic.UserList.GetUser(model.Email, model.Password);
                HttpContext.Session["UserInfo"] = login;

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