using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class vm_WebSite : Model.WebSiteInfo
    {
        public bool IsEdit { get; set; }
    }
}