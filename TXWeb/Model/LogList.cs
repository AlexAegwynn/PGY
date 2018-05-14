using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LogList
    {
        /// <summary>
        /// 日志ID，主键，自增
        /// </summary>
        public int LogID { get; set; } = 0;

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; } = Convert.ToDateTime("2000-01-01");

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
