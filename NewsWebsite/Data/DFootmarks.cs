using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string sql = @" SELECT * FROM nw_Footmarks WHERE UID = @inUID ORDER BY MarkTime DESC ";

            SqlParameter para = new SqlParameter("@inUID", SqlDbType.Int, 32)
            {
                Value = inUID
            };

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, "", para);
            List<MFootmarks> list = new List<MFootmarks>();

            foreach (DataRow item in dt.Rows)
            {
                MFootmarks model = new MFootmarks
                {
                    FmID = Convert.ToInt32(item["FmID"]),
                    UID = Convert.ToInt32(item["UID"]),
                    ArticleID = Convert.ToInt64(item["ArticleID"]),
                    MarkTime = Convert.ToDateTime(item["MarkTime"]),
                    FmTitle = item["FmTitle"].ToString()
                };
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 根据用户ID和文章ID确认足迹是否存在
        /// </summary>
        /// <param name="inUID"></param>
        /// <param name="inArticleID"></param>
        /// <returns>足迹ID</returns>
        public static string ExistFootmark(int inUID, long inArticleID)
        {
            string sql = @" SELECT * FROM nw_Footmarks WHERE UID = @inUID AND ArticleID = @inArticleID ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inUID", SqlDbType.Int, 32),
                new SqlParameter("@inArticleID", SqlDbType.BigInt, 64)
            };
            paras[0].Value = inUID;
            paras[1].Value = inArticleID;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, "", paras);
            var result = "";

            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["FmID"].ToString();
            }

            return result;
        }

        /// <summary>
        /// 搜索足迹
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public static List<MFootmarks> SearchMarks(string search, int inUID)
        {
            string sql = @" SELECT * FROM nw_Footmarks WHERE UID = @inUID AND FmTitle LIKE '%' + @inSearch + '%' ORDER BY MarkTime DESC ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inSearch", SqlDbType.NVarChar),
                new SqlParameter("@inUID", SqlDbType.Int, 32)
            };
            paras[0].Value = search;
            paras[1].Value = inUID;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, "", paras);
            List<MFootmarks> list = new List<MFootmarks>();

            foreach (DataRow item in dt.Rows)
            {
                MFootmarks model = new MFootmarks
                {
                    FmID = Convert.ToInt32(item["FmID"]),
                    UID = Convert.ToInt32(item["UID"]),
                    ArticleID = Convert.ToInt64(item["ArticleID"]),
                    MarkTime = Convert.ToDateTime(item["MarkTime"]),
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
            sql.Append(" UID, ArticleID, MarkTime, FmTitle ) VALUES ( ");
            sql.Append(" @inUID, @inArticleID, @inMarkTime, @inFmTitle ) ");

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inUID", SqlDbType.Int, 32),
                new SqlParameter("@inArticleID", SqlDbType.BigInt, 64),
                new SqlParameter("@inMarkTime", SqlDbType.DateTime),
                new SqlParameter("@inFmTitle", SqlDbType.NVarChar, 200)
            };
            paras[0].Value = inModel.UID;
            paras[1].Value = inModel.ArticleID;
            paras[2].Value = inModel.MarkTime;
            paras[3].Value = inModel.FmTitle;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            return result;
        }

        /// <summary>
        /// 更新足迹记录
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int UpdateFootmark(MFootmarks inModel)
        {
            string sql = @" UPDATE nw_Footmarks SET MarkTime = @inMarkTime WHERE FmID = @inFmID ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inMarkTime", SqlDbType.DateTime),
                new SqlParameter("@inFmID", SqlDbType.Int, 32)
            };
            paras[0].Value = inModel.MarkTime;
            paras[1].Value = inModel.FmID;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
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
