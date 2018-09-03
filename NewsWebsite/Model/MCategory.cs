using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MCategory
    {
        /// <summary>
        /// 类目编号
        /// </summary>
        public long CatID { get; set; } = 0;

        /// <summary>
        /// 类目名称
        /// </summary>
        public string CatName { get; set; } = string.Empty;
    }
}
