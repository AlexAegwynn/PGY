using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace Web.Timer
{
    public class TaskAction
    {
        public static void InsertAS(object source, ElapsedEventArgs e)
        {
            var list = Web.Controllers.HomeController.asList;
            Web.Controllers.HomeController.asList = new List<ViewModels.vm_AccessStatistics>();

            var mlist_2 = (from l in list where l.WebSiteID == "2" select l).ToList();
            if (mlist_2.Count > 0)
            {
                Model.AccessStatistics model = Logic.AccessStatistics.GetAS(2);
                Model.AccessStatistics as_2 = new Model.AccessStatistics
                {
                    WebSiteID = 2
                };

                if (model != null)
                {
                    as_2.ASCount = model.ASCount + mlist_2.Count;
                    Logic.AccessStatistics.UpdateAS(as_2);
                }
                else
                {
                    as_2.ASCount = mlist_2.Count;
                    Logic.AccessStatistics.InsertAS(as_2);
                }
            }

            var mlist_5 = (from l in list where l.WebSiteID == "5" select l).ToList();
            if (mlist_5.Count > 0)
            {
                Model.AccessStatistics model = Logic.AccessStatistics.GetAS(5);
                Model.AccessStatistics as_5 = new Model.AccessStatistics
                {
                    WebSiteID = 5
                };

                if (model != null)
                {
                    as_5.ASCount = model.ASCount + mlist_5.Count;
                    Logic.AccessStatistics.UpdateAS(as_5);
                }
                else
                {
                    as_5.ASCount = mlist_5.Count;
                    Logic.AccessStatistics.InsertAS(as_5);
                }
            }

        }

        public static void SetContent()
        {

        }
    }
}