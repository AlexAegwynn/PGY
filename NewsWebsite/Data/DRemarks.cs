using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DRemarks
    {
        /// <summary>
        /// 获取文章评论
        /// </summary>
        /// <param name="inArticleID"></param>
        /// <returns></returns>
        public static List<Model.MRemarks> GetRemarks(long inArticleID)
        {
            string sql = @" SELECT * FROM nw_Remarks WHERE ArticleID = @inArticleID ";

            SqlParameter para = new SqlParameter("@inArticleID", SqlDbType.BigInt)
            {
                Value = inArticleID
            };

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, "", para);
            List<Model.MRemarks> list = new List<Model.MRemarks>();

            foreach (DataRow item in dt.Rows)
            {
                Model.MRemarks model = new Model.MRemarks
                {
                    RID = Convert.ToInt32(item["RID"]),
                    ArticleID = Convert.ToInt64(item["ArticleID"]),
                    UID = Convert.ToInt32(item["UID"]),
                    UName = item["UName"].ToString(),
                    Remark = item["Remark"].ToString(),
                    RTime = Convert.ToDateTime(item["RTime"])
                };
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int InsertRemark(Model.MRemarks inModel)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO nw_Remarks ( ");
            sql.Append(" ArticleID, UID, UName, Remark, RTime ) VALUES ( ");
            sql.Append(" @inArticleID, @inUID, @inUName, @inRemark, @inRTime ) ");
            //sql.Append(" SELECT @@IDENTITY AS RID ");

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inArticleID", SqlDbType.BigInt),
                new SqlParameter("@inUID", SqlDbType.Int),
                new SqlParameter("@inUName", SqlDbType.NVarChar, 50),
                new SqlParameter("@inRemark", SqlDbType.NVarChar, 500),
                new SqlParameter("@inRTime", SqlDbType.DateTime)
            };
            paras[0].Value = inModel.ArticleID;
            paras[1].Value = inModel.UID;
            paras[2].Value = inModel.UName;
            paras[3].Value = inModel.Remark;
            paras[4].Value = inModel.RTime;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            //DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql.ToString(), "", paras);
            //int result = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["RID"]) : 0;

            return result;
        }

        #region Reply
        /// <summary>
        /// 获取评论回复列表
        /// </summary>
        /// <param name="inRID"></param>
        /// <returns></returns>
        public static List<Model.MReplies> GetReplies(int inRID)
        {
            string sql = @" SELECT * FROM nw_Replies WHERE RID = @inRID ORDER BY RTime ASC ";
            SqlParameter para = new SqlParameter("@inRID", SqlDbType.Int)
            {
                Value = inRID
            };
            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, "", para);
            List<Model.MReplies> list = new List<Model.MReplies>();

            foreach (DataRow item in dt.Rows)
            {
                Model.MReplies model = new Model.MReplies
                {
                    ReID = Convert.ToInt32(item["ReID"]),
                    RID = Convert.ToInt32(item["ReID"]),
                    BeUID = Convert.ToInt32(item["ReID"]),
                    BeUName = item["ReID"].ToString(),
                    UID = Convert.ToInt32(item["ReID"]),
                    UName = item["ReID"].ToString(),
                    Remark = item["ReID"].ToString(),
                    RTime = Convert.ToInt32(item["ReID"])
                };
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 获取回复数
        /// </summary>
        /// <param name="inRID"></param>
        /// <returns></returns>
        public static int GetReplyTotal(int inRID)
        {
            string sql = @" SELECT COUNT(ReID) AS Total FROM nw_Replies WHERE RID = @inRID ";
            SqlParameter para = new SqlParameter("@inRID", SqlDbType.Int)
            {
                Value = inRID
            };
            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, "", para);
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["Total"]) : 0;
        }
        #endregion
    }
}
