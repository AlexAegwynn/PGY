using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class vm_Img
    {
        public Guid WebSiteKey { get; set; }

        public string DomainName { get; set; }

        public string FileName { get; set; }

        public string ImgUrl { get; set; }
    }
}