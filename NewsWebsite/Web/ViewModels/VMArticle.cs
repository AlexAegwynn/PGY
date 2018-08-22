using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMArticle : MContent
    {
        public new string ReleaseTime { get; set; } = string.Empty;

        public string ImgSrc { get; set; } = string.Empty;

        public bool IsFavorites { get; set; } = false;
    }
}