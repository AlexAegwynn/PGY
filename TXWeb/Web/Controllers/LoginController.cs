using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LoginController : BaseController
    {
        private static string VerificationCode = string.Empty;

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult LoginPartial()
        {
            ViewBag.Module = "Login";
            return PartialView();
        }

        public PartialViewResult RegisterPartial()
        {
            ViewModels.UserViewModel vModel = new ViewModels.UserViewModel();

            ViewBag.Module = "Register";
            return PartialView(vModel);
        }

        [HttpPost]
        public JsonResult ExistUser(string inEmail, string inPassword)
        {
            JsonResult json = new JsonResult();

            try
            {
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

                    Logic.LogList.InsertLog(vModel.UserID, vModel.UserName, DateTime.Now, "成功登录");

                    HttpContext.Session["UserInfo"] = vModel;
                    json.Data = new { result = true, msg = "" };
                }
                else
                {
                    json.Data = new { result = false, msg = "用户名或密码错误！" };
                }
            }
            catch (Exception ex)
            {
                Logic.LogList.InsertLog(0, "system", DateTime.Now, ex.Message);
                json.Data = new { result = false, msg = "登录失败" };
            }

            return json;
        }

        [HttpPost]
        public JsonResult RegisterUser(ViewModels.LoginViewModel inModel)
        {
            JsonResult json = new JsonResult();

            try
            {
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

                    Logic.LogList.InsertLog(vModel.UserID, vModel.UserName, DateTime.Now, "注册并成功登录");

                    HttpContext.Session["UserInfo"] = vModel;

                    json.Data = new { result = true, msg = "" };
                }
                else
                {
                    json.Data = new { result = false, msg = "注册失败" };
                }
            }
            catch (Exception ex)
            {
                Logic.LogList.InsertLog(0, "system", DateTime.Now, ex.Message);
                json.Data = new { result = false, msg = "注册失败" };
            }

            return json;
        }

        [HttpPost]
        public JsonResult VerifyPhone(string inPhoneNumber)
        {
            JsonResult json = new JsonResult();

            VerificationCode = "123";

            json.Data = new { result = true };

            return json;
        }

        public ActionResult Logout()
        {
            try
            {
                Logic.LogList.InsertLog(LoginUser.UserID, LoginUser.UserName, DateTime.Now, "退出登录");
                HttpContext.Session.Remove("UserInfo");
            }
            catch (Exception ex)
            {
                Logic.LogList.InsertLog(0, "system", DateTime.Now, ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}