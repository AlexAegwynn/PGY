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
        /// 获取内容列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.MContent> GetContents(int domainid)
        {
            string sql = @" SELECT TOP 500 * FROM wz_Content WHERE Origin = '微博博主' ";

            if (domainid != 0)
            {
                sql += " AND DomainID = " + domainid;
            }
            else
            {
                sql = " SELECT TOP 500 * FROM wz_Content TABLESAMPLE(1 PERCENT) WHERE Origin = '微博博主' ";
                //sql = @" SELECT TOP 100 * FROM wz_Content WHERE Origin = '微博博主' ORDER BY NEWID() ";
            }

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql);
            List<Model.MContent> list = new List<Model.MContent>();

            foreach (DataRow item in dt.Rows)
            {
                Model.MContent model = new Model.MContent
                {
                    ArticleID = Convert.ToInt64(item["ArticleID"]),
                    DomainID = Convert.ToInt32(item["DomainID"]),
                    Conten = item["Conten"].ToString(),
                    Abstract = item["Abstract"].ToString(),
                    ImgUrl = item["ImgUrl"].ToString()
                };
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 更新内容
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int UpdateVideo(Model.MContent inModel)
        {
            string sql = @" UPDATE wz_Content SET ImgUrl = @inImgUrl WHERE ArticleID = @inArticleID ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inImgUrl", SqlDbType.VarChar, 1000),
                new SqlParameter("@inArticleID", SqlDbType.BigInt)
            };
            paras[0].Value = inModel.ImgUrl;
            paras[1].Value = inModel.ArticleID;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);

            return result;
        }

        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        public static int DeleteVideo(int articleID)
        {
            string sql = @" UPDATE wz_Content SET ImgUrl = '' WHERE ArticleID = @inArticleID ";

            SqlParameter para = new SqlParameter("@inArticleID", SqlDbType.BigInt);
            para.Value = articleID;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, para);

            return result;
        }
    }
}
