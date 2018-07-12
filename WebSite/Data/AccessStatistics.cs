using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class AccessStatistics
    {
        public static Model.AccessStatistics GetAS(int inWebSiteID)
        {
            string sql = @" SELECT * FROM Web_AccessStatistics WHERE WebSiteID = @inWebSiteID ";

            SqlParameter para = new SqlParameter("@inWebSiteID", SqlDbType.Int, 32)
            {
                Value = inWebSiteID
            };

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, para);
            Model.AccessStatistics model = null;

            if (dt.Rows.Count > 0)
            {
                model = new Model.AccessStatistics
                {
                    WebSiteID = Convert.ToInt32(dt.Rows[0]["WebSiteID"]),
                    ASCount = Convert.ToInt32(dt.Rows[0]["ASCount"])
                };
            }

            return model;
        }

        public static int InsertAS(Model.AccessStatistics inModel)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO Web_AccessStatistics ( ");
            sql.Append(" WebSiteID, ASCount ) VALUES ( ");
            sql.Append(" @inWebSiteID, @inASCount ) ");

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inWebSiteID", SqlDbType.Int, 32),
                new SqlParameter("@inASCount", SqlDbType.BigInt, 64)
            };
            paras[0].Value = inModel.WebSiteID;
            paras[1].Value = inModel.ASCount;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            return result;
        }

        public static int UpdateAS(Model.AccessStatistics inModel)
        {
            string sql = @" UPDATE Web_AccessStatistics SET ASCount = @inASCount WHERE WebSiteID = @inWebSiteID ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inWebSiteID", SqlDbType.Int, 32),
                new SqlParameter("@inASCount", SqlDbType.BigInt, 64)
            };
            paras[0].Value = inModel.WebSiteID;
            paras[1].Value = inModel.ASCount;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            return result;
        }
    }
}
