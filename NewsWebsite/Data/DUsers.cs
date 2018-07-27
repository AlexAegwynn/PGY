using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace Data
{
    /// <summary>
    /// 用户数据处理类
    /// </summary>
    public class DUsers
    {
        /// <summary>
        /// 根据用户名和密码获取用户
        /// </summary>
        public static MUsers GetUsers(string inUserName, string inPassword)
        {
            string sql = @" SELECT * FROM nw_Users WHERE UserName = @inUserName AND Password = @inPassword ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inUserName", SqlDbType.NVarChar, 50),
                new SqlParameter("@inPassword", SqlDbType.VarChar, 50)
            };
            paras[0].Value = inUserName;
            paras[1].Value = inPassword;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, paras);
            MUsers model = null;

            if (dt.Rows.Count > 0)
            {
                model = new MUsers
                {
                    UID = Convert.ToInt32(dt.Rows[0]["UID"]),
                    UserName = dt.Rows[0]["UserName"].ToString(),
                    Password = dt.Rows[0]["Password"].ToString(),
                    Email = dt.Rows[0]["Email"].ToString()
                };
            }

            return model;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        public static int CreateUser(MUsers inModel)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO nw_Users ( ");
            sql.Append(" UserName, Password, Email ) VALUES ( ");
            sql.Append(" @inUserName, @inPassword, @inEmail ) ");

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inUserName", SqlDbType.NVarChar, 50),
                new SqlParameter("@inPassword", SqlDbType.VarChar, 50),
                new SqlParameter("@inEmail", SqlDbType.VarChar, 50)
            };
            paras[0].Value = inModel.UserName;
            paras[0].Value = inModel.Password;
            paras[0].Value = inModel.Email;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            return result;
        }
    }
}
