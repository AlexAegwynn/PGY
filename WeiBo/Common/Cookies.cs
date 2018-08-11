using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.HttpAPI.TBPublic;

namespace Common
{
    public class Cookies : TbBase
    {
        /// <summary>
        /// 获取 Cookies
        /// </summary>
        public bool GetCookies(System.Uri urlObject, string url, ref string cookies)
        {
            if (urlObject != null)
            {
                url = urlObject.AbsoluteUri;
            }
            return GetCookiesStone(url, ref cookies);
        }
    }
}
