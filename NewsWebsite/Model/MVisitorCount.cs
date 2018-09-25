using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MVisitorCount
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int VCID { get; set; } = 0;

        /// <summary>
        /// 访问数量
        /// </summary>
        public int Count { get; set; } = 0;
    }
}
