using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Data
{
    /// <summary>
    /// 收藏文章数据处理类
    /// </summary>
    public class DFavorites
    {
        /// <summary>
        /// 获取收藏列表
        /// </summary>
        /// <param name="inUID"></param>
        /// <returns></returns>
        public static List<MFavorites> GetFavorites(int inUID)
        {
            string sql = @" SELECT * FROM nw_Favorites WHERE UID = @inUID ";

            SqlParameter para = new SqlParameter("@inUID", SqlDbType.Int, 32);
            para.Value = inUID;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, para);
            List<MFavorites> list = new List<MFavorites>();

            foreach (DataRow item in dt.Rows)
            {
                MFavorites model = new MFavorites
                {
                    FaID = Convert.ToInt32(item["FaID"]),
                    UID = Convert.ToInt32(item["UID"]),
                    ArticleID = Convert.ToInt64(item["ArticleID"]),
                    FaTitle = item["FaTitle"].ToString()
                };
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 创建收藏记录
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int CreateFavorite(MFavorites inModel)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO nw_Favorites ( ");
            sql.Append(" UID, ArticleID, FaTitle ) VALUES ( ");
            sql.Append(" @inUID, @inArticleID, @inFaTitle ) ");

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inUID", SqlDbType.Int, 32),
                new SqlParameter("@inArticleID", SqlDbType.BigInt, 64),
                new SqlParameter("@inFaTitle", SqlDbType.NVarChar, 200)
            };
            paras[0].Value = inModel.UID;
            paras[1].Value = inModel.ArticleID;
            paras[2].Value = inModel.FaTitle;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            return result;
        }

        /// <summary>
        /// 删除收藏记录
        /// </summary>
        /// <param name="inFaID"></param>
        /// <returns></returns>
        public static int DeleteFavorite(int inFaID)
        {
            string sql = @" DELETE FROM nw_Favorites WHERE FaID = @inFaID ";

            SqlParameter para = new SqlParameter("@inFaID", SqlDbType.Int, 32);
            para.Value = inFaID;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, para);

            return result;
        }
    }
}
