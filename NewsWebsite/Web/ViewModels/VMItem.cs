using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMItem : Model.MItem
    {
        /// <summary>
        /// 折扣后的价格
        /// </summary>
        public string Price { get; set; }
    }
}