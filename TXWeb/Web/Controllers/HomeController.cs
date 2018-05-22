using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (LoginUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (LoginUser.IsAdmin)
            {
                return RedirectToAction("UserList");
            }
            else
            {
                return RedirectToAction("CommodityList");
            }
        }

        public ActionResult UserList()
        {
            if (LoginUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            List<Model.UserList> list = Logic.UserList.GetUsers();

            ViewData["LoginUser"] = LoginUser;
            ViewBag.Module = "comoruser";

            return View(list);
        }

        public ActionResult UserInfo()
        {
            if (LoginUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["LoginUser"] = LoginUser;

            ViewModels.UserViewModel vModel = new ViewModels.UserViewModel();
            vModel.User = Logic.UserList.GetUser(LoginUser.UserID);

            ViewBag.Module = "info";

            return View(vModel);
        }

        public ActionResult UserPreview(int inUserID)
        {
            if (LoginUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["LoginUser"] = LoginUser;

            ViewModels.UserViewModel vModel = new ViewModels.UserViewModel();
            vModel.User = Logic.UserList.GetUser(inUserID);

            return View(vModel);
        }

        public ActionResult UpdateUser(int inUserID)
        {
            if (LoginUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["LoginUser"] = LoginUser;

            ViewModels.UserViewModel vModel = new ViewModels.UserViewModel();
            vModel.User = Logic.UserList.GetUser(inUserID);

            ViewBag.Module = "comoruser";

            return View("~/Views/Home/UserInfo.cshtml", vModel);
        }

        [HttpPost]
        public JsonResult UpdateUser(ViewModels.UserViewModel inModel)
        {
            JsonResult json = new JsonResult();

            if (Logic.UserList.UpdateUser(inModel.User) == 1)
            {
                json.Data = new { result = true };
            }
            else
            {
                json.Data = new { result = false, msg = "保存失败" };
            }

            return json;
        }

        [HttpPost]
        public JsonResult DelectUser(int inUserID)
        {
            JsonResult json = new JsonResult();



            return json;
        }

        public ActionResult CommodityList(int page = 1)
        {
            if (LoginUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["LoginUser"] = LoginUser;

            var list = Logic.CommodityList.GetUserCommodityList(LoginUser.UserID);
            decimal count = Math.Ceiling(Convert.ToDecimal(list.Count) / 8);

            list = list.Skip((page - 1) * 8).Take(8).ToList();

            ViewModels.CommodityViewModel vModel = new ViewModels.CommodityViewModel();
            vModel.CommodityList = list;
            vModel.PageCode = page;
            vModel.PageCount = Convert.ToInt32(count);

            ViewBag.Module = "comoruser";

            return View(vModel);
        }

        public ActionResult AllCommodityList(int page = 1, string sprice = "", string hprice = "", string search = "", string cate = "")
        {
            if (LoginUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["LoginUser"] = LoginUser;

            var list = Logic.CommodityList.GetCommodityList();

            if (search != "")
            {
                list = (from c in list where c.Title.ToUpper().IndexOf(search.ToUpper()) >= 0 select c).ToList();
            }
            if (sprice != "")
            {
                list = (from c in list where c.Price >= Convert.ToInt32(sprice) select c).ToList();
            }
            if (hprice != "")
            {
                list = (from c in list where c.Price <= Convert.ToInt32(hprice) select c).ToList();
            }
            if (cate != "" && cate.Split('_')[1] != "0")
            {
                list = (from c in list where c.Category == Convert.ToInt32(cate.Split('_')[1]) select c).ToList();
            }

            decimal count = Math.Ceiling(Convert.ToDecimal(list.Count) / 8);

            list = list.Skip((page - 1) * 8).Take(8).ToList();

            ViewModels.CommodityViewModel vModel = new ViewModels.CommodityViewModel
            {
                CommodityList = list,
                PageCode = page,
                PageCount = Convert.ToInt32(count),
                SPrice = sprice,
                HPrice = hprice,
                Search = search,
                CatID = cate
            };

            foreach (var item in Enum.GetValues(typeof(Common.Categorys)))
            {
                ViewModels.Catagory cat = new ViewModels.Catagory
                {
                    CategoryID = Convert.ToInt32(item),
                    CategoryStr = item.ToString()
                };

                vModel.CateList.Add(cat);
            }

            ViewBag.Module = "allcomm";

            return View(vModel);
        }

        public ActionResult CommodityInfo(string inCommodityID = "")
        {
            if (LoginUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["LoginUser"] = LoginUser;

            ViewModels.CommodityInfoViewModel vModel = new ViewModels.CommodityInfoViewModel();
            vModel.IsEdit = false;
            if (inCommodityID != "")
            {
                vModel.IsEdit = true;
                vModel.CommodityInfo = Logic.CommodityList.GetCommodity(Convert.ToInt32(inCommodityID));
            }

            List<SelectListItem> catList = new List<SelectListItem>();

            foreach (var item in Enum.GetValues(typeof(Common.Categorys)))
            {
                SelectListItem cat = new SelectListItem
                {
                    Value = Convert.ToInt32(item).ToString(),
                    Text = item.ToString()
                };
                catList.Add(cat);
            }
            //catList[0].Text = "未分类";
            ViewData["CatList"] = catList;

            ViewBag.Module = "comoruser";

            return View(vModel);
        }
        
        public ActionResult CommodityPreview(int inCommodityID)
        {
            if (LoginUser == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewData["LoginUser"] = LoginUser;

            ViewModels.CommodityInfoViewModel vModel = new ViewModels.CommodityInfoViewModel();
            vModel.CommodityInfo = Logic.CommodityList.GetCommodity(inCommodityID);

            return View(vModel);
        }

        [HttpPost]
        public JsonResult SaveCommodity(ViewModels.CommodityInfoViewModel inModel)
        {
            JsonResult json = new JsonResult();
            bool isAdd = inModel.CommodityInfo.CommodityID == 0;
            int result = 0;

            string path = @"/Images/" + LoginUser.UserID + @"/";

            string saveFileUrl = ".." + path;  //存数据库和显示的路径
            string saveFilePath = Server.MapPath("~" + path);  //存储文件的路径            

            if (!Directory.Exists(saveFilePath))
            {
                Directory.CreateDirectory(saveFilePath);
            }

            HttpPostedFileBase file = Request.Files["picfile"];

            if (isAdd)
            {
                if (file.ContentLength != 0)
                {
                    string fileName = Guid.NewGuid().ToString();

                    string fileEx = Path.GetExtension(file == null ? "" : file.FileName);  //获取后缀
                    string filePath = saveFilePath + fileName + fileEx;  //文件重命名

                    inModel.CommodityInfo.ImgUrl = saveFileUrl + fileName + fileEx;
                    file.SaveAs(filePath);   //保存文件
                }
                inModel.CommodityInfo.UserID = LoginUser.UserID;
                result = Logic.CommodityList.CreateCommodity(inModel.CommodityInfo);
            }
            else
            {
                var imgUrl = Logic.CommodityList.GetImgUrl(inModel.CommodityInfo.CommodityID);

                if (file.ContentLength != 0)
                {
                    if (string.IsNullOrEmpty(imgUrl))
                    {
                        string fileName = Guid.NewGuid().ToString();

                        string fileEx = Path.GetExtension(file == null ? "" : file.FileName);  //获取后缀
                        string filePath = saveFilePath + fileName + fileEx;  //文件重命名

                        inModel.CommodityInfo.ImgUrl = saveFileUrl + fileName + fileEx;
                        file.SaveAs(filePath);   //保存文件
                    }
                    else
                    {
                        string fileName = imgUrl.Substring(imgUrl.LastIndexOf("/") + 1);
                        string filePath = saveFilePath + fileName;
                        inModel.CommodityInfo.ImgUrl = imgUrl;
                        file.SaveAs(filePath);   //保存文件
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(inModel.CommodityInfo.ImgUrl))
                    {
                        if (!string.IsNullOrEmpty(imgUrl))
                        {
                            string fileName = imgUrl.Substring(imgUrl.LastIndexOf("/") + 1);
                            string filePath = saveFilePath + fileName;

                            inModel.CommodityInfo.ImgUrl = "";
                            System.IO.File.Delete(filePath);
                        }
                    }
                }

                result = Logic.CommodityList.UpdateCommodity(inModel.CommodityInfo);
            }

            json.Data = new { result = result > 0 };

            return json;
        }
    }
}