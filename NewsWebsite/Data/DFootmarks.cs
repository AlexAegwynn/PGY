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


    }
}
