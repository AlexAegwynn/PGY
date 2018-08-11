using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM.Model
{
    /// <summary>
    /// 实体类，新版直通车网页流方式获取到的信息
    /// </summary>
	[Serializable]
	public class MurlMsg
	{
		//{"code":"600","msg":["包含了不属于该客户的关键词Id, 请去掉。"],"result":null,"hostName":"newbpfe5.vm.kgb.cm3"} --> 批量改价时发生，即该关键词 不存在 或 已被删除
		//{"code":"600","msg":["关键词不能为空。"],"result":null,"hostName":"newbpfe1.vm.kgb.cm3"} --> 单个改价时发生，即该关键词 不存在 或 已被删除
		private int _Code = 200;
		/// <summary>
		/// 信息代码，正常 =【200】、出现验证码 =【701】
		/// <para>【600】=【用户未登录或者登录过期】</para>
		/// </summary>
		public int Code { set { _Code = value; } get { return _Code; } }

		private string _Msg = "";
		/// <summary>
		/// 提示信息，正常 = ""
		/// </summary>
		public string Msg { set { _Msg = value; } get { return _Msg; } }

		private string _HostName = "";
		/// <summary>
		/// 主机名，如：newbpfe1.vm.kgb.cm6
		/// </summary>
		public string HostName { set { _HostName = value; } get { return _HostName; } }

        private string _IP = "";
        /// <summary>
        /// 代理IP
        /// </summary>
        public string IP { set { _IP = value; } get { return _IP; } }

        private string _Port = "";
        /// <summary>
        /// 代理端口
        /// </summary>
        public string Port { set { _Port = value; } get { return _Port; } }

		private string _Result = "";
		/// <summary>
		/// 返回 Json 信息
		/// </summary>
		public string Result { set { _Result = value; } get { return _Result; } }

		private string _SubmitUrl = "";
		/// <summary>
		/// 提交的网址
		/// </summary>
		public string SubmitUrl { set { _SubmitUrl = value; } get { return _SubmitUrl; } }

		private string _HtmlStr = "";
		/// <summary>
		/// 网页回传内容
		/// </summary>
		public string HtmlStr { set { _HtmlStr = value; } get { return _HtmlStr; } }

		private string _ReturnUrl = "";
		/// <summary>
		/// 网页 输入网址 或 回传网址
		/// </summary>
		public string ReturnUrl { set { _ReturnUrl = value; } get { return _ReturnUrl; } }

		private int _ResultType = 0;
		/// <summary>
		/// 操作结果判断：未知错误=【-1】、正常=【0】、Cookies失效=【1】、出现验证码=【2】、Result字符串分析出错=【3】、错误次数太多，停止操作=【4】、重新开始操作=【5】、操作账号打开失败=【6】、页面不存在=【7】
		/// </summary>
		public int ResultType { set { _ResultType = value; } get { return _ResultType; } }

		private bool _IsError = false;
		/// <summary>
		/// 操作结果是否出错：默认为否
		/// </summary>
		public bool IsError { set { _IsError = value; } get { return _IsError; } }

		private bool _IsNormal = true;
		/// <summary>
		/// 操作内容是否正常：默认为是
		/// </summary>
		public bool IsNormal { set { _IsNormal = value; } get { return _IsNormal; } }

		#region 验证码用

		private string _Group = "";
		/// <summary>
		/// 【group】【验证码用】
		/// </summary>
		public string Group { set { _Group = value; } get { return _Group; } }

		private string _Rid = "";
		/// <summary>
		/// 【rid】【验证码用】
		/// </summary>
		public string Rid { set { _Rid = value; } get { return _Rid; } }

		private string _SessionID = "";
		/// <summary>
		/// 【sessionId】【验证码用】
		/// </summary>
		public string SessionID { set { _SessionID = value; } get { return _SessionID; } }

		private string _CheckCode = "";
		/// <summary>
		/// 输入的验证码【验证码用】
		/// </summary>
		public string CheckCode { set { _CheckCode = value; } get { return _CheckCode; } }

		private string _CheckCodeUrl = "";
		/// <summary>
		/// 验证码图片网址【验证码用】
		/// </summary>
		public string CheckCodeUrl { set { _CheckCodeUrl = value; } get { return _CheckCodeUrl; } }

		#endregion
	}
}