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
            string sql = @" SELECT * FROM nw_Favorites WHERE UID = @inUID ORDER BY FavoritesTime DESC ";

            SqlParameter para = new SqlParameter("@inUID", SqlDbType.Int, 32);
            para.Value = inUID;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, "", para);
            List<MFavorites> list = new List<MFavorites>();

            foreach (DataRow item in dt.Rows)
            {
                MFavorites model = new MFavorites
                {
                    FaID = Convert.ToInt32(item["FaID"]),
                    UID = Convert.ToInt32(item["UID"]),
                    FavoritesTime = Convert.ToDateTime(item["FavoritesTime"]),
                    ArticleID = Convert.ToInt64(item["ArticleID"]),
                    FaTitle = item["FaTitle"].ToString()
                };
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 验证文章是否已收藏
        /// </summary>
        /// <param name="inArticleID"></param>
        /// <param name="inUID"></param>
        /// <returns></returns>
        public static bool ExistFavorites(long inArticleID, int inUID)
        {
            string sql = @" SELECT * FROM nw_Favorites WHERE ArticleID = @inArticleID AND UID = @inUID ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inArticleID", SqlDbType.BigInt, 64),
                new SqlParameter("@inUID", SqlDbType.Int)
            };
            paras[0].Value = inArticleID;
            paras[1].Value = inUID;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, "", paras);
            bool result = dt.Rows.Count > 0;

            return result;
        }

        /// <summary>
        /// 搜索收藏文章
        /// </summary>
        /// <param name="search"></param>
        /// <param name="inUID"></param>
        /// <returns></returns>
        public static List<MFavorites> SearchFav(string search, int inUID)
        {
            string sql = @" SELECT * FROM nw_Favorites WHERE UID = @inUID AND FaTitle LIKE '%' + @inSearch + '%' ORDER BY FavoritesTime DESC ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inUID", SqlDbType.Int,32),
                new SqlParameter("@inSearch", SqlDbType.NVarChar)
            };
            paras[0].Value = inUID;
            paras[1].Value = search;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, "", paras);
            List<MFavorites> list = new List<MFavorites>();

            foreach (DataRow item in dt.Rows)
            {
                MFavorites model = new MFavorites
                {
                    FaID = Convert.ToInt32(item["FaID"]),
                    UID = Convert.ToInt32(item["UID"]),
                    FavoritesTime = Convert.ToDateTime(item["FavoritesTime"]),
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
            sql.Append(" UID, ArticleID, FavoritesTime, FaTitle ) VALUES ( ");
            sql.Append(" @inUID, @inArticleID, @inFavoritesTime, @inFaTitle ) ");

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inUID", SqlDbType.Int, 32),
                new SqlParameter("@inArticleID", SqlDbType.BigInt, 64),
                new SqlParameter("@inFavoritesTime", SqlDbType.DateTime),
                new SqlParameter("@inFaTitle", SqlDbType.NVarChar, 200)
            };
            paras[0].Value = inModel.UID;
            paras[1].Value = inModel.ArticleID;
            paras[2].Value = inModel.FavoritesTime;
            paras[3].Value = inModel.FaTitle;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            return result;
        }

        /// <summary>
        /// 删除收藏记录
        /// </summary>
        /// <param name="inFaID"></param>
        /// <returns></returns>
        public static int DeleteFavorite(long inArticleID, int inUID)
        {
            string sql = @" DELETE FROM nw_Favorites WHERE ArticleID = @inArticleID AND UID = @inUID ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inArticleID", SqlDbType.BigInt, 64),
                new SqlParameter("@inUID", SqlDbType.Int)
            };
            paras[0].Value = inArticleID;
            paras[1].Value = inUID;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);

            return result;
        }
    }
}
