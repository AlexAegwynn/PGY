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
    public class DContent
    {
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns></returns>
        public static List<MContent> GetArticles(int inPageIndex, int domainId, string search)
        {
            int pagesize = 10;
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT TOP(" + pagesize + ") * FROM ( ");
            sql.Append(" SELECT ROW_NUMBER() OVER( ORDER BY ArticleID DESC )  ");
            sql.Append(" AS ROWNUMBER,* FROM wz_Content WITH(NOLOCK) WHERE Origin = '今日头条' ");
            if (domainId != 0)
            {
                sql.Append("  AND DomainID = " + domainId + " ");
            }
            if (!string.IsNullOrEmpty(search))
            {
                sql.Append(" AND Title LIKE '%" + search + "%' ");
            }
            sql.Append(" ) A WHERE A.ROWNUMBER > ((" + inPageIndex + ") * " + pagesize + ") ");

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql.ToString());
            List<MContent> list = new List<MContent>();

            foreach (DataRow item in dt.Rows)
            {
                MContent model = new MContent
                {
                    ArticleID = Convert.ToInt32(item["ArticleID"]),
                    Title = item["Title"].ToString(),
                    ReleaseTime = Convert.ToInt64(item["ReleaseTime"]),
                    DomainID = Convert.ToInt32(item["DomainID"]),
                    Conten = item["Conten"].ToString()
                };
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 随机获取文章
        /// </summary>
        /// <param name="top"></param>
        /// <param name="domainId"></param>
        /// <returns></returns>
        public static List<MContent> GetRandomArticles(int top, int domainId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT TOP " + top + " * FROM wz_Content WHERE Origin = '今日头条' ");
            if (domainId != 0)
            {
                sql.Append(" AND DomainID = " + domainId);
            }
            sql.Append(" ORDER BY NEWID() ");

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql.ToString());
            List<MContent> list = new List<MContent>();

            foreach (DataRow item in dt.Rows)
            {
                MContent model = new MContent
                {
                    ArticleID = Convert.ToInt32(item["ArticleID"]),
                    Title = item["Title"].ToString(),
                    ReleaseTime = Convert.ToInt64(item["ReleaseTime"]),
                    DomainID = Convert.ToInt32(item["DomainID"]),
                    Conten = item["Conten"].ToString()
                };
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 获取文章总数
        /// </summary>
        /// <returns></returns>
        public static int GetArticleTotal()
        {
            string sql = @" SELECT COUNT(ArticleID) as Total FROM wz_Content WHERE Origin = '今日头条' ";

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql);
            int total = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["Total"]) : 0;

            return total;
        }

        /// <summary>
        /// 根据 ArticleID 获取文章实体
        /// </summary>
        /// <param name="inArticleID"></param>
        /// <returns></returns>
        public static MContent GetArticle(long inArticleID)
        {
            string sql = @" SELECT * FROM wz_Content WHERE Origin = '今日头条' AND ArticleID = @inArticleID ";

            SqlParameter para = new SqlParameter("@inArticleID", SqlDbType.BigInt)
            {
                Value = inArticleID
            };

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, "", para);
            MContent model = null;
            if (dt.Rows.Count > 0)
            {
                model = new MContent
                {
                    ArticleID = Convert.ToInt64(dt.Rows[0]["ArticleID"]),
                    DomainID = Convert.ToInt32(dt.Rows[0]["DomainID"]),
                    Title = dt.Rows[0]["Title"].ToString(),
                    Conten = dt.Rows[0]["Conten"].ToString(),
                    ReleaseTime = Convert.ToInt64(dt.Rows[0]["ReleaseTime"])
                };
            }
            return model;
        }
    }
}
