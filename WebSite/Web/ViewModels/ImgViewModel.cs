using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class ImgViewModel
    {
        public Guid WebSiteKey { get; set; }

        public string DomainName { get; set; }

        public string FileName { get; set; }

        public string ImgUrl { get; set; }
    }
}