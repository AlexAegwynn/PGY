using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ItemWeb
    {
        /// <summary>
        /// 信息编号,自增
        /// </summary>
        public int ID { set; get; } = 0;
        /// <summary>
        /// 网站编号
        /// </summary>
        public int WID { get; set; } = 0;
        /// <summary>
        /// 商品编号
        /// </summary>
        public long IID { get; set; } = 0;
    }
}
