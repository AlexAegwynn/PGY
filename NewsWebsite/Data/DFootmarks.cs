using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    /// <summary>
    /// 足迹文章数据处理类
    /// </summary>
    public class DFootmarks
    {
        /// <summary>
        /// 获取足迹列表
        /// </summary>
        /// <param name="inUID"></param>
        /// <returns></returns>
        public static List<MFootmarks> GetFootmarks(int inUID)
        {
            string sql = @" SELECT * FROM nw_Footmarks WHERE UID = @inUID ";

            SqlParameter para = new SqlParameter("@inUID", SqlDbType.Int, 32)
            {
                Value = inUID
            };

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, para);
            List<MFootmarks> list = new List<MFootmarks>();

            foreach (DataRow item in dt.Rows)
            {
                MFootmarks model = new MFootmarks
                {
                    FmID = Convert.ToInt32(item["FmID"]),
                    UID = Convert.ToInt32(item["UID"]),
                    ArticleID = Convert.ToInt64(item["ArticleID"]),
                    FmTitle = item["FmTitle"].ToString()
                };
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 创建足迹记录
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int CreateFootmark(MFootmarks inModel)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO nw_Footmarks ( ");
            sql.Append(" UID, ArticleID, FmTitle ) VALUES ( ");
            sql.Append(" @inUID, @inArticleID, @inFmTitle ) ");

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inUID", SqlDbType.Int, 32),
                new SqlParameter("@inArticleID", SqlDbType.BigInt, 64),
                new SqlParameter("@inFmTitle", SqlDbType.NVarChar, 200)
            };
            paras[0].Value = inModel.UID;
            paras[1].Value = inModel.ArticleID;
            paras[2].Value = inModel.FmTitle;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            return result;
        }

        /// <summary>
        /// 删除足迹记录
        /// </summary>
        /// <param name="inFmID"></param>
        /// <returns></returns>
        public static int DeleteFootmark(int inFmID)
        {
            string sql = @" DELETE FROM nw_Footmarks WHERE FmID = @inFmID ";

            SqlParameter para = new SqlParameter("@inFmID", SqlDbType.Int, 32);
            para.Value = inFmID;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, para);

            return result;
        }
    }
}
