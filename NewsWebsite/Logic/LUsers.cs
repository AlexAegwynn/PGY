using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Data;

namespace Logic
{
    /// <summary>
    /// 用户逻辑处理类
    /// </summary>
    public class LUsers
    {
        /// <summary>
        /// 根据用户名和密码获取用户
        /// </summary>
        public static MUsers GetUsers(string inUserName, string inPassword)
        {
            return DUsers.GetUsers(inUserName, inPassword);
        }

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="inUserName"></param>
        /// <returns></returns>
        public static bool ExistUser(string inUserName)
        {
            return Data.DUsers.ExistUser(inUserName);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        public static int CreateUser(MUsers inModel)
        {
            return DUsers.CreateUser(inModel);
        }
    }
}
