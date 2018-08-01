using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace Data
{
    public class DContent
    {
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns></returns>
        public static List<MContent> GetArticles()
        {
            string sql = @" SELECT * FROM wz_Content WHERE Origin = '今日头条' ORDER BY ReleaseTime DESC ";

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql);
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
    }
}
