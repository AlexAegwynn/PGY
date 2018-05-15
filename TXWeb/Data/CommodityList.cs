using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class CommodityList
    {
        /// <summary>
        /// 获取所有商品列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.CommodityList> GetCommodityList()
        {
            string sql = @" SELECT * FROM Tx_CommodityList ";

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql);

            return GetCommodities(dt);
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="inCommodityID"></param>
        /// <returns></returns>
        public static Model.CommodityList GetCommodity(int inCommodityID)
        {
            string sql = @" SELECT * FROM Tx_CommodityList WHERE CommodityID = @inCommodityID ";

            SqlParameter para = new SqlParameter("@inCommodityID", SqlDbType.Int, 32);
            para.Value = inCommodityID;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, para);
            List<Model.CommodityList> list = GetCommodities(dt);

            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 私有方法，获取商品列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static List<Model.CommodityList> GetCommodities(DataTable dt)
        {
            List<Model.CommodityList> list = new List<Model.CommodityList>();

            foreach (DataRow item in dt.Rows)
            {
                Model.CommodityList model = new Model.CommodityList
                {
                    CommodityID = Convert.ToInt32(item["CommodityID"]),
                    Title = item["Title"].ToString(),
                    Category = item["Category"].ToString(),
                    Price = Convert.ToInt32(item["Price"]),
                    TxPrice = Convert.ToInt32(item["TxPrice"]),
                    Unit = item["Unit"].ToString(),
                    ImgUrl = item["ImgUrl"].ToString(),
                    Description = item["Description"].ToString()
                };

                list.Add(model);
            }

            return list;
        }
    }
}
