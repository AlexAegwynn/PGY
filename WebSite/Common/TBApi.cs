using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.HttpAPI;
using Top.Api;

namespace Common
{
    public class TBApi
    {
        public static List<string> GetZKPice(string numIIDs)
        {
            TBTbk tkbll = new TBTbk();
            TopResponse topRsp = null;
            List<string> list = new List<string>();

            var strModel = tkbll.GetItemInfoByID(2, numIIDs.TrimEnd(','), ref topRsp);

            foreach (var item in strModel)
            {
                list.Add(item.ZkFinalPrice);
            }

            return list;
        }
    }
}
