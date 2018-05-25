using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UserSession
    {
        /// <summary>
        /// 获取Session记录
        /// </summary>
        /// <returns></returns>
        public static Model.UserSession GetSession()
        {



            Model.UserSession model = new Model.UserSession();

            return model;
        }

        /// <summary>
        /// 添加Session记录
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int CreateSession(Model.UserSession inModel)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO Tx_UserSessionList ( ");
            sql.Append(" SID, UserID, SessionID ) VALUES (  ");
            sql.Append(" @inSID, @inUserID, @inSessionID ) ");

            SqlParameter[] paras = GetParas(inModel);

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            return result;
        }

        /// <summary>
        /// 更新Session记录
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int UpdateSession(Model.UserSession inModel)
        {
            string sql = @" UPDATE Tx_UserSessionList SET SessionID = @inSessionID WHERE UserID = @inUserID ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inSessionID", SqlDbType.Int, 32),
                new SqlParameter("@inUserID", SqlDbType.Int, 32)
            };
            paras[0].Value = inModel.SessionID;
            paras[1].Value = inModel.UserID;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);

            return result;
        }

        /// <summary>
        /// 删除Session记录
        /// </summary>
        /// <param name="inUserID"></param>
        /// <returns></returns>
        public static int DeleteSession(int inUserID)
        {
            string sql = @" DELETE FROM Tx_UserSessionList WHERE UserID = @inUserID ";

            SqlParameter para = new SqlParameter("@inUserID", SqlDbType.Int, 32);
            para.Value = inUserID;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, para);

            return result;
        }

        private static SqlParameter[] GetParas(Model.UserSession inModel)
        {
            List<SqlParameter> list = new List<SqlParameter>();

            SqlParameter sid = new SqlParameter("@inSID", SqlDbType.UniqueIdentifier, 16);
            sid.Value = inModel.SID;
            list.Add(sid);

            SqlParameter userID = new SqlParameter("@inUserID", SqlDbType.Int, 32);
            userID.Value = inModel.UserID;
            list.Add(userID);

            SqlParameter sessionID = new SqlParameter("@inSessionID", SqlDbType.Int, 32);
            sessionID.Value = inModel.SessionID;
            list.Add(sessionID);

            return list.ToArray();
        }
    }
}
