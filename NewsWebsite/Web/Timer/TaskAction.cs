using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace Web.Timer
{
    public class TaskAction
    {
        public static void InsertVC(object source, ElapsedEventArgs e)
        {
            var list = Controllers.HomeController.vcList;
            Controllers.HomeController.vcList = new List<string>();

            if (list.Count > 0)
            {
                Model.MVisitorCount model = Logic.LVisitorCount.GetVisitorCount();

                if (model != null)
                {
                    model.Count = model.Count + list.Count;
                    Logic.LVisitorCount.UpdateVC(model);
                }
                else
                {
                    model = new Model.MVisitorCount
                    {
                        Count = list.Count
                    };
                    Logic.LVisitorCount.InsertVC(model);
                }
            }
        }

        public static void SetContent()
        {

        }
    }
}