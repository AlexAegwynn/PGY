using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMFootmark
    {
        /// <summary>
        /// 足迹文章ID
        /// </summary>
        public int FmID { get; set; }

        /// <summary>
        /// 文章ID
        /// </summary>
        public long ArticleID { get; set; }

        /// <summary>
        /// 足迹文章标题
        /// </summary>
        public string FmTitle { get; set; }
    }
}