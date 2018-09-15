using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMReplies
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        public int RID { get; set; }

        /// <summary>
        /// 被回复的用户名
        /// </summary>
        public int BeUName { get; set; }

        /// <summary>
        /// 回复的用户名
        /// </summary>
        public int UName { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        public int Remark { get; set; }

        /// <summary>
        /// 回复时间
        /// </summary>
        public int RTime { get; set; }
    }
}