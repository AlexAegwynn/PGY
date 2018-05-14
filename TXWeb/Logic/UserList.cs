using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UserList
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="inName"></param>
        /// <param name="inPassword"></param>
        /// <returns></returns>
        public static Model.UserList GetUser(string inEmail, string inPassword)
        {
            return Data.UserList.GetUser(inEmail, inPassword);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.UserList> GetUsers()
        {
            return Data.UserList.GetUsers();
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int CreateUser(Model.UserList inModel)
        {
            return Data.UserList.CreateUser(inModel);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int UpdateUser(Model.UserList inModel)
        {
            return Data.UserList.UpdateUser(inModel);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="inUserID"></param>
        /// <returns></returns>
        public static int DeleteUser(int inUserID)
        {
            return Data.UserList.DeleteUser(inUserID);
        }
    }
}