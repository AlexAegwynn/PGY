using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DVisitorCount
    {
        /// <summary>
        /// 获取访问人数
        /// </summary>
        /// <returns></returns>
        public static Model.MVisitorCount GetVisitorCount()
        {
            string sql = @" SELECT * FROM nw_VisitorCount ";

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql);
            Model.MVisitorCount model = null;

            if (dt.Rows.Count > 0)
            {
                model = new Model.MVisitorCount
                {
                    VCID = Convert.ToInt32(dt.Rows[0]["VCID"]),
                    Count = Convert.ToInt32(dt.Rows[0]["Count"])
                };
            }

            return model;
        }

        /// <summary>
        /// 添加访问计数
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int InsertVC(Model.MVisitorCount inModel)
        {
            string sql = @" INSERT INTO nw_VisitorCount ( Count ) VALUES ( @inCount ) ";
            SqlParameter para = new SqlParameter("@inCount", SqlDbType.Int);
            para.Value = inModel.Count;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, para);
            return result;
        }

        /// <summary>
        /// 更新访问计数
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int UpdateVC(Model.MVisitorCount inModel)
        {
            string sql = @" UPDATE nw_VisitorCount SET Count = @inCount WHERE VCID = @inVCID ";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inCount", SqlDbType.Int),
                new SqlParameter("@inVCID", SqlDbType.Int)
            };
            paras[0].Value = inModel.Count;
            paras[1].Value = inModel.VCID;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return result;
        }
    }
}
