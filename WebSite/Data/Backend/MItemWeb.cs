using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM.Model.Web
{
	public class MItemWeb
	{
		#region 表字段
		private int _ID = 0;
		/// <summary>
		/// 信息编号,自增
		/// </summary>
		public int ID { set { _ID = value; } get { return _ID; } }

		private int _WID = 0;
		/// <summary>
		/// 网站编号
		/// </summary>
		public int WID{get { return _WID; } set { _WID = value; }}

		private long _IID = 0;
		/// <summary>
		/// 商品编号
		/// </summary>
		public long IID{get { return _IID; }set { _IID = value; } }
		#endregion

		#region 非表字段
		private string _WName = "";
		/// <summary>
		/// 网站名称
		/// </summary>
		public string WName { get { return _WName; } set { _WName = value; } }

		private string _Title = "";
		/// <summary>
		/// 商品标题
		/// </summary>
		public string Title { get { return _Title; } set { _Title = value; } }
		#endregion


	}
}
