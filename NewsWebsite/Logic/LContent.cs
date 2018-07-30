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
        public static List<MContent> GetArticles()
        {
            return Data.DContent.GetArticles();
        }
    }
}
