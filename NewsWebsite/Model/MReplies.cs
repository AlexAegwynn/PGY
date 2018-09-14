using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MReplies
    {
        /// <summary>
        /// 回复ID
        /// </summary>
        public int ReID { get; set; } = 0;

        /// <summary>
        /// 评论ID
        /// </summary>
        public int RID { get; set; } = 0;

        /// <summary>
        /// 被回复的用户ID
        /// </summary>
        public int BeUID { get; set; } = 0;

        /// <summary>
        /// 被回复的用户名
        /// </summary>
        public string BeUName { get; set; } = string.Empty;

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UID { get; set; } = 0;

        /// <summary>
        /// 用户名
        /// </summary>
        public string UName { get; set; } = string.Empty;

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 评论时间
        /// </summary>
        public int RTime { get; set; } = 0;
    }
}
