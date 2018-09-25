using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LVisitorCount
    {
        /// <summary>
        /// 获取访问人数
        /// </summary>
        /// <returns></returns>
        public static Model.MVisitorCount GetVisitorCount()
        {
            return Data.DVisitorCount.GetVisitorCount();
        }

        /// <summary>
        /// 添加访问计数
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int InsertVC(Model.MVisitorCount inModel)
        {
            return Data.DVisitorCount.InsertVC(inModel);
        }

        /// <summary>
        /// 更新访问计数
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int UpdateVC(Model.MVisitorCount inModel)
        {
            return Data.DVisitorCount.UpdateVC(inModel);
        }
    }
}
