using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class CommodityInfoViewModel
    {
        public Model.CommodityList CommodityInfo { get; set; } = new Model.CommodityList();
    }
}