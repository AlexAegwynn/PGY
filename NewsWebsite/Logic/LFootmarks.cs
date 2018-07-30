using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LFootmarks
    {
        /// <summary>
        /// 获取足迹列表
        /// </summary>
        /// <param name="inUID"></param>
        /// <returns></returns>
        public static List<MFootmarks> GetFootmarks(int inUID)
        {
            return Data.DFootmarks.GetFootmarks(inUID);
        }
    }
}
