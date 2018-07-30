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
        public static int DeleteFavorite(int inFaID)
        {
            return Data.DFavorites.DeleteFavorite(inFaID);
        }
    }
}
