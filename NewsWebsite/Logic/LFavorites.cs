using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// 收藏文章逻辑处理类
    /// </summary>
    public class LFavorites
    {
        /// <summary>
        /// 获取收藏列表
        /// </summary>
        /// <param name="inUID"></param>
        /// <returns></returns>
        public static List<MFavorites> GetFavorites(int inUID)
        {
            return Data.DFavorites.GetFavorites(inUID);
        }

        /// <summary>
        /// 验证文章是否已收藏
        /// </summary>
        /// <param name="inArticleID"></param>
        /// <param name="inUID"></param>
        /// <returns></returns>
        public static bool ExistFavorites(long inArticleID, int inUID)
        {
            return Data.DFavorites.ExistFavorites(inArticleID, inUID);
        }

        /// <summary>
        /// 搜索收藏文章
        /// </summary>
        /// <param name="search"></param>
        /// <param name="inUID"></param>
        /// <returns></returns>
        public static List<MFavorites> SearchFav(string search, int inUID)
        {
            return Data.DFavorites.SearchFav(search, inUID);
        }

        /// <summary>
        /// 创建收藏记录
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int CreateFavorite(MFavorites inModel)
        {
            return Data.DFavorites.CreateFavorite(inModel);
        }

        /// <summary>
        /// 删除收藏记录
        /// </summary>
        /// <param name="inFaID"></param>
        /// <returns></returns>
        public static int DeleteFavorite(long inArticleID, int inUID)
        {
            return Data.DFavorites.DeleteFavorite(inArticleID, inUID);
        }
    }
}
