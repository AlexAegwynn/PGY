using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserSession
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid SID { get; set; } = Guid.Empty;

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; } = 0;

        /// <summary>
        /// SessionID
        /// </summary>
        public int SessionID { get; set; } = 0;
    }
}
