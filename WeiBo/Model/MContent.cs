using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MContent
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public long ArticleID { get; set; } = 0;

        /// <summary>
        /// 分类ID
        /// </summary>
        public int DomainID { get; set; } = 1;

        /// <summary>
        /// 内容
        /// </summary>
        public string Conten { get; set; } = string.Empty;
        
        /// <summary>
        /// 修饰符
        /// </summary>
        public string Abstract { get; set; } = string.Empty;

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; } = string.Empty;
    }
}
