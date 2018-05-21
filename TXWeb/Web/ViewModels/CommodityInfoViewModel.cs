using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class CommodityInfoViewModel
    {
        public bool IsEdit { get; set; } = false;

        public Model.CommodityList CommodityInfo { get; set; } = new Model.CommodityList();
    }
}