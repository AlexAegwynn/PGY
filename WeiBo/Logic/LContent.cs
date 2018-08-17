using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LContent
    {
        /// <summary>
        /// 获取内容列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.MContent> GetContents(int domainid)
        {
            return Data.DContent.GetContents(domainid);
        }

        /// <summary>
        /// 更新内容
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int UpdateContent(Model.MContent inModel)
        {
            return Data.DContent.UpdateContent(inModel);
        }
    }
}
