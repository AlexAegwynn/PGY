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
        /// 获取用户
        /// </summary>
        /// <param name="inName"></param>
        /// <param name="inPassword"></param>
        /// <returns></returns>
        public static Model.UserList GetUser(string inName, string inPassword)
        {
            string sql = @" SELECT * FROM Tx_UserList WHERE Name = @inName AND Password = @inPassword ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inName", SqlDbType.NVarChar, 50),
                new SqlParameter("@inPassword", SqlDbType.NVarChar, 50)
            };
            paras[0].Value = inName;
            paras[1].Value = inPassword;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, paras);
            List<Model.UserList> list = GetUserList(dt);

            return list.Count > 0 ? list[0] : null;
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
    }
}
