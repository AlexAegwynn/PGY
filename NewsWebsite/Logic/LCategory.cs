using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LCategory
    {
        /// <summary>
        /// 获取类目列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.MCategory> GetCatsList()
        {
            return Data.DCategory.GetCatsList();
        }
    }
}
