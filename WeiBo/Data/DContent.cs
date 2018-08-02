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
        public static List<Model.MContent> GetContents()
        {
            string sql = @" SELECT * FROM wz_Content WHERE Origin = '微博博主' ";

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
