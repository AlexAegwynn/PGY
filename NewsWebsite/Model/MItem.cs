using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MItem
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public long ID { get; set; } = 0;

        /// <summary>
        /// 商品标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 分类ID
        /// </summary>
        public long CatID { get; set; } = 0;

        /// <summary>
        /// 商品图片
        /// </summary>
        public string ImgSmall { get; set; } = string.Empty;

        /// <summary>
        /// 商品链接
        /// </summary>
        public string ClickUrl { get; set; } = string.Empty;

        /// <summary>
        /// 标题描述
        /// </summary>
        public string TitleDescribe { get; set; } = string.Empty;
    }
}
