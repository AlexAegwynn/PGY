using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// 足迹文章逻辑处理类
    /// </summary>
    public class LFootmarks
    {
        /// <summary>
        /// 获取足迹列表
        /// </summary>
        /// <param name="inUID"></param>
        /// <returns></returns>
        public static List<MFootmarks> GetFootmarks(int inUID)
        {
            return Data.DFootmarks.GetFootmarks(inUID);
        }

        /// <summary>
        /// 根据用户ID和文章ID确认足迹是否存在
        /// </summary>
        /// <param name="inUID"></param>
        /// <param name="inArticleID"></param>
        /// <returns></returns>
        public static string ExistFootmark(int inUID, long inArticleID)
        {
            return Data.DFootmarks.ExistFootmark(inUID, inArticleID);
        }

        /// <summary>
        /// 搜索足迹
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static List<MFootmarks> SearchMarks(string search, int inUID)
        {
            return Data.DFootmarks.SearchMarks(search, inUID);
        }

        /// <summary>
        /// 创建足迹记录
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int CreateFootmark(MFootmarks inModel)
        {
            return Data.DFootmarks.CreateFootmark(inModel);
        }

        /// <summary>
        /// 更新足迹记录
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int UpdateFootmark(MFootmarks inModel)
        {
            return Data.DFootmarks.UpdateFootmark(inModel);
        }

        /// <summary>
        /// 删除足迹记录
        /// </summary>
        /// <param name="inFmID"></param>
        /// <returns></returns>
        public static int DeleteFootmark(int inFmID)
        {
            return Data.DFootmarks.DeleteFootmark(inFmID);
        }
    }
}
