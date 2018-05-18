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
            string sql = @" SELECT * FROM Tx_UserList WHERE UserID = @inUserID ";

            SqlParameter para = new SqlParameter("@inUserID", SqlDbType.Int, 32);
            para.Value = inUserID;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, para);

            List<Model.UserList> list = GetUserList(dt);

            return list.Count > 0 ? list[0] : null;
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
            sql.Append(" Name, Password, Class, Sex, ");
            sql.Append(" Email, PhoneNumber, WeChat, ");
            sql.Append(" QQ, Address, EnterpriseInfo, IsAdmin ");
            sql.Append(" ) VALUES ( ");
            sql.Append(" @inName, @inPassword, @inClass, @inSex, ");
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
            sql.Append(" Name = @inName, Class = @inClass, Sex = @inSex, ");
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
                    Name = item["Name"].ToString(),
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

            SqlParameter name = new SqlParameter("@inName", SqlDbType.NVarChar, 50);
            name.Value = inModel.Name;
            list.Add(name);

            SqlParameter password = new SqlParameter("@inPassword", SqlDbType.NVarChar, 50);
            password.Value = inModel.Password;
            list.Add(password);

            SqlParameter _class = new SqlParameter("@inClass", SqlDbType.NVarChar, 50);
            _class.Value = inModel.Class;
            list.Add(_class);

            SqlParameter sex = new SqlParameter("@inSex", SqlDbType.Int, 32);
            sex.Value = inModel.Sex;
            list.Add(sex);

            SqlParameter email = new SqlParameter("@inEmail", SqlDbType.NVarChar, 50);
            email.Value = inModel.Email;
            list.Add(email);

            SqlParameter phonenumber = new SqlParameter("@inPhoneNumber", SqlDbType.NVarChar, 50);
            phonenumber.Value = inModel.PhoneNumber;
            list.Add(phonenumber);

            SqlParameter wechat = new SqlParameter("@inWeChat", SqlDbType.NVarChar, 50);
            wechat.Value = inModel.WeChat;
            list.Add(wechat);

            SqlParameter qq = new SqlParameter("@inQQ", SqlDbType.NVarChar, 50);
            qq.Value = inModel.QQ;
            list.Add(qq);

            SqlParameter address = new SqlParameter("@inAddress", SqlDbType.NVarChar, 500);
            address.Value = inModel.Address;
            list.Add(address);

            SqlParameter enterpriseinfo = new SqlParameter("@inEnterpriseInfo", SqlDbType.NVarChar, -1);
            enterpriseinfo.Value = inModel.EnterpriseInfo;
            list.Add(enterpriseinfo);

            SqlParameter isadmin = new SqlParameter("@inIsAdmin", SqlDbType.Int, 32);
            isadmin.Value = inModel.IsAdmin;
            list.Add(isadmin);

            return list.ToArray();
        }
    }
}