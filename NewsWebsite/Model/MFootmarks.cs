﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 文章足迹实体类
    /// </summary>
    public class MFootmarks
    {
        /// <summary>
        /// 足迹文章ID
        /// </summary>
        public int FmID { get; set; } = 0;

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UID { get; set; } = 0;

        /// <summary>
        /// 文章ID
        /// </summary>
        public long ArticleID { get; set; } = 0;

        /// <summary>
        /// 访问时间
        /// </summary>
        public DateTime MarkTime { get; set; } = Convert.ToDateTime("1900-1-1");

        /// <summary>
        /// 足迹文章标题
        /// </summary>
        public string FmTitle { get; set; } = string.Empty;
    }
}
