using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMRemark : Model.MRemarks
    {
        public new string RTime { get; set; }

        public int RepliesTotal { get; set; }
    }
}