using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class PictureViewModel
    {
        public string PictureID { get; set; }

        public string Title { get; set; }

        public List<Conten> Conten { get; set; }

        public string ReleaseTime { get; set; }

        public string DomainName { get; set; }

        public string RAdminName { get; set; }

        public string APPName { get; set; }
    }

    public class Conten
    {
        public string Src { get; set; }
        public string Desc { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
    }
}