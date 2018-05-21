using System;
using System.Collections.Generic;
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

            return View();
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

            decimal count = Math.Ceiling(Convert.ToDecimal(list.Count) / 8);

            list = list.Skip((page - 1) * 8).Take(8).ToList();

            ViewModels.CommodityViewModel vModel = new ViewModels.CommodityViewModel
            {
                CommodityList = list,
                PageCode = page,
                PageCount = Convert.ToInt32(count),
                SPrice = sprice,
                HPrice = hprice,
                Search = search
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
            if (inCommodityID != "")
            {
                vModel.CommodityInfo = Logic.CommodityList.GetCommodity(Convert.ToInt32(inCommodityID));
            }

            ViewBag.Module = "comoruser";

            return View(vModel);
        }

        [HttpPost]
        public JsonResult SaveCommodity(ViewModels.CommodityInfoViewModel inModel)
        {
            JsonResult json = new JsonResult();
            bool isAdd = inModel.CommodityInfo.CommodityID == 0;
            int result = 0;
            if (isAdd)
            {
                inModel.CommodityInfo.UserID = LoginUser.UserID;
                result = Logic.CommodityList.CreateCommodity(inModel.CommodityInfo);
            }
            else
            {
                result = Logic.CommodityList.UpdateCommodity(inModel.CommodityInfo);
            }

            json.Data = new { result = result > 0 };

            return json;
        }
    }
}