using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class CategoryViewModel
    {
        /// <summary>
        /// 分类 ID
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// true 高亮，false 正常
        /// </summary>
        public bool State { get; set; }
    }
}