using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Data
{
    /// <summary>
    /// 网站操作类
    /// </summary>
	public class WebSite
    {
        /// <summary>
        /// 根据网站ID获取网站实体
        /// </summary>
        /// <param name="inWebSiteID">网站ID</param>
        /// <returns></returns>
        public static Model.WebSiteInfo GetWebSite(string inWebSiteID)
        {
            string sql = @" SELECT * FROM WebSiteInfo WHERE WebSiteID = @inWebSiteID ";

            SqlParameter para = new SqlParameter("@inWebSiteID", SqlDbType.NVarChar, 50);
            para.Value = inWebSiteID;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, para);

            return GetWebSite(dt);
        }

        /// <summary>
        /// 根据站点Key获取网站实体
        /// </summary>
        /// <param name="inKey"></param>
        /// <returns></returns>
        public static Model.WebSiteInfo GetWebSiteByKey(Guid inKey)
        {
            string sql = @" SELECT * FROM WebSiteInfo WHERE WebSiteKey = @inWebSiteKey ";

            SqlParameter para = new SqlParameter("@inWebSiteKey", SqlDbType.UniqueIdentifier, 16);
            para.Value = inKey;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, para);

            return GetWebSite(dt);
        }

        /// <summary>
        /// 获取站点列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.WebSiteInfo> GetWebSiteList()
        {
            string sql = @" SELECT * FROM WebSiteInfo ";

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql);
            List<Model.WebSiteInfo> list = new List<Model.WebSiteInfo>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Model.WebSiteInfo model = new Model.WebSiteInfo()
                    {
                        WebSiteKey = (Guid)(item["WebSiteKey"]),
                        WebSiteName = item["WebSiteName"].ToString(),
                        WebSiteID = item["WebSiteID"].ToString(),
                        DomainName = item["DomainName"].ToString(),
                        LogoImgUrl = item["LogoImgUrl"].ToString(),
                        CompanyName = item["CompanyName"].ToString(),
                        Address = item["Address"].ToString(),
                        PhoneNumber = item["PhoneNumber"].ToString(),
                        QQ = item["QQ"].ToString(),
                        WeChat = item["WeChat"].ToString(),
                        Email = item["Email"].ToString(),
                        QRCodeUrl = item["QRCodeUrl"].ToString(),
                        RecordNumber = item["RecordNumber"].ToString(),
                        Keywords = item["Keywords"].ToString(),
                        Description = item["Description"].ToString(),
                        Category = item["Category"].ToString(),
                        BackgroundImg = item["BackgroundImg"].ToString()
                    };

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 根据站点Key获取图片地址
        /// </summary>
        /// <param name="inKey">站点主键</param>
        /// <returns></returns>
        public static List<string> GetImgList(Guid inKey)
        {
            string sql = @" SELECT LogoImgUrl, QRCodeUrl, BackgroundImg FROM WebSiteInfo WHERE WebSiteKey = @inWebSiteKey ";

            SqlParameter para = new SqlParameter("@inWebSiteKey", SqlDbType.UniqueIdentifier, 16);
            para.Value = inKey;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, para);
            List<string> list = new List<string>();

            if (dt.Rows.Count > 0)
            {
                string logo = dt.Rows[0]["LogoImgUrl"].ToString();
                list.Add(logo);
                string qrCode = dt.Rows[0]["QRCodeUrl"].ToString();
                list.Add(qrCode);
                string bgImg = dt.Rows[0]["BackgroundImg"].ToString();
                list.Add(bgImg);
            }

            return list;
        }

        /// <summary>
        /// 添加站点
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int AddWebSite(Model.WebSiteInfo inModel)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO WebSiteInfo ( ");
            sql.Append(" WebSiteKey, WebSiteName, DomainName, LogoImgUrl, CompanyName, Address, ");
            sql.Append(" PhoneNumber, QQ, WeChat, Email, QRCodeUrl, RecordNumber, Keywords, Description, Category, BackgroundImg ");
            sql.Append(" ) VALUES ( ");
            sql.Append(" @inWebSiteKey, @inWebSiteName, @inDomainName, @inLogoImgUrl, @inCompanyName, @inAddress, ");
            sql.Append(" @inPhoneNumber, @inQQ, @inWeChat, @inEmail, @inQRCodeUrl, @inRecordNumber, @inKeywords, @inDescription, @inCategory, @inBackgroundImg ) ");

            SqlParameter[] paras = GetParas(inModel);

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            return result;
        }

        /// <summary>
        /// 更新站点
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public static int UpdateWebSite(Model.WebSiteInfo inModel)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" UPDATE WebSiteInfo SET WebSiteName = @inWebSiteName, ");
            sql.Append(" DomainName = @inDomainName, CompanyName = @inCompanyName, ");
            sql.Append(" PhoneNumber = @inPhoneNumber, Address = @inAddress, ");
            sql.Append(" QQ = @inQQ, WeChat = @inWeChat, Email = @inEmail, RecordNumber = @inRecordNumber, ");
            sql.Append(" Keywords = @inKeywords, Description = @inDescription, Category = @inCategory ");
            sql.Append(" WHERE WebSiteKey = @inWebSiteKey ");

            SqlParameter[] paras = GetParas(inModel);

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), paras);

            return result;
        }

        /// <summary>
        /// 根据站点Key删除站点
        /// </summary>
        /// <param name="inKey">站点Key</param>
        /// <returns></returns>
        public static int DeleteWebSite(Guid inKey)
        {
            string sql = @" DELETE FROM WebSiteInfo WHERE WebSiteKey = @inWebSiteKey ";

            SqlParameter para = new SqlParameter("@inWebSiteKey", SqlDbType.UniqueIdentifier, 16);
            para.Value = inKey;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, para);

            return result;
        }

        /// <summary>
        /// 保存Logo
        /// </summary>
        /// <param name="inLogoImgUrl"></param>
        /// <param name="inWebSiteID"></param>
        /// <returns></returns>
        public static int SaveLogo(string inLogoImgUrl, Guid inKey)
        {
            string sql = @" UPDATE WebSiteInfo SET LogoImgUrl = @inLogoImgUrl WHERE WebSiteKey = @inWebSiteKey ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inLogoImgUrl", SqlDbType.NVarChar, 250),
                new SqlParameter("@inWebSiteKey", SqlDbType.UniqueIdentifier, 16)
            };
            paras[0].Value = inLogoImgUrl;
            paras[1].Value = inKey;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);

            return result;
        }

        /// <summary>
        /// 删除Logo
        /// </summary>
        /// <param name="inWebSiteID"></param>
        /// <returns></returns>
        public static int DeleteLogo(Guid inKey)
        {
            string sql = @" UPDATE WebSiteInfo SET LogoImgUrl = '' WHERE WebSiteKey = @inWebSiteKey ";

            SqlParameter para = new SqlParameter("@inWebSiteKey", SqlDbType.UniqueIdentifier, 16);
            para.Value = inKey;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, para);

            return result;
        }

        /// <summary>
        /// 保存二维码
        /// </summary>
        /// <param name="inQRCodeUrl"></param>
        /// <param name="inWebSiteID"></param>
        /// <returns></returns>
        public static int SaveQRCode(string inQRCodeUrl, Guid inKey)
        {
            string sql = @" UPDATE WebSiteInfo SET QRCodeUrl = @inQRCodeUrl WHERE WebSiteKey = @inWebSiteKey ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inQRCodeUrl", SqlDbType.NVarChar, 250),
                new SqlParameter("@inWebSiteKey", SqlDbType.UniqueIdentifier, 16)
            };
            paras[0].Value = inQRCodeUrl;
            paras[1].Value = inKey;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return result;
        }

        /// <summary>
        /// 删除二维码
        /// </summary>
        /// <returns></returns>
        public static int DeleteQRCode(Guid inKey)
        {
            string sql = @" UPDATE WebSiteInfo SET QRCodeUrl = '' WHERE WebSiteKey = @inWebSiteKey ";

            SqlParameter para = new SqlParameter("@inWebSiteKey", SqlDbType.UniqueIdentifier, 16);
            para.Value = inKey;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, para);

            return result;
        }

        /// <summary>
        /// 保存背景图片
        /// </summary>
        /// <param name="inBackgroundImg"></param>
        /// <param name="inKey"></param>
        /// <returns></returns>
        public static int SaveBackgroundImg(string inBackgroundImg, Guid inKey)
        {
            string sql = @" UPDATE WebSiteInfo SET BackgroundImg = @inBackgroundImg WHERE WebSiteKey = @inWebSiteKey ";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@inBackgroundImg", SqlDbType.NVarChar, 250),
                new SqlParameter("@inWebSiteKey", SqlDbType.UniqueIdentifier, 16)
            };
            paras[0].Value = inBackgroundImg;
            paras[1].Value = inKey;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, paras);
            return result;
        }

        /// <summary>
        /// 删除背景图片
        /// </summary>
        /// <param name="inKey"></param>
        /// <returns></returns>
        public static int DeleteBackgroundImg(Guid inKey)
        {
            string sql = @" UPDATE WebSiteInfo SET BackgroundImg = '' WHERE WebSiteKey = @inWebSiteKey ";

            SqlParameter para = new SqlParameter("@inWebSiteKey", SqlDbType.UniqueIdentifier, 16);
            para.Value = inKey;

            int result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, para);
            return result;
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        private static SqlParameter[] GetParas(Model.WebSiteInfo inModel)
        {
            List<SqlParameter> list = new List<SqlParameter>();

            SqlParameter webSiteKey = new SqlParameter("@inWebSiteKey", SqlDbType.UniqueIdentifier, 16);
            webSiteKey.Value = inModel.WebSiteKey;
            list.Add(webSiteKey);

            SqlParameter webSiteName = new SqlParameter("@inWebSiteName", SqlDbType.NVarChar, 50);
            webSiteName.Value = inModel.WebSiteName;
            list.Add(webSiteName);

            SqlParameter domainName = new SqlParameter("@inDomainName", SqlDbType.NVarChar, 50);
            domainName.Value = inModel.DomainName;
            list.Add(domainName);

            SqlParameter logoImgUrl = new SqlParameter("@inLogoImgUrl", SqlDbType.NVarChar, 250);
            logoImgUrl.Value = inModel.LogoImgUrl;
            list.Add(logoImgUrl);

            SqlParameter companyName = new SqlParameter("@inCompanyName", SqlDbType.NVarChar, 50);
            companyName.Value = inModel.CompanyName;
            list.Add(companyName);

            SqlParameter address = new SqlParameter("@inAddress", SqlDbType.NVarChar, 300);
            address.Value = inModel.Address;
            list.Add(address);

            SqlParameter phoneNumber = new SqlParameter("@inPhoneNumber", SqlDbType.NVarChar, 50);
            phoneNumber.Value = inModel.PhoneNumber;
            list.Add(phoneNumber);

            SqlParameter qQ = new SqlParameter("@inQQ", SqlDbType.NVarChar, 50);
            qQ.Value = inModel.QQ;
            list.Add(qQ);

            SqlParameter weChat = new SqlParameter("@inWeChat", SqlDbType.NVarChar, 50);
            weChat.Value = inModel.WeChat;
            list.Add(weChat);

            SqlParameter email = new SqlParameter("@inEmail", SqlDbType.NVarChar, 50);
            email.Value = inModel.Email;
            list.Add(email);

            SqlParameter qRCodeUrl = new SqlParameter("@inQRCodeUrl", SqlDbType.NVarChar, 250);
            qRCodeUrl.Value = inModel.QRCodeUrl;
            list.Add(qRCodeUrl);

            SqlParameter recordNumber = new SqlParameter("@inRecordNumber", SqlDbType.NVarChar, 100);
            recordNumber.Value = inModel.RecordNumber;
            list.Add(recordNumber);

            SqlParameter keywords = new SqlParameter("@inKeywords", SqlDbType.NVarChar, 500);
            keywords.Value = inModel.Keywords;
            list.Add(keywords);

            SqlParameter description = new SqlParameter("@inDescription", SqlDbType.NVarChar, 200);
            description.Value = inModel.Description;
            list.Add(description);

            SqlParameter category = new SqlParameter("@inCategory", SqlDbType.NVarChar, 50);
            category.Value = inModel.Category;
            list.Add(category);

            SqlParameter backgroundImg = new SqlParameter("@inBackgroundImg", SqlDbType.NVarChar, 250);
            backgroundImg.Value = inModel.BackgroundImg;
            list.Add(backgroundImg);

            return list.ToArray();
        }

        /// <summary>
        /// 获取网站实体
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static Model.WebSiteInfo GetWebSite(DataTable dt)
        {
            Model.WebSiteInfo model = new Model.WebSiteInfo();

            if (dt.Rows.Count > 0)
            {
                model.WebSiteKey = (Guid)(dt.Rows[0]["WebSiteKey"]);
                model.WebSiteName = dt.Rows[0]["WebSiteName"].ToString();
                model.WebSiteID = dt.Rows[0]["WebSiteID"].ToString();
                model.DomainName = dt.Rows[0]["DomainName"].ToString();
                model.LogoImgUrl = dt.Rows[0]["LogoImgUrl"].ToString();
                model.CompanyName = dt.Rows[0]["CompanyName"].ToString();
                model.Address = dt.Rows[0]["Address"].ToString();
                model.PhoneNumber = dt.Rows[0]["PhoneNumber"].ToString();
                model.QQ = dt.Rows[0]["QQ"].ToString();
                model.WeChat = dt.Rows[0]["WeChat"].ToString();
                model.Email = dt.Rows[0]["Email"].ToString();
                model.QRCodeUrl = dt.Rows[0]["QRCodeUrl"].ToString();
                model.RecordNumber = dt.Rows[0]["RecordNumber"].ToString();
                model.Keywords = dt.Rows[0]["Keywords"].ToString();
                model.Description = dt.Rows[0]["Description"].ToString();
                model.Category = dt.Rows[0]["Category"].ToString();
                model.BackgroundImg = dt.Rows[0]["BackgroundImg"].ToString();
            }

            return model;
        }
    }
}