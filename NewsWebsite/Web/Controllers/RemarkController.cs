using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class RemarkController : Controller
    {
        // GET: TestRemark
        public ActionResult Index()
        {
            List<Model.MRemarks> list = Logic.LRemarks.GetRemarks(6672546);
            List<ViewModels.VMRemark> vList = new List<ViewModels.VMRemark>();
            foreach (var item in list)
            {
                ViewModels.VMRemark vModel = new ViewModels.VMRemark
                {
                    RID = item.RID,
                    ArticleID = item.ArticleID,
                    UID = item.UID,
                    UName = item.UName,
                    Remark = item.Remark,
                    RTime = item.RTime.ToString(),
                    RepliesTotal = Logic.LRemarks.GetReplyTotal(item.RID)
                };
                vList.Add(vModel);
            }
            return View(vList);
        }

        public JsonResult InsertRemark(ViewModels.VMRemark inModel)
        {
            JsonResult json = new JsonResult();

            Model.MRemarks model = new Model.MRemarks
            {
                ArticleID = inModel.ArticleID,
                UID = 1,
                UName = "user1",
                Remark = inModel.Remark,
                RTime = DateTime.Now
            };

            int rid = Logic.LRemarks.InsertRemark(model);

            json.Data = rid;

            return json;
        }


    }
}