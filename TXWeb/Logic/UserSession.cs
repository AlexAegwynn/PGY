using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UserSession
    {
        /// <summary>
        /// 获取Session记录
        /// </summary>
        /// <returns></returns>
        public static Model.UserSession GetSession(int inUserID)
        {
            return Data.UserSession.GetSession(inUserID);
        }

        /// <summary>
        /// 添加Session记录
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int CreateSession(Model.UserSession inModel)
        {
            return Data.UserSession.CreateSession(inModel);
        }

        /// <summary>
        /// 更新Session记录
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int UpdateSession(Model.UserSession inModel)
        {
            return Data.UserSession.UpdateSession(inModel);
        }

        /// <summary>
        /// 删除Session记录
        /// </summary>
        /// <param name="inUserID"></param>
        /// <returns></returns>
        public static int DeleteSession(int inUserID)
        {
            return Data.UserSession.DeleteSession(inUserID);
        }
    }
}
