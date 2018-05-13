using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class WebSiteViewModel : Model.WebSiteInfo
    {
        public bool IsEdit { get; set; }
    }
}