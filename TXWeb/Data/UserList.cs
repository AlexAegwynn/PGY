using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class UserList
    {
        /// <summary>
        /// 根据邮箱和密码获取用户
        /// </summary>
        /// <param name="inEmail"></param>
        /// <param name="inPassword"></param>
        /// <returns></returns>
        public static Model.UserList ExistUser(string inEmail, string inPassword)
        {
            string sql = @" SELECT * FROM Tx_UserList WHERE Email = @inEmail AND Password = @inPassword ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inEmail", SqlDbType.NVarChar, 50),
                new SqlParameter("@inPassword", SqlDbType.NVarChar, 50)
            };
            paras[0].Value = inEmail;
            paras[1].Value = inPassword;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, paras);
            List<Model.UserList> list = GetUserList(dt);

            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 根据用户ID获取用户
        /// </summary>
        /// <param name="inUserID"></param>
        /// <returns></returns>
        public static Model.UserList GetUser(int inUserID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT a.*, c.Unit FROM Tx_UserList a ");
            sql.Append(" LEFT JOIN Tx_UserCommodityList b ON a.UserID = b.UserID ");
            sql.Append(" LEFT JOIN Tx_CommodityList c ON c.CommodityID = b.CommodityID ");
            sql.Append(" WHERE a.UserID = @inUserID ");

            SqlParameter para = new SqlParameter("@inUserID", SqlDbType.Int, 32);
            para.Value = inUserID;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql.ToString(), para);

            List<Model.UserList> list = GetUserList(dt);
            Model.UserList model = list.Count > 0 ? list[0] : null;

            if (model != null)
            {
                model.Unit = dt.Rows[0]["Unit"].ToString();
            }

            return model;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.UserList> GetUsers()
        {
            string sql = @" SELECT * FROM Tx_UserList ";

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql);

            List<Model.UserList> list = GetUserList(dt);

            return list;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int CreateUser(Model.UserList inModel)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO Tx_UserList ( ");
            sql.Append(" UserName, Password, Class, Sex, ");
            sql.Append(" Email, PhoneNumber, WeChat, ");
            sql.Append(" QQ, Address, EnterpriseInfo, IsAdmin ");
            sql.Append(" ) VALUES ( ");
            sql.Append(" @inUserName, @inPassword, @inClass, @inSex, ");
            sql.Append(" @inEmail, @inPhoneNumber, @inWeChat, ");
            sql.Append(" @inQQ, @inAddress, @inEnterpriseInfo, @inIsAdmin ) ");

            SqlParameter[] paras = GetParameters(inModel);

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            return result;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int UpdateUser(Model.UserList inModel)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" UPDATE Tx_UserList SET ");
            sql.Append(" UserName = @inUserName, Class = @inClass, Sex = @inSex, ");
            sql.Append(" Email = @inEmail, PhoneNumber = @inPhoneNumber, WeChat = @inWeChat, ");
            sql.Append(" QQ = @inQQ, Address = @inAddress, EnterpriseInfo = @inEnterpriseInfo, ");
            sql.Append(" IsAdmin = @inIsAdmin WHERE UserID = @inUserID ");

            SqlParameter[] paras = GetParameters(inModel);
            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            return result;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="inUserID"></param>
        /// <returns></returns>
        public static int DeleteUser(int inUserID)
        {
            string deleteTrigger = @" IF( OBJECT_ID ('AutoDelete') IS NOT NULL ) DROP TRIGGER AutoDelete ";
            SqlHelper.ExecuteNonQueryVoid(deleteTrigger, CommandType.Text, false);
            
            StringBuilder createTrigger = new StringBuilder();
            createTrigger.Append(" CREATE TRIGGER AutoDelete ");
            createTrigger.Append(" ON Tx_UserList FOR DELETE AS ");
            createTrigger.Append(" DELETE FROM Tx_UserCommodityList WHERE UserID = @inUserID ");
            SqlHelper.ExecuteNonQueryTrigger(createTrigger.ToString());

            string sql = @" DELETE FROM Tx_UserList WHERE UserID = @inUserID ";

            SqlParameter para = new SqlParameter("@inUserID", SqlDbType.Int, 32);
            para.Value = inUserID;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, para);

            return result;
        }

        /// <summary>
        /// 私有方法，获取用户列表
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static List<Model.UserList> GetUserList(DataTable dt)
        {
            List<Model.UserList> list = new List<Model.UserList>();

            foreach (DataRow item in dt.Rows)
            {
                Model.UserList model = new Model.UserList
                {
                    UserID = Convert.ToInt32(item["UserID"]),
                    UserName = item["UserName"].ToString(),
                    Password = item["Password"].ToString(),
                    Class = item["Class"].ToString(),
                    Sex = Convert.ToInt32(item["Sex"]),
                    Email = item["Email"].ToString(),
                    PhoneNumber = item["PhoneNumber"].ToString(),
                    WeChat = item["WeChat"].ToString(),
                    QQ = item["QQ"].ToString(),
                    Address = item["Address"].ToString(),
                    EnterpriseInfo = item["EnterpriseInfo"].ToString(),
                    IsAdmin = Convert.ToInt32(item["IsAdmin"])
                };
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 私有方法，获取User参数
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        private static SqlParameter[] GetParameters(Model.UserList inModel)
        {
            List<SqlParameter> list = new List<SqlParameter>();

            if (inModel.UserID != 0)
            {
                SqlParameter userid = new SqlParameter("@inUserID", SqlDbType.Int, 32);
                userid.Value = inModel.UserID;
                list.Add(userid);
            }

            SqlParameter name = new SqlParameter("@inUserName", SqlDbType.NVarChar, 50);
            name.Value = inModel.UserName ?? string.Empty;
            list.Add(name);

            SqlParameter password = new SqlParameter("@inPassword", SqlDbType.NVarChar, 50);
            password.Value = inModel.Password ?? string.Empty;
            list.Add(password);

            SqlParameter _class = new SqlParameter("@inClass", SqlDbType.NVarChar, 50);
            _class.Value = inModel.Class ?? string.Empty;
            list.Add(_class);

            SqlParameter sex = new SqlParameter("@inSex", SqlDbType.Int, 32);
            sex.Value = inModel.Sex;
            list.Add(sex);

            SqlParameter email = new SqlParameter("@inEmail", SqlDbType.NVarChar, 50);
            email.Value = inModel.Email ?? string.Empty;
            list.Add(email);

            SqlParameter phonenumber = new SqlParameter("@inPhoneNumber", SqlDbType.NVarChar, 50);
            phonenumber.Value = inModel.PhoneNumber ?? string.Empty;
            list.Add(phonenumber);

            SqlParameter wechat = new SqlParameter("@inWeChat", SqlDbType.NVarChar, 50);
            wechat.Value = inModel.WeChat ?? string.Empty;
            list.Add(wechat);

            SqlParameter qq = new SqlParameter("@inQQ", SqlDbType.NVarChar, 50);
            qq.Value = inModel.QQ ?? string.Empty;
            list.Add(qq);

            SqlParameter address = new SqlParameter("@inAddress", SqlDbType.NVarChar, 500);
            address.Value = inModel.Address ?? string.Empty;
            list.Add(address);

            SqlParameter enterpriseinfo = new SqlParameter("@inEnterpriseInfo", SqlDbType.NVarChar, -1);
            enterpriseinfo.Value = inModel.EnterpriseInfo ?? string.Empty;
            list.Add(enterpriseinfo);

            SqlParameter isadmin = new SqlParameter("@inIsAdmin", SqlDbType.Int, 32);
            isadmin.Value = inModel.IsAdmin;
            list.Add(isadmin);

            return list.ToArray();
        }
    }
}