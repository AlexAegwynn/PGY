using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Web.ViewModels;

namespace Web.Controllers
{
    public class RemarkController : BaseController
    {
        // GET: TestRemark
        public ActionResult Index(int inArticleID)
        {
            List<Model.MRemarks> list = Logic.LRemarks.GetRemarks(inArticleID);
            List<VMRemark> vList = new List<VMRemark>();
            foreach (var item in list)
            {
                VMRemark vModel = new VMRemark
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
            ViewBag.State = State;
            return View(vList);
        }

        public JsonResult InsertRemark(VMRemark inModel)
        {
            JsonResult json = new JsonResult();
            bool result = false;

            if (Session["LoginUser"] is VMUser user)
            {
                Model.MRemarks model = new Model.MRemarks
                {
                    ArticleID = inModel.ArticleID,
                    UID = user.UID,
                    UName = user.UserName,
                    Remark = inModel.Remark,
                    RTime = DateTime.Now
                };
                result = Logic.LRemarks.InsertRemark(model) > 0;
            }

            json.Data = result;

            return json;
        }

        public PartialViewResult GetReplies(int inRID)
        {
            List<Model.MReplies> list = Logic.LRemarks.GetReplies(inRID);
            List<VMReplies> vList = new List<VMReplies>();

            foreach (var item in list)
            {
                VMReplies vModel = new VMReplies
                {
                    RID = item.RID,
                    BeUID = item.BeUID,
                    UID = item.UID,
                    UName = item.UName,
                    BeUName = item.BeUName,
                    Remark = item.Remark,
                    RTime = item.RTime.ToString()
                };
                vList.Add(vModel);
            }

            return PartialView(vList);
        }

        public JsonResult InsertReply(VMReplies inModel)
        {
            JsonResult json = new JsonResult();
            bool result = false;

            if (Session["LoginUser"] is VMUser user)
            {
                Model.MReplies model = new Model.MReplies
                {
                    RID = inModel.RID,
                    BeUID = inModel.BeUID,
                    BeUName = inModel.BeUName,
                    UID = user.UID,
                    UName = user.UserName,
                    Remark = inModel.Remark,
                    RTime = DateTime.Now
                };
                result = Logic.LRemarks.InsertReply(model) > 0;
            }

            json.Data = result;

            return json;
        }

        public JsonResult GetLoginUser()
        {
            JsonResult json = new JsonResult
            {
                Data = Session["LoginUser"] != null
            };

            return json;
        }
    }
}