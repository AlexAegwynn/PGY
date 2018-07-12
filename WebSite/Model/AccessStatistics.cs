using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AccessStatistics
    {
        /// <summary>
        /// 网站ID
        /// </summary>
        public int WebSiteID { get; set; } = 2;

        /// <summary>
        /// 访问量
        /// </summary>
        public long ASCount { get; set; } = 0;
    }
}
