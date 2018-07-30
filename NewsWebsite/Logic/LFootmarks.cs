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
        /// 创建足迹记录
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int CreateFootmark(MFootmarks inModel)
        {
            return Data.DFootmarks.CreateFootmark(inModel);
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
