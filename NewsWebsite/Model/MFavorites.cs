using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 收藏文章实体类
    /// </summary>
    public class MFavorites
    {
        /// <summary>
        /// 收藏文章ID
        /// </summary>
        public int FaID { get; set; } = 0;

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UID { get; set; } = 0;

        /// <summary>
        /// 文章ID
        /// </summary>
        public long ArticleID { get; set; } = 0;

        /// <summary>
        /// 收藏文章标题
        /// </summary>
        public string FaTitel { get; set; } = string.Empty;
    }
}
