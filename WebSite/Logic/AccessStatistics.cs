using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class AccessStatistics
    {
        /// <summary>
        /// 获取网站访问列表
        /// </summary>
        /// <param name="inWebSiteID"></param>
        /// <returns></returns>
        public static Model.AccessStatistics GetAS(int inWebSiteID)
        {
            return Data.AccessStatistics.GetAS(inWebSiteID);
        }

        /// <summary>
        /// 添加网站访问信息
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int InsertAS(Model.AccessStatistics inModel)
        {
            return Data.AccessStatistics.InsertAS(inModel);
        }

        /// <summary>
        /// 更新网站访问信息
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int UpdateAS(Model.AccessStatistics inModel)
        {
            return Data.AccessStatistics.UpdateAS(inModel);
        }
    }
}
