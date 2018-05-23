using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
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
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO Tx_LogList ( ");
            sql.Append(" UserID, UserName, Time, Message ");
            sql.Append(" ) VALUES ( ");
            sql.Append(" @inUserID, @inUserName, @inTime, @inMessage ) ");

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inUserID", SqlDbType.Int, 32),
                new SqlParameter("@inUserName", SqlDbType.NVarChar, 50),
                new SqlParameter("@inTime", SqlDbType.DateTime),
                new SqlParameter("@inMessage", SqlDbType.NVarChar, -1)
            };
            paras[0].Value = inUserID;
            paras[1].Value = inUserName;
            paras[2].Value = inTime;
            paras[3].Value = inMessage;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            return result;
        }
    }
}
