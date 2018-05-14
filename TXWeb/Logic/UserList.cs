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
        public static Model.UserList GetUser(string inName, string inPassword)
        {
            return Data.UserList.GetUser(inName, inPassword);
        }
    }
}