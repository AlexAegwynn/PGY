using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DCategory
    {
        /// <summary>
        /// 获取类目列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.MCategory> GetCatsList()
        {
            string sql = @" SELECT * FROM rpt_Category ";

            List<Model.MCategory> list = new List<Model.MCategory>();
            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql);

            foreach (DataRow item in dt.Rows)
            {
                Model.MCategory model = new Model.MCategory
                {
                    CatID = Convert.ToInt32(item["CatID"]),
                    CatName = item["CatName"].ToString()
                };
                list.Add(model);
            }

            return list;
        }
    }
}
