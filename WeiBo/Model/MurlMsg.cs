using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class MurlMsg
    {
        /// <summary>
        /// 信息代码，正常 =【200】、出现验证码 =【701】
        /// <para>【600】=【用户未登录或者登录过期】</para>
        /// </summary>
        public int Code { set; get; } = 200;

        /// <summary>
        /// 提示信息，正常 = ""
        /// </summary>
        public string Msg { set; get; } = "";

        /// <summary>
        /// 主机名，如：newbpfe1.vm.kgb.cm6
        /// </summary>
        public string HostName { set; get; } = "";

        /// <summary>
        /// 代理IP
        /// </summary>
        public string IP { set; get; } = "";

        /// <summary>
        /// 代理端口
        /// </summary>
        public string Port { set; get; } = "";

        /// <summary>
        /// 返回 Json 信息
        /// </summary>
        public string Result { set; get; } = "";

        /// <summary>
        /// 提交的网址
        /// </summary>
        public string SubmitUrl { set; get; } = "";

        /// <summary>
        /// 网页回传内容
        /// </summary>
        public string HtmlStr { set; get; } = "";

        /// <summary>
        /// 网页 输入网址 或 回传网址
        /// </summary>
        public string ReturnUrl { get => _returnUrl; set => _returnUrl = value; }

        /// <summary>
        /// 操作结果判断：未知错误=【-1】、正常=【0】、Cookies失效=【1】、出现验证码=【2】、Result字符串分析出错=【3】、错误次数太多，停止操作=【4】、重新开始操作=【5】、操作账号打开失败=【6】、页面不存在=【7】
        /// </summary>
        public int ResultType { set; get; } = 0;

        /// <summary>
        /// 操作结果是否出错：默认为否
        /// </summary>
        public bool IsError { set; get; } = false;

        /// <summary>
        /// 操作内容是否正常：默认为是
        /// </summary>
        public bool IsNormal { set; get; } = true;

        /// <summary>
        /// 【group】【验证码用】
        /// </summary>
        public string Group { set; get; } = "";

        /// <summary>
        /// 【rid】【验证码用】
        /// </summary>
        public string Rid { set; get; } = "";

        /// <summary>
        /// 【sessionId】【验证码用】
        /// </summary>
        public string SessionID { set; get; } = "";

        /// <summary>
        /// 输入的验证码【验证码用】
        /// </summary>
        public string CheckCode { set; get; } = "";

        private string _returnUrl = "";

        /// <summary>
        /// 验证码图片网址【验证码用】
        /// </summary>
        public string CheckCodeUrl { set; get; } = "";
    }
}

