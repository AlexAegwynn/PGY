using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 文章实体类
    /// </summary>
    public class MContent
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public long ArticleID { get; set; } = 0;

        /// <summary>
        /// 发布时间
        /// </summary>
        public long ReleaseTime { get; set; } = 0;

        /// <summary>
        /// 分类ID
        /// </summary>
        public int DomainID { get; set; } = 1;

        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        public string Conten { get; set; } = string.Empty;
    }
}
