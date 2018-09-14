using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MRemarks
    {
        /// <summary>
        /// 主键 自增 评论ID
        /// </summary>
        public int RID { get; set; } = 0;

        /// <summary>
        /// 文章ID
        /// </summary>
        public long ArticleID { get; set; } = 0;

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UID { get; set; } = 0;

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UName { get; set; } = string.Empty;

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime RTime { get; set; } = DateTime.Now;
    }
}
