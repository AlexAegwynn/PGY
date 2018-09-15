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
        /// 被回复的用户ID
        /// </summary>
        public int BeUID { get; set; }

        /// <summary>
        /// 被回复的用户名
        /// </summary>
        public string BeUName { get; set; }

        /// <summary>
        /// 回复的用户名
        /// </summary>
        public int UID { get; set; }

        /// <summary>
        /// 回复的用户名
        /// </summary>
        public string UName { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 回复时间
        /// </summary>
        public string RTime { get; set; }
    }
}