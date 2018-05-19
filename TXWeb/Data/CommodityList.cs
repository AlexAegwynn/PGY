﻿using System;
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
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT a.UserID, UserName, b.* FROM Tx_UserCommodityList a ");
            sql.Append(" LEFT JOIN Tx_CommodityList b ON a.CommodityID = b.CommodityID ");
            sql.Append(" LEFT JOIN Tx_UserList c ON a.UserID = c.UserID ");

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql.ToString());

            return GetCommodities(dt);
        }

        /// <summary>
        /// 获取用户商品列表
        /// </summary>
        /// <param name="inUserID"></param>
        /// <returns></returns>
        public static List<Model.CommodityList> GetUserCommodityList(int inUserID)
        {
            string sql = @" SELECT a.UserID, c.UserName, b.* FROM Tx_UserCommodityList a " +
                                  " LEFT JOIN Tx_CommodityList b ON a.CommodityID = b.CommodityID " +
                                  " LEFT JOIN Tx_UserList c ON c.UserID = a.UserID WHERE a.UserID = @inUserID ";

            SqlParameter para = new SqlParameter("@inUserID", SqlDbType.Int, 32);
            para.Value = inUserID;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, para);

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
            Model.CommodityList model = null;

            if (dt.Rows.Count > 0)
            {
                Model.CommodityList item = new Model.CommodityList
                {
                    CommodityID = Convert.ToInt32(dt.Rows[0]["CommodityID"]),
                    Title = dt.Rows[0]["Title"].ToString(),
                    Category = dt.Rows[0]["Category"].ToString(),
                    Price = Convert.ToInt32(dt.Rows[0]["Price"]),
                    TxPrice = Convert.ToInt32(dt.Rows[0]["TxPrice"]),
                    Unit = dt.Rows[0]["Unit"].ToString(),
                    ImgUrl = dt.Rows[0]["ImgUrl"].ToString(),
                    Description = dt.Rows[0]["Description"].ToString()
                };
                model = item;
            }

            return model;
        }

        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int UpdateCommodity(Model.CommodityList inModel)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" UPDATE Tx_CommodityList SET ");
            sql.Append(" Title = @inTitle, Category = @inCategory, Price = @inPrice, ");
            sql.Append(" TxPrice = @inTxPrice, Unit = @inUnit, ImgUrl = @inImgUrl, ");
            sql.Append(" Description = @inDescription WHERE CommodityID = @inCommodityID ");

            SqlParameter[] paras = GetParas(inModel);

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            return result;
        }

        /// <summary>
        /// 创建商品
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int CreateCommodity(Model.CommodityList inModel)
        {
            StringBuilder deleteTrigger = new StringBuilder();
            deleteTrigger.Append(" IF( OBJECT_ID ('AutoInsert') IS NOT NULL ) ");
            deleteTrigger.Append(" DROP TRIGGER AutoInsert ");
            SqlHelper.ExecuteNonQueryVoid(deleteTrigger.ToString(), CommandType.Text, false);

            StringBuilder createTrigger = new StringBuilder();
            createTrigger.Append(" CREATE TRIGGER AutoInsert ");
            createTrigger.Append(" ON Tx_CommodityList FOR INSERT AS ");
            createTrigger.Append(" DECLARE @CommodityID INT ");
            createTrigger.Append(" SELECT @CommodityID = CommodityID FROM inserted ");
            createTrigger.Append(" INSERT INTO Tx_UserCommodityList( UserID, CommodityID ) ");
            createTrigger.Append(" VALUES( " + inModel.UserID + " , @CommodityID ) ");
            SqlHelper.ExecuteNonQueryTrigger(createTrigger.ToString());

            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO Tx_CommodityList ( ");
            sql.Append(" Title, Category, Price, TxPrice, Unit, ImgUrl, Description ");
            sql.Append(" ) VALUES ( ");
            sql.Append(" @inTitle, @inCategory, @inPrice, @inTxPrice, @inUnit, @inImgUrl, @inDescription ) ");

            SqlParameter[] paras = GetParas(inModel);

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            return result;
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
                    Description = item["Description"].ToString(),
                    UserID = Convert.ToInt32(item["UserID"]),
                    UserName = item["UserName"].ToString()
                };

                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 私有方法，获取商品参数
        /// </summary>
        /// <returns></returns>
        private static SqlParameter[] GetParas(Model.CommodityList inModel)
        {
            List<SqlParameter> list = new List<SqlParameter>();

            if (inModel.CommodityID != 0)
            {
                SqlParameter commodityid = new SqlParameter("@inCommodityID", SqlDbType.Int, 32);
                commodityid.Value = inModel.CommodityID;
                list.Add(commodityid);
            }

            SqlParameter title = new SqlParameter("@inTitle", SqlDbType.NVarChar, 150);
            title.Value = inModel.Title ?? string.Empty;
            list.Add(title);

            SqlParameter category = new SqlParameter("@inCategory", SqlDbType.NVarChar, 50);
            category.Value = inModel.Category ?? string.Empty;
            list.Add(category);

            SqlParameter price = new SqlParameter("@inPrice", SqlDbType.Int, 32);
            price.Value = inModel.Price;
            list.Add(price);

            SqlParameter txprice = new SqlParameter("@inTxPrice", SqlDbType.Int, 32);
            txprice.Value = inModel.TxPrice;
            list.Add(txprice);

            SqlParameter unit = new SqlParameter("@inUnit", SqlDbType.NVarChar, 150);
            unit.Value = inModel.Unit ?? string.Empty;
            list.Add(unit);

            SqlParameter imgurl = new SqlParameter("@inImgUrl", SqlDbType.NVarChar, 250);
            imgurl.Value = inModel.ImgUrl ?? string.Empty;
            list.Add(imgurl);

            SqlParameter description = new SqlParameter("@inDescription", SqlDbType.NVarChar, 500);
            description.Value = inModel.Description ?? string.Empty;
            list.Add(description);

            SqlParameter userid = new SqlParameter("@inUserID", SqlDbType.Int, 32);
            userid.Value = inModel.UserID;
            list.Add(userid);

            return list.ToArray();
        }
    }
}
