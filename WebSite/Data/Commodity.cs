using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class Commodity
    {
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="inRows">获取商品数</param>
        /// <param name="inPageCount">跳过的商品数</param>
        /// <param name="inWebID">网站ID</param>
        /// <returns></returns>
        public static List<Model.ItemsInfo> GetItemsInfos(int inRows, int inPageCount, int inWebID, string inSearch)
        {
            #region SQL
            string str = " NumIID,Title,TitleSub,UrlShort,PriceNow,SalesCount,ClickUrl,CommissionRate," +
                                                                    "CouponMoney,ImgUrl,ImgSmall,RemainCount,TotalCount,CouponInfo,IsEnable";
            
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT TOP " + inRows + str + " FROM Web_ItemWeb a ");
            sql.Append(" LEFT JOIN rpt_ItemsInfo b ON a.IID = b.ID ");
            sql.Append(" WHERE a.WID = @inWebID AND b.Title LIKE '%'+ @inSearch +'%' ");
            sql.Append(" AND a.ID NOT IN (  ");
            sql.Append(" SELECT TOP " + inPageCount + " a.ID FROM Web_ItemWeb a ");
            sql.Append(" LEFT JOIN rpt_ItemsInfo b ON a.IID = b.ID ");
            sql.Append(" WHERE a.WID = @inWebID AND b.Title LIKE '%'+ @inSearch +'%' ) ");
            sql.Append(" ORDER BY a.ID ASC ");
            #endregion

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inWebID", SqlDbType.Int, 32),
                new SqlParameter("@inSearch", SqlDbType.NVarChar, 50)
            };
            paras[0].Value = inWebID;
            paras[1].Value = inSearch;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql.ToString(), paras);
            List<Model.ItemsInfo> list = new List<Model.ItemsInfo>();

            string numIIDs = string.Empty;

            foreach (DataRow item in dt.Rows)
            {
                Model.ItemsInfo model = new Model.ItemsInfo
                {
                    //ID = Convert.ToInt64(item["ID"]),
                    NumIID = Convert.ToInt64(item["NumIID"]),
                    Title = item["Title"].ToString(),
                    //KeyWordStr = item["KeyWordStr"].ToString(),
                    TitleSub = item["TitleSub"].ToString(),
                    //IsPush = Convert.ToInt32(item["IsPush"]),
                    //PushTime = Convert.ToInt64(item["PushTime"]),
                    //TitleDescribe = item["TitleDescribe"].ToString(),
                    //CatID = Convert.ToInt64(item["CatID"]),
                    //Navigation = Convert.ToInt32(item["Navigation"]),
                    //OrderUrl = item["OrderUrl"].ToString(),
                    ImgUrl = item["ImgUrl"].ToString(),
                    ImgSmall = item["ImgSmall"].ToString(),
                    PriceNow = Convert.ToSingle(item["PriceNow"]),
                    //IsTmall = Convert.ToInt32(item["IsTmall"]),
                    SalesCount = Convert.ToInt32(item["SalesCount"]),
                    //IsGood = Convert.ToInt32(item["IsGood"]),
                    //CreateTime = Convert.ToInt64(item["CreateTime"]),
                    //UpdateTime = Convert.ToInt64(item["UpdateTime"]),
                    IsEnable = Convert.ToInt32(item["IsEnable"]),
                    //ActivityID = item["ActivityID"].ToString(),
                    //TimeStart = Convert.ToInt64(item["TimeStart"]),
                    //TimeEnd = Convert.ToInt64(item["TimeEnd"]),
                    ClickUrl = item["ClickUrl"].ToString(),
                    UrlShort = item["UrlShort"].ToString(),
                    TotalCount = Convert.ToInt32(item["TotalCount"]),
                    RemainCount = Convert.ToInt32(item["RemainCount"]),
                    CommissionRate = Convert.ToSingle(item["CommissionRate"]),
                    //Commission = Convert.ToSingle(item["Commission"]),
                    CouponInfo = item["CouponInfo"].ToString(),
                    CouponMoney = Convert.ToSingle(item["CouponMoney"]),
                    //TimeUpdate = Convert.ToInt64(item["TimeUpdate"]),
                    //CouponType = Convert.ToInt32(item["CouponType"]),
                    //UseCount = Convert.ToInt32(item["UseCount"]),
                    //Nick = item["Nick"].ToString(),
                    //SellerID = Convert.ToInt64(item["SellerID"])
                };
                numIIDs += model.NumIID + ",";
                list.Add(model);
            }

            List <Top.Api.Domain.NTbkItem> zkList = Common.TBApi.GetZKPice(numIIDs);

            for (int i = 0; i < list.Count; i++)
            {
                list[i].ZKPice = zkList[i].ZkFinalPrice;
            }

            return list;
        }

        /// <summary>
        /// 获取商品总数
        /// </summary>
        /// <param name="inWebSiteID"></param>
        /// <param name="inSearch"></param>
        /// <returns></returns>
        public static int GetTotal(int inWebSiteID, string inSearch)
        {
            string sql = @" SELECT COUNT(*) AS Total FROM Web_ItemWeb a LEFT JOIN rpt_ItemsInfo b 
                                                        ON a.IID = b.ID WHERE a.WID = @inWebSiteID AND Title LIKE '%' + @inSearch + '%' ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inWebSiteID", SqlDbType.Int, 32),
                new SqlParameter("@inSearch", SqlDbType.NVarChar, 50)
            };
            paras[0].Value = inWebSiteID;
            paras[1].Value = inSearch;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, paras);

            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["Total"]) : 0;
        }

        /// <summary>
        /// 获取单个商品
        /// </summary>
        /// <param name="inID">商品ID</param>
        /// <returns></returns>
        public static Model.ItemsInfo GetItemsInfo(int inID)
        {
            string sql = @" SELECT * FROM rpt_ItemsInfo Where ID = @inID ";

            SqlParameter para = new SqlParameter("@inID", SqlDbType.Int, 32);
            para.Value = inID;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, para);

            Model.ItemsInfo model = new Model.ItemsInfo
            {
                ID = Convert.ToInt64(dt.Rows[0]["ID"]),
                NumIID = Convert.ToInt64(dt.Rows[0]["NumIID"]),
                Title = dt.Rows[0]["Title"].ToString(),
                KeyWordStr = dt.Rows[0]["KeyWordStr"].ToString(),
                TitleSub = dt.Rows[0]["TitleSub"].ToString(),
                IsPush = Convert.ToInt32(dt.Rows[0]["IsPush"]),
                PushTime = Convert.ToInt64(dt.Rows[0]["PushTime"]),
                TitleDescribe = dt.Rows[0]["TitleDescribe"].ToString(),
                CatID = Convert.ToInt64(dt.Rows[0]["NumIID"]),
                Navigation = Convert.ToInt32(dt.Rows[0]["Navigation"]),
                //OrderUrl = dt.Rows[0]["OrderUrl"].ToString(),
                ImgUrl = dt.Rows[0]["ImgUrl"].ToString(),
                ImgSmall = dt.Rows[0]["ImgSmall"].ToString(),
                PriceNow = Convert.ToSingle(dt.Rows[0]["PriceNow"]),
                IsTmall = Convert.ToInt32(dt.Rows[0]["IsTmall"]),
                SalesCount = Convert.ToInt32(dt.Rows[0]["SalesCount"]),
                IsGood = Convert.ToInt32(dt.Rows[0]["IsGood"]),
                CreateTime = Convert.ToInt64(dt.Rows[0]["CreateTime"]),
                UpdateTime = Convert.ToInt64(dt.Rows[0]["UpdateTime"]),
                IsEnable = Convert.ToInt32(dt.Rows[0]["IsEnable"]),
                ActivityID = dt.Rows[0]["ActivityID"].ToString(),
                TimeStart = Convert.ToInt64(dt.Rows[0]["TimeStart"]),
                TimeEnd = Convert.ToInt64(dt.Rows[0]["TimeEnd"]),
                ClickUrl = dt.Rows[0]["ClickUrl"].ToString(),
                UrlShort = dt.Rows[0]["UrlShort"].ToString(),
                TotalCount = Convert.ToInt32(dt.Rows[0]["TotalCount"]),
                RemainCount = Convert.ToInt32(dt.Rows[0]["RemainCount"]),
                CommissionRate = Convert.ToSingle(dt.Rows[0]["CommissionRate"]),
                Commission = Convert.ToSingle(dt.Rows[0]["Commission"]),
                CouponInfo = dt.Rows[0]["CouponInfo"].ToString(),
                CouponMoney = Convert.ToSingle(dt.Rows[0]["CouponMoney"]),
                TimeUpdate = Convert.ToInt64(dt.Rows[0]["TimeUpdate"]),
                CouponType = Convert.ToInt32(dt.Rows[0]["CouponType"]),
                UseCount = Convert.ToInt32(dt.Rows[0]["UseCount"]),
                Nick = dt.Rows[0]["Nick"].ToString(),
                SellerID = Convert.ToInt64(dt.Rows[0]["SellerID"])
            };

            return model;
        }
    }
}
