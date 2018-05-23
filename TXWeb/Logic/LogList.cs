using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LogList
    {
        /// <summary>
        /// 插入日志
        /// </summary>
        /// <param name="inUserID"></param>
        /// <param name="inUserName"></param>
        /// <param name="inTime"></param>
        /// <param name="inMessage"></param>
        /// <returns></returns>
        public static int InsertLog(int inUserID, string inUserName, DateTime inTime, string inMessage)
        {
            return Data.LogList.InsertLog(inUserID, inUserName, inTime, inMessage);
        }
    }
}
