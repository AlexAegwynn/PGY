using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace Data
{
    public class DContent
    {
        public static List<MContent> GetContents()
        {
            string sql = @" SELECT * FROM wz_Content WHERE Origin = '今日头条' ";
        }
    }
}
