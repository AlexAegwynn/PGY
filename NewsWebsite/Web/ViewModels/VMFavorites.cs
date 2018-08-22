using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMFavorites
    {
        public int FaID { get; set; }

        public long ArticleID { get; set; }

        public string FavoritesTime { get; set; }

        public string FaTitle { get; set; }
    }
}