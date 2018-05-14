using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Keys
    {
        public static string connectionString = string.Empty;

        public static void ConnectionString(string inState)
        {
            switch (inState)
            {
                case "Development":
                    //connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebSite; uid=sa;pwd=22446688"; break;
                    connectionString = @"Data Source=USER-20170408WR\SQL2005;Initial Catalog=WebSite; uid=sa;pwd=22446688"; break;
                case "Release":
                    connectionString = @"Data Source=ECS-2D04\SQL2005;Initial Catalog=WebSite;User ID=sa;Password=ASDQWE!@#qwe"; break;
            }
        }
    }
}
