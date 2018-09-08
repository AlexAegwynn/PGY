using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LContent
    {
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns></returns>
        public static List<MContent> GetArticles(int inPageIndex, int domainId, string search)
        {
            return Data.DContent.GetArticles(inPageIndex, domainId, search);
        }

        /// <summary>
        /// 随机获取文章
        /// </summary>
        /// <param name="top"></param>
        /// <param name="domainId"></param>
        /// <returns></returns>
        public static List<MContent> GetRandomArticles(int top, int domainId)
        {
            return Data.DContent.GetRandomArticles(top, domainId);
        }

        /// <summary>
        /// 获取文章总数
        /// </summary>
        /// <returns></returns>
        public static int GetArticleTotal()
        {
            return Data.DContent.GetArticleTotal();
        }

        /// <summary>
        /// 根据 ArticleID 获取文章实体
        /// </summary>
        /// <param name="inArticleID"></param>
        /// <returns></returns>
        public static MContent GetArticle(long inArticleID)
        {
            return Data.DContent.GetArticle(inArticleID);
        }
    }
}
