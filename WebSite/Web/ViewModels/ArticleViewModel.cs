using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class ArticleViewModel
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int ToPag { get; set; } = 0;

        /// <summary>
        /// 文章信息列表
        /// </summary>
        public List<Article> Article { get; set; }
    }

    public class Article
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public string ArticleID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string Conten { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public string ReleaseTime { get; set; }

        public string DomainName { get; set; }

        /// <summary>
        /// 发布者
        /// </summary>
        public string RAdminName { get; set; }

        /// <summary>
        /// APP名称
        /// </summary>
        public string APPName { get; set; }
    }
}