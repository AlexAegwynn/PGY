using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DContent
    {
        public long ArticleID { get; set; } = 0;

        public int DomainID { get; set; } = 1;

        public string Conten { get; set; } = string.Empty;

        public string ImgUrl { get; set; } = string.Empty;
    }
}
