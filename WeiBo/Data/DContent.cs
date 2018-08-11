using System;
using System.Collections.Generic;
using System.Data;
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
            string sql = @" SELECT TOP 100 * FROM wz_Content WHERE Origin = '微博博主' ";

            if (domainid != 0)
            {
                sql += " AND DomainID = " + domainid;
            }
            else
            {
                sql = " SELECT TOP 100 * FROM wz_Content TABLESAMPLE(1 PERCENT) WHERE Origin = '微博博主' ";
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
                    ImgUrl = item["ImgUrl"].ToString()
                };
                list.Add(model);
            }

            return list;
        }
    }
}
