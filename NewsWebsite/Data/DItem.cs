using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DItem
    {
        /// <summary>
        /// 根据类目ID获取商品列表
        /// </summary>
        /// <param name="catID"></param>
        /// <returns></returns>
        public static List<Model.MItem> GetItemsByCatID(long catID)
        {
            string sql = @" SELECT * FROM rpt_ItemsInfo WHERE CatID = @inCatID ";

            SqlParameter para = new SqlParameter("@inCatID", SqlDbType.BigInt);
            para.Value = catID;

            List<Model.MItem> list = new List<Model.MItem>();

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, "PGY_TK", para);
            foreach (DataRow item in dt.Rows)
            {
                Model.MItem model = new Model.MItem
                {
                    ID = Convert.ToInt64(item["ID"]),
                    Title = item["Title"].ToString(),
                    CatID = Convert.ToInt64(item["CatID"]),
                    ImgSmall = item["ImgSmall"].ToString(),
                    ClickUrl = item["ClickUrl"].ToString(),
                    TitleDescribe = item["TitleDescribe"].ToString()
                };
                list.Add(model);
            }

            return list;
        }
    }
}
