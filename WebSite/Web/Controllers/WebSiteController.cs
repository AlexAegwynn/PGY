using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    /// <summary>
    /// 站点后台控制器
    /// </summary>
    public class WebSiteController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(ViewModels.LoginUserViewModel model)
        {
            JsonResult json = new JsonResult();
            if (model.Name == "stone" && model.Password == "pgy123")
            {
                HttpContext.Session["LoginUser"] = model;
                json.Data = new { result = true };
            }
            else
            {
                json.Data = new { result = false };
            }
            return json;
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("LoginUser");

            return RedirectToAction("Index");
        }

        public ActionResult WebSiteList()
        {
            ViewModels.LoginUserViewModel vLogin = HttpContext.Session["LoginUser"] as ViewModels.LoginUserViewModel;

            if (vLogin == null) { return RedirectToAction("Index"); }

            List<Model.WebSiteInfo> list = Logic.WebSite.GetWebSiteList();
            ViewBag.UserName = vLogin.Name;

            return View(list);
        }

        public ActionResult WebSiteInfo(string inKey)
        {
            ViewModels.LoginUserViewModel vLogin = HttpContext.Session["LoginUser"] as ViewModels.LoginUserViewModel;

            if (vLogin == null) { return RedirectToAction("Index"); }

            ViewModels.WebSiteViewModel vModel = new ViewModels.WebSiteViewModel();
            vModel.IsEdit = false;

            if (!string.IsNullOrEmpty(inKey))
            {
                Model.WebSiteInfo model = Logic.WebSite.GetWebSiteByKey(new Guid(inKey));
                vModel.IsEdit = true;
                vModel.WebSiteKey = model.WebSiteKey;
                vModel.WebSiteName = model.WebSiteName;
                vModel.WebSiteID = model.WebSiteID;
                vModel.DomainName = model.DomainName;
                vModel.LogoImgUrl = model.LogoImgUrl;
                vModel.CompanyName = model.CompanyName;
                vModel.Address = model.Address;
                vModel.PhoneNumber = model.PhoneNumber;
                vModel.QQ = model.QQ;
                vModel.WeChat = model.WeChat;
                vModel.Email = model.Email;
                vModel.QRCodeUrl = model.QRCodeUrl;
                vModel.RecordNumber = model.RecordNumber;
                vModel.Keywords = model.Keywords;
                vModel.Description = model.Description;
                vModel.Category = model.Category;
                vModel.BackgroundImg = model.BackgroundImg;
            }

            ViewBag.UserName = vLogin.Name;

            return View(vModel);
        }

        [HttpPost]
        public JsonResult SaveWebSite(ViewModels.WebSiteViewModel inModel)
        {
            JsonResult json = new JsonResult();

            if (inModel.WebSiteKey == Guid.Empty && inModel.IsEdit)
            {
                json.Data = new { result = false, message = "保存失败" };
                return json;
            }

            if (string.IsNullOrEmpty(inModel.DomainName))
            {
                json.Data = new { result = false, message = "域名不能为空" };
                return json;
            }

            Model.WebSiteInfo model = new Model.WebSiteInfo()
            {
                WebSiteKey = inModel.IsEdit ? inModel.WebSiteKey : Guid.NewGuid(),
                DomainName = inModel.DomainName,

                WebSiteName = inModel.WebSiteName ?? "",
                CompanyName = inModel.CompanyName ?? "",
                Address = inModel.Address ?? "",
                PhoneNumber = inModel.PhoneNumber ?? "",
                QQ = inModel.QQ ?? "",
                WeChat = inModel.WeChat ?? "",
                Email = inModel.Email ?? "",
                RecordNumber = inModel.RecordNumber ?? "",
                Keywords = inModel.Keywords ?? "",
                Description = inModel.Description ?? "",
                Category = inModel.Category ?? "",
            };

            if (!inModel.IsEdit)
            {
                model.LogoImgUrl = inModel.LogoImgUrl ?? "";
                model.QRCodeUrl = inModel.QRCodeUrl ?? "";
                model.BackgroundImg = inModel.BackgroundImg ?? "";
            }

            int n = inModel.IsEdit ? Logic.WebSite.UpdateWebSite(model) : Logic.WebSite.AddWebSite(model);

            json.Data = n < 0 ? new { result = false, message = "保存失败", key = "" } : new { result = true, message = "", key = model.WebSiteKey.ToString() };

            return json;
        }

        [HttpPost]
        public JsonResult DeleteWebSite(string inKey)
        {
            JsonResult json = new JsonResult();
            int n = Logic.WebSite.DeleteWebSite(new Guid(inKey));

            json.Data = n <= 0 ? new { result = false, message = "删除失败" } : new { result = true, message = "" };

            return json;
        }

        [HttpPost]
        public JsonResult SaveImg(ViewModels.ImgViewModel model)
        {
            JsonResult json = new JsonResult();

            //string path = model.FileName == "bgfile" ? @"/Images/" + model.DomainName + @"/" : @"\Images\" + model.DomainName + @"\";
            string path = @"/Images/" + model.DomainName + @"/";

            string saveFileUrl = ".." + path;  //存数据库和显示的路径
            string saveFilePath = Server.MapPath("~" + path);  //存储文件的路径

            if (!Directory.Exists(saveFilePath))
            {
                Directory.CreateDirectory(saveFilePath);
            }

            HttpPostedFileBase file = Request.Files[model.FileName]; //获取文件
            string fileEx = Path.GetExtension(file == null ? "" : file.FileName);  //获取后缀
            string filePath = saveFilePath + model.FileName + fileEx;  //文件重命名

            if (file != null)
            {
                string fileurl = saveFileUrl + model.FileName + fileEx;
                int result = 0;

                switch (model.FileName)
                {
                    case "logofile":
                        result = Logic.WebSite.SaveLogo(fileurl, model.WebSiteKey); break;
                    case "qrcodefile":
                        result = Logic.WebSite.SaveQRCode(fileurl, model.WebSiteKey); break;
                    case "bgfile":
                        result = Logic.WebSite.SaveBackgroundImg(fileurl, model.WebSiteKey); break;
                }

                if (result > 0)
                {
                    file.SaveAs(filePath);   //保存文件
                    json.Data = new { result = true, msg = "保存成功", url = fileurl };
                }
            }
            else if (string.IsNullOrEmpty(model.ImgUrl))
            {
                List<string> list = Logic.WebSite.GetImgList(model.WebSiteKey);
                string imgPath = model.FileName == "logofile" ? list[0] : model.FileName == "qrcodefile" ? list[1] : list[2];

                if (!string.IsNullOrEmpty(imgPath))
                {
                    imgPath = saveFilePath + imgPath.Substring(imgPath.LastIndexOf("/") + 1);
                    int result = 0;

                    switch (model.FileName)
                    {
                        case "logofile":
                            result = Logic.WebSite.DeleteLogo(model.WebSiteKey); break;
                        case "qrcodefile":
                            result = Logic.WebSite.DeleteQRCode(model.WebSiteKey); break;
                        case "bgfile":
                            result = Logic.WebSite.DeleteBackgroundImg(model.WebSiteKey); break;
                    }

                    if (result > 0)
                    {
                        System.IO.File.Delete(imgPath);   //删除文件
                        json.Data = new { result = true, msg = "保存成功", url = "" };
                    }
                }
                else
                {
                    json.Data = new { result = true, msg = "保存成功", url = "" };
                }
            }
            else
            {
                json.Data = new { result = true, msg = "保存成功", url = model.ImgUrl };
            }

            return json;
        }
    }
}