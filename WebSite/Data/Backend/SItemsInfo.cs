using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using CM.DBUtility;
using CM.IDAL;
using CM.Model;
//using CM.Common.WebCookie;


namespace CM.SQLServerDAL
{
	/// <summary>
	/// 商品基本信息
	/// </summary>
	public class SItemsInfo : IItemsInfo
	{

		#region 参数变量
		private const string ID = "@ID";
		private const string NumIID = "@NumIID";
		private const string Title = "@Title";
		private const string KeyWordStr = "@KeyWordStr";
		private const string TitleSub = "@TitleSub";
		private const string TitleDescribe = "@TitleDescribe";
		private const string IsPush = "@IsPush";
		private const string PushTime = "@PushTime";
		private const string CatID = "@CatID";
		private const string Navigation = "@Navigation";
		private const string OrderUrl = "@OrderUrl";//商品链接
		private const string ImgUrl = "@ImgUrl";
		private const string ImgSmall = "@ImgSmall";
		private const string Inside = "@Inside";//原价
		private const string PriceNow = "@PriceNow";
		private const string IsTmall = "@IsTmall";
		private const string SalesCount = "@SalesCount";
		private const string IsGood = "@IsGood";
		private const string CreateTime = "@CreateTime";
		private const string UpdateTime = "@UpdateTime";
		private const string IsEnable = "@IsEnable";
		private const string ActivityID = "@ActivityID";
		private const string TimeStart = "@TimeStart";
		private const string TimeEnd = "@TimeEnd";
		private const string ClickUrl = "@ClickUrl";
		private const string UrlShort = "@UrlShort";
		private const string TotalCount = "@TotalCount";
		private const string RemainCount = "@RemainCount";
		private const string CommissionRate = "@CommissionRate";
		private const string Commission = "@Commission";
		private const string CouponInfo = "@CouponInfo";
		private const string CouponMoney = "@CouponMoney";
		private const string TimeUpdate = "@TimeUpdate";
		private const string CouponType = "@CouponType";
		private const string UseCount = "@UseCount";
		private const string Nick = "@Nick";
		private const string SellerID = "@SellerID";

		//外表
		private const string Price = "@Price";
		private const string PID = "@PID";
		private const string TimeStartPID = "@TimeStartPID";
		private const string TimeEndPID = "@TimeEndPID";
		private const string ClickUrlPID = "@ClickUrlPID";
		private const string TotalCountPID = "@TotalCountPID";
		private const string RemainCountPID = "@RemainCountPID";
		private const string CommissionRatePID = "@CommissionRatePID";
		private const string CommissionPID = "@CommissionPID";
		private const string CouponInfoPID = "@CouponInfoPID";
		private const string CouponMoneyPID = "@CouponMoneyPID";
		private const string TimeUpdatePID = "@TimeUpdatePID";
		private const string CouponTypePID = "@CouponTypePID";
		private const string TpwdPID = "@TpwdPID";
		private const string TpwdTimePID = "@TpwdTimePID";
		private const string CatName = "@CatName";

		#endregion

		#region 通用参数

		/// <summary>
		/// 参数语句
		/// </summary>
		private SqlCommand GetCommand(MItemsInfo model)
		{
			SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add(new SqlParameter(ID, SqlDbType.BigInt));
			cmd.Parameters.Add(new SqlParameter(NumIID, SqlDbType.BigInt));			
			cmd.Parameters.Add(new SqlParameter(Title, SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter(KeyWordStr, SqlDbType.VarChar,-1));
			cmd.Parameters.Add(new SqlParameter(TitleSub, SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter(TitleDescribe, SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter(IsPush, SqlDbType.TinyInt));
			cmd.Parameters.Add(new SqlParameter(PushTime, SqlDbType.BigInt));
			cmd.Parameters.Add(new SqlParameter(CatID, SqlDbType.BigInt));
            cmd.Parameters.Add(new SqlParameter(Navigation, SqlDbType.TinyInt));
			cmd.Parameters.Add(new SqlParameter(ImgUrl, SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter(ImgSmall, SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter(PriceNow, SqlDbType.Decimal));
			cmd.Parameters.Add(new SqlParameter(IsTmall, SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter(SalesCount, SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter(IsGood, SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter(CreateTime, SqlDbType.BigInt));
			cmd.Parameters.Add(new SqlParameter(UpdateTime, SqlDbType.BigInt));
			cmd.Parameters.Add(new SqlParameter(IsEnable,SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter(ActivityID, SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter(TimeStart, SqlDbType.BigInt));
			cmd.Parameters.Add(new SqlParameter(TimeEnd, SqlDbType.BigInt));
			cmd.Parameters.Add(new SqlParameter(ClickUrl, SqlDbType.VarChar));
            cmd.Parameters.Add(new SqlParameter(UrlShort, SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter(TotalCount, SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter(RemainCount, SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter(CommissionRate, SqlDbType.Decimal));
			cmd.Parameters.Add(new SqlParameter(Commission, SqlDbType.Decimal));
			cmd.Parameters.Add(new SqlParameter(CouponInfo, SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter(CouponMoney, SqlDbType.Decimal));
			cmd.Parameters.Add(new SqlParameter(TimeUpdate, SqlDbType.BigInt));
			cmd.Parameters.Add(new SqlParameter(CouponType,SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter(UseCount, SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter(Nick, SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter(SellerID, SqlDbType.BigInt));

			//外表
			cmd.Parameters.Add(new SqlParameter(Price, SqlDbType.Decimal));
			cmd.Parameters.Add(new SqlParameter(PID,SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter(TimeStartPID, SqlDbType.BigInt));
			cmd.Parameters.Add(new SqlParameter(TimeEndPID, SqlDbType.BigInt));
			cmd.Parameters.Add(new SqlParameter(ClickUrlPID, SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter(TotalCountPID, SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter(RemainCountPID, SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter(CommissionRatePID, SqlDbType.Decimal));
			cmd.Parameters.Add(new SqlParameter(CommissionPID, SqlDbType.Decimal));
			cmd.Parameters.Add(new SqlParameter(CouponInfoPID, SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter(CouponMoneyPID, SqlDbType.Decimal));
			cmd.Parameters.Add(new SqlParameter(TimeUpdatePID, SqlDbType.BigInt));
			cmd.Parameters.Add(new SqlParameter(CouponTypePID,SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter(CatName, SqlDbType.VarChar));

            cmd.Parameters[ID].Value = model.ID;
			cmd.Parameters[NumIID].Value = model.NumIID;			
			cmd.Parameters[Title].Value = model.Title;
			cmd.Parameters[KeyWordStr].Value = model.KeyWordStr;
			cmd.Parameters[TitleSub].Value = model.TitleSub;
			cmd.Parameters[TitleDescribe].Value = model.TitleDescribe;
			cmd.Parameters[IsPush].Value = model.IsPush;
			cmd.Parameters[PushTime].Value = model.PushTime == DateTime.Parse("2000-01-01") ? 0 : SPublic.GetTimeByDateTime(model.PushTime);
			cmd.Parameters[CatID].Value = model.CatID;
            cmd.Parameters[Navigation].Value = model.Navigation;
			cmd.Parameters[ImgUrl].Value = model.ImgUrl;
			cmd.Parameters[ImgSmall].Value = model.ImgSmall;
			cmd.Parameters[PriceNow].Value = model.PriceNow;
			cmd.Parameters[IsTmall].Value = model.IsTmall;
			cmd.Parameters[SalesCount].Value = model.SalesCount;
			cmd.Parameters[IsGood].Value = model.IsGood;
			cmd.Parameters[CreateTime].Value = model.CreateTime == DateTime.Parse("2000-01-01") ? 0 : SPublic.GetTimeByDateTime(model.CreateTime);
			cmd.Parameters[UpdateTime].Value = model.UpdateTime == DateTime.Parse("2000-01-01") ? 0 : SPublic.GetTimeByDateTime(model.UpdateTime);
			cmd.Parameters[IsEnable].Value = model.IsEnable;
			cmd.Parameters[ActivityID].Value = model.ActivityID;
			cmd.Parameters[TimeStart].Value = model.TimeStart == DateTime.Parse("2000-01-01") ? 0 : SPublic.GetTimeByDateTime(model.TimeStart);
			cmd.Parameters[TimeEnd].Value = model.TimeEnd == DateTime.Parse("2000-01-01") ? 0 : SPublic.GetTimeByDateTime(model.TimeEnd);
			cmd.Parameters[ClickUrl].Value = model.ClickUrl;
            cmd.Parameters[UrlShort].Value = model.UrlShort;
			cmd.Parameters[TotalCount].Value = model.TotalCount;
			cmd.Parameters[RemainCount].Value = model.RemainCount;
			cmd.Parameters[CommissionRate].Value = model.CommissionRate;
			cmd.Parameters[Commission].Value = model.Commission;
			cmd.Parameters[CouponInfo].Value = model.CouponInfo;
			cmd.Parameters[CouponMoney].Value = model.CouponMoney;
			cmd.Parameters[TimeUpdate].Value = model.TimeUpdate == DateTime.Parse("2000-01-01") ? 0 : SPublic.GetTimeByDateTime(model.TimeUpdate);
			cmd.Parameters[CouponType].Value = model.CouponType;
			cmd.Parameters[UseCount].Value = model.UseCount;
			cmd.Parameters[Nick].Value = model.Nick;
			cmd.Parameters[SellerID].Value = model.SellerID;

			//外表
			cmd.Parameters[Price].Value = model.Price;
			cmd.Parameters[PID].Value = model.PID;
			cmd.Parameters[TimeStartPID].Value = model.TimeStartPID == DateTime.Parse("2000-01-01") ? 0 : SPublic.GetTimeByDateTime(model.TimeStartPID);
			cmd.Parameters[TimeEndPID].Value = model.TimeEndPID == DateTime.Parse("2000-01-01") ? 0 : SPublic.GetTimeByDateTime(model.TimeEndPID);
			cmd.Parameters[ClickUrlPID].Value = model.ClickUrlPID;
			cmd.Parameters[TotalCountPID].Value = model.TotalCountPID;
			cmd.Parameters[RemainCountPID].Value = model.RemainCountPID;
			cmd.Parameters[CommissionRatePID].Value = model.CommissionRatePID;
			cmd.Parameters[CommissionPID].Value = model.CommissionPID;
			cmd.Parameters[CouponInfoPID].Value = model.CouponInfoPID;
			cmd.Parameters[CouponMoneyPID].Value = model.CouponMoneyPID;
			cmd.Parameters[TimeUpdatePID].Value = model.TimeUpdatePID == DateTime.Parse("2000-01-01") ? 0 : SPublic.GetTimeByDateTime(model.TimeUpdatePID);
			cmd.Parameters[CouponTypePID].Value = model.CouponTypePID;
			cmd.Parameters[CatName].Value = model.CatName;

			return cmd;
		}

		/// <summary>
		/// 实体信息
		/// </summary>
		/// <returns></returns>
		private MItemsInfo GetModel(DataRow row)
		{

			MItemsInfo model = new MItemsInfo();
            if (row.Table.Columns.Contains("ID") && row["ID"] != DBNull.Value)
                model.ID = Convert.ToInt64(row["ID"]);

			if (row.Table.Columns.Contains("NumIID") && row["NumIID"] != DBNull.Value)
				model.NumIID = Convert.ToInt64(row["NumIID"]);

			if (row.Table.Columns.Contains("Title") && row["Title"] != DBNull.Value)
				model.Title = Convert.ToString(row["Title"]);

			if (row.Table.Columns.Contains("KeyWordStr") && row["KeyWordStr"] != DBNull.Value)
				model.KeyWordStr = Convert.ToString(row["KeyWordStr"]);

			if (row.Table.Columns.Contains("TitleSub") && row["TitleSub"] != DBNull.Value)
				model.TitleSub = Convert.ToString(row["TitleSub"].ToString());

			if (row.Table.Columns.Contains("TitleDescribe") && row["TitleDescribe"] != DBNull.Value)
				model.TitleDescribe = Convert.ToString(row["TitleDescribe"].ToString());

			if (row.Table.Columns.Contains("IsPush") && row["IsPush"] != DBNull.Value)
				model.IsPush = Convert.ToInt32(row["IsPush"]);

			if (row.Table.Columns.Contains("PushTime") && row["PushTime"] != DBNull.Value)
				model.PushTime = SPublic.GetDateTimeByTime(Convert.ToInt64(row["PushTime"]));

			if (row.Table.Columns.Contains("CatID") && row["CatID"] != DBNull.Value)
				model.CatID = Convert.ToInt64(row["CatID"]);

            if (row.Table.Columns.Contains("Navigation") && row["Navigation"] != DBNull.Value)
                model.Navigation = Convert.ToInt32(row["Navigation"]);

			if (row.Table.Columns.Contains("ImgUrl") && row["ImgUrl"] != DBNull.Value)
				model.ImgUrl = Convert.ToString(row["ImgUrl"]);

			if (row.Table.Columns.Contains("ImgSmall") && row["ImgSmall"] != DBNull.Value)
				model.ImgSmall = Convert.ToString(row["ImgSmall"]);

			if (row.Table.Columns.Contains("PriceNow") && row["PriceNow"] != DBNull.Value)
				model.PriceNow = float.Parse(row["PriceNow"].ToString());

			if (row.Table.Columns.Contains("IsTmall") && row["IsTmall"] != DBNull.Value)
				model.IsTmall = Convert.ToInt32(row["IsTmall"]);

			if (row.Table.Columns.Contains("SalesCount") && row["SalesCount"] != DBNull.Value)
				model.SalesCount = Convert.ToInt32(row["SalesCount"]);

			if (row.Table.Columns.Contains("IsGood") && row["IsGood"] != DBNull.Value)
				model.IsGood = Convert.ToInt32(row["IsGood"]);

			if (row.Table.Columns.Contains("CreateTime") && row["CreateTime"] != DBNull.Value)
				model.CreateTime = SPublic.GetDateTimeByTime(Convert.ToInt64(row["CreateTime"]));

			if (row.Table.Columns.Contains("UpdateTime") && row["UpdateTime"] != DBNull.Value)
				model.UpdateTime = SPublic.GetDateTimeByTime(Convert.ToInt64(row["UpdateTime"]));

			if (row.Table.Columns.Contains("IsEnable") && row["IsEnable"] != DBNull.Value)
				model.IsEnable = Convert.ToInt32(row["IsEnable"]);

			if (row.Table.Columns.Contains("ActivityID") && row["ActivityID"] != DBNull.Value)
				model.ActivityID = Convert.ToString(row["ActivityID"]);

			if (row.Table.Columns.Contains("TimeStart") && row["TimeStart"] != DBNull.Value)
				model.TimeStart = SPublic.GetDateTimeByTime(Convert.ToInt64(row["TimeStart"]));

			if (row.Table.Columns.Contains("TimeEnd") && row["TimeEnd"] != DBNull.Value)
				model.TimeEnd = SPublic.GetDateTimeByTime(Convert.ToInt64(row["TimeEnd"]));

			if (row.Table.Columns.Contains("ClickUrl") && row["ClickUrl"] != DBNull.Value)
				model.ClickUrl = Convert.ToString(row["ClickUrl"]);

            if (row.Table.Columns.Contains("UrlShort") && row["UrlShort"] != DBNull.Value)
                model.UrlShort = Convert.ToString(row["UrlShort"]);

			if (row.Table.Columns.Contains("TotalCount") && row["TotalCount"] != DBNull.Value)
				model.TotalCount = Convert.ToInt32(row["TotalCount"]);

			if (row.Table.Columns.Contains("RemainCount") && row["RemainCount"] != DBNull.Value)
				model.RemainCount = Convert.ToInt32(row["RemainCount"]);

			if (row.Table.Columns.Contains("CommissionRate") && row["CommissionRate"] != DBNull.Value)
				model.CommissionRate = float.Parse(row["CommissionRate"].ToString());

			if (row.Table.Columns.Contains("Commission") && row["Commission"] != DBNull.Value)
				model.Commission = float.Parse(row["Commission"].ToString());

			if (row.Table.Columns.Contains("CouponInfo") && row["CouponInfo"] != DBNull.Value)
				model.CouponInfo = Convert.ToString(row["CouponInfo"]);

			if (row.Table.Columns.Contains("CouponMoney") && row["CouponMoney"] != DBNull.Value)
				model.CouponMoney = float.Parse(row["CouponMoney"].ToString());

			if (row.Table.Columns.Contains("TimeUpdate") && row["TimeUpdate"] != DBNull.Value)
				model.TimeUpdate = SPublic.GetDateTimeByTime(Convert.ToInt64(row["TimeUpdate"]));

			if (row.Table.Columns.Contains("CouponType") && row["CouponType"] != DBNull.Value)
				model.CouponType = Convert.ToInt32(row["CouponType"]);

			if (row.Table.Columns.Contains("UseCount") && row["UseCount"] != DBNull.Value)
				model.UseCount = Convert.ToInt32(row["UseCount"]);

			if (row.Table.Columns.Contains("Nick") && row["Nick"] != DBNull.Value)
				model.Nick = Convert.ToString(row["Nick"]);

			if (row.Table.Columns.Contains("SellerID") && row["SellerID"] != DBNull.Value)
				model.SellerID = Convert.ToInt64(row["SellerID"]);

			//外表

			if (row.Table.Columns.Contains("PID") && row["PID"]!=DBNull.Value)
				model.PID=Convert.ToString(row["PID"]);

			if (row.Table.Columns.Contains("TimeStartPID") && row["TimeStartPID"] != DBNull.Value)
				model.TimeStartPID = SPublic.GetDateTimeByTime(Convert.ToInt64(row["TimeStartPID"]));

			if (row.Table.Columns.Contains("TimeEndPID") && row["TimeEndPID"] != DBNull.Value)
				model.TimeEndPID = SPublic.GetDateTimeByTime(Convert.ToInt64(row["TimeEndPID"]));

			if (row.Table.Columns.Contains("ClickUrlPID") && row["ClickUrlPID"] != DBNull.Value)
				model.ClickUrlPID = Convert.ToString(row["ClickUrlPID"]);

			if (row.Table.Columns.Contains("TotalCountPID") && row["TotalCountPID"] != DBNull.Value)
				model.TotalCountPID = Convert.ToInt32(row["TotalCountPID"]);

			if (row.Table.Columns.Contains("RemainCountPID") && row["RemainCountPID"] != DBNull.Value)
				model.RemainCountPID = Convert.ToInt32(row["RemainCountPID"]);

			if (row.Table.Columns.Contains("CommissionRatePID") && row["CommissionRatePID"] != DBNull.Value)
				model.CommissionRatePID = float.Parse(row["CommissionRatePID"].ToString());

			if (row.Table.Columns.Contains("CommissionPID") && row["CommissionPID"] != DBNull.Value)
				model.CommissionPID = float.Parse(row["CommissionPID"].ToString());

			if (row.Table.Columns.Contains("CouponInfoPID") && row["CouponInfoPID"] != DBNull.Value)
				model.CouponInfoPID = Convert.ToString(row["CouponInfoPID"]);

			if (row.Table.Columns.Contains("CouponMoneyPID") && row["CouponMoneyPID"] != DBNull.Value)
				model.CouponMoneyPID = float.Parse(row["CouponMoneyPID"].ToString());

			if (row.Table.Columns.Contains("TimeUpdatePID") && row["TimeUpdatePID"] != DBNull.Value)
				model.TimeUpdatePID = SPublic.GetDateTimeByTime(Convert.ToInt64(row["TimeUpdatePID"]));

			if (row.Table.Columns.Contains("CouponTypePID") && row["CouponTypePID"] != DBNull.Value)
				model.CouponTypePID = Convert.ToInt32(row["CouponTypePID"]);

			if (row.Table.Columns.Contains("CatName") && row["CatName"] != DBNull.Value)
				model.CatName = Convert.ToString(row["CatName"]);

			return model;

		}

		/// <summary>
		/// 增加
		/// </summary>
        public long Insert(MItemsInfo model, string ConnStr)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append(" INSERT rpt_ItemsInfo(");
			sql.Append(" NumIID,Title,KeyWordStr,TitleSub,TitleDescribe,IsPush,PushTime,ImgUrl,ImgSmall,ClickUrl,UrlShort,PriceNow,CouponMoney,CouponInfo,CommissionRate,TotalCount,RemainCount,SalesCount,TimeStart,TimeEnd,CouponType,IsTmall,CatID,Navigation,IsGood,ActivityID,CreateTime,UpdateTime,TimeUpdate,IsEnable,Commission,UseCount,Nick,SellerID)");
			sql.Append(" SELECT");
			sql.Append(" @NumIID,@Title,@KeyWordStr,@TitleSub,@TitleDescribe,@IsPush,@PushTime,@ImgUrl,@ImgSmall,@ClickUrl,@UrlShort,@PriceNow,@CouponMoney,@CouponInfo,@CommissionRate,@TotalCount,@RemainCount,@SalesCount,@TimeStart,@TimeEnd,@CouponType,@IsTmall,@CatID,@Navigation,@IsGood,@ActivityID,@CreateTime,@UpdateTime,@TimeUpdate,@IsEnable,@Commission,@UseCount,@Nick,@SellerID");
			sql.Append("  SELECT @ID=@@IDENTITY");
			SqlCommand cmd = GetCommand(model);
			DbHelperSQL.RunSqlText(sql.ToString(), cmd, 3600, ConnStr);
			return Convert.ToInt64(cmd.Parameters[ID].Value);
		}

		/// <summary>
		/// 删除
		/// </summary>
        public int Delete(string strWhere, string ConnStr)
		{

			StringBuilder sql = new StringBuilder();
			sql.Append("DELETE rpt_ItemsInfo ");
			sql.Append("WHERE 1=1 " + strWhere);
			if (strWhere == "")
			{
				//清空表
				sql = new StringBuilder();
				sql.Append("TRUNCATE TABLE rpt_ItemsInfo");
			}
            return DbHelperSQL.RunSqlText(sql.ToString(), 3600, ConnStr);
		}

		/// <summary>
		/// 更新
		/// </summary>
        public int Update(MItemsInfo model, string strWhere, string updateStr, string ConnStr)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("UPDATE rpt_ItemsInfo ");
			sql.Append("SET");
			if (updateStr.Length == 0)
			{
				sql.Append("   NumIID=@NumIID,");
				sql.Append("   Title=@Title,");
				sql.Append("   KeyWordStr=@KeyWordStr,");
				sql.Append("   TitleSub=@TitleSub,");
				sql.Append("   TitleDescribe=@TitleDescribe, ");
				sql.Append("	IsPush=@IsPush, ");
				sql.Append("	PushTime=@PushTime, ");
				sql.Append("   ImgUrl=@ImgUrl,");
				sql.Append("   ImgSmall=@ImgSmall, ");
				sql.Append("   ClickUrl=@ClickUrl,");
				sql.Append("   UrlShort=@UrlShort, ");
				sql.Append("   PriceNow=@PriceNow,");
				sql.Append("   CouponMoney=@CouponMoney,");
				sql.Append("   CouponInfo=@CouponInfo,");
				sql.Append("   Commission=@Commission,");
				sql.Append("   CommissionRate=@CommissionRate,");
				sql.Append("   TotalCount=@TotalCount,");
				sql.Append("   RemainCount=@RemainCount,");
				sql.Append("   SalesCount=@SalesCount,");
				sql.Append("   UpdateTime=@UpdateTime, ");
				sql.Append("   TimeStart=@TimeStart,");
				sql.Append("   TimeEnd=@TimeEnd,");
				sql.Append("   CouponType=@CouponType,");
				sql.Append("   IsTmall=@IsTmall,");
				sql.Append("   IsEnable=@IsEnable,");
				sql.Append("   CatID=@CatID, ");
				sql.Append("   Navigation=@Navigation, ");
				sql.Append("   UseCount=@UseCount, ");
				sql.Append("   Nick=@Nick, ");
				sql.Append("   SellerID=@SellerID ");
			}
			else
			{
				SPublic.GetUpdateString(updateStr, ref sql);
			}
			//sql.Append("FROM rpt_ItemsInfo a LEFT JOIN rpt_ItemsShop b ON b.NumIID = a.NumIID ");
			sql.Append(" WHERE 1=1" + strWhere);
			SqlCommand cmd = GetCommand(model);
            return DbHelperSQL.RunSqlText(sql.ToString(), cmd, 3600, ConnStr);
		}

		/// <summary>
		/// 获取数据列表【多张表】
		/// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string selectStr, string ConnStr)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append(" SELECT  ");

            sql.Append("  a.ID, ");
				sql.Append("	a.NumIID, ");
				sql.Append("	a.Title, ");
				sql.Append("	a.KeyWordStr, ");
				sql.Append("	a.TitleSub, ");
				sql.Append("    a.TitleDescribe, ");
				sql.Append("	a.IsPush, ");
				sql.Append("	a.PushTime, ");
				sql.Append("	a.CatID,");
                sql.Append("   a.Navigation,");
				sql.Append("	a.ImgUrl, ");
				sql.Append("	a.ImgSmall, ");
				sql.Append("	a.PriceNow, ");
				sql.Append("	a.IsTmall,");
				sql.Append("	a.SalesCount, ");
				sql.Append("	a.IsGood, ");
				sql.Append("	a.CreateTime, ");
				sql.Append("	a.UpdateTime,");
				sql.Append("	a.ActivityID, ");
				sql.Append("	a.TimeStart, ");
				sql.Append("	a.TimeEnd, ");
				sql.Append("	a.ClickUrl,");
                sql.Append("  a.UrlShort, ");
				sql.Append("	a.TotalCount, ");
				sql.Append("	a.RemainCount, ");
				sql.Append("	a.CommissionRate, ");
				sql.Append("	a.Commission,");
				sql.Append("	a.CouponInfo, ");
				sql.Append("	a.CouponMoney, ");
				sql.Append("	a.TimeUpdate, ");
				sql.Append("	a.CouponType, ");
				sql.Append("	a.UseCount, ");
				sql.Append("	a.Nick, ");
				sql.Append("	a.SellerID, ");
				sql.Append("	b.CatName ");

				sql.Append("         FROM rpt_ItemsInfo a LEFT JOIN rpt_Category b ON b.CatID = a.CatID ");
				sql.Append("         WHERE 1=1" + strWhere);
            return DbHelperSQL.RunSqlText(sql.ToString(), pageIndex, pageSize, 3600, ConnStr);
		}

		/// <summary>
		/// 获取实体列表
		/// </summary>
        public MItemsInfo GetModel(string strWhere, string selectStr, string ConnStr)
		{
			using (DataTable dTable = GetList(0, 0, strWhere, selectStr,  ConnStr).Tables[0])
			{
				if (dTable.Rows.Count > 0)
					return GetModel(dTable.Rows[0]);
				else
					return null;
			}
		}

		/// <summary>
		/// 获取实体列表
		/// </summary>
        public List<MItemsInfo> GetModelList(int pageSize, int pageIndex, string strWhere, string selectStr, string ConnStr)
		{
			List<MItemsInfo> modelList = new List<MItemsInfo>();
			using (DataTable dTable = GetList(pageSize, pageIndex, strWhere, selectStr,  ConnStr).Tables[0])
			{
				foreach (DataRow row in dTable.Rows)
				{
					modelList.Add(GetModel(row));
				}
			}
			return modelList;
		}
		#endregion

		#region 个性函数
		/// <summary>
		/// 获取商品类目并按商品数量排序
		/// </summary>
		public List<MItemsInfo> GetModelCat(int pageSize, int pageIndex, string strWhere,  string ConnStr)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("  SELECT  ");
			sql.Append("  CatID, ");
			sql.Append("  COUNT(*) ");
			sql.Append("  FROM rpt_ItemsInfo ");
			sql.Append("  WHERE 1=1 " + strWhere);
			sql.Append("  GROUP BY CatID ");
			sql.Append("  ORDER BY COUNT(*) DESC ");
			List<MItemsInfo> modellist = new List<MItemsInfo>();
			using (SqlDataReader reader = DbHelperSQL.ExecuteReader(sql.ToString(),ConnStr))
			{
				while (reader.Read())
				{
					MItemsInfo model = new MItemsInfo();
					model.CatID = Convert.ToInt32(reader["CatID"]);
					modellist.Add(model);
				}
			}
			return modellist;
		}


		/// <summary>
		/// 商品转图集
		/// </summary>
		public List<MItemsInfo> GetModelListnumber(int pageIndex, int pageSize, string strWhere, int number, int top, string catid, string ConnStr)
		{
			List<MItemsInfo> modelList = new List<MItemsInfo>();
			StringBuilder sql = new StringBuilder();
			sql.Append("    SELECT " );
			sql.Append("    TOP " + number);

			sql.Append("  a.ID, ");
			sql.Append("	a.NumIID, ");
			sql.Append("	a.Title, ");
			sql.Append("	a.KeyWordStr, ");
			sql.Append("	a.TitleSub, ");
			sql.Append("    a.TitleDescribe, ");
			sql.Append("	a.IsPush, ");
			sql.Append("	a.PushTime, ");
			sql.Append("	a.CatID,");
			sql.Append("    a.Navigation,");
			sql.Append("	a.ImgUrl, ");
			sql.Append("	a.ImgSmall, ");
			sql.Append("	a.PriceNow, ");
			sql.Append("	a.IsTmall,");
			sql.Append("	a.SalesCount, ");
			sql.Append("	a.IsGood, ");
			sql.Append("	a.CreateTime, ");
			sql.Append("	a.UpdateTime,");
			sql.Append("	a.ActivityID, ");
			sql.Append("	a.TimeStart, ");
			sql.Append("	a.TimeEnd, ");
			sql.Append("	a.ClickUrl,");
			sql.Append("    a.UrlShort, ");
			sql.Append("	a.TotalCount, ");
			sql.Append("	a.RemainCount, ");
			sql.Append("	a.CommissionRate, ");
			sql.Append("	a.Commission,");
			sql.Append("	a.CouponInfo, ");
			sql.Append("	a.CouponMoney, ");
			sql.Append("	a.TimeUpdate, ");
			sql.Append("	a.CouponType, ");
			sql.Append("	a.UseCount, ");
			sql.Append("	a.Nick, ");
			sql.Append("	a.SellerID, ");
			sql.Append("	b.CatName ");
			sql.Append("    FROM rpt_ItemsInfo a ");
			sql.Append("  LEFT JOIN rpt_Category b ON b.CatID = a.CatID  ");
			sql.Append("    WHERE 1=1" + strWhere);
			sql.Append("    AND a.ID NOT IN (SELECT TOP " + top + " ID FROM rpt_ItemsInfo WHERE 1=1 " + strWhere.Replace("a.", "") + " )");
			using (DataTable dTable = DbHelperSQL.RunSqlText(sql.ToString(), pageIndex, pageSize, 3600, ConnStr).Tables[0])
			{
				foreach (DataRow row in dTable.Rows)
				{
					modelList.Add(GetModel(row));
				}
			}
			return modelList;
		}

		#endregion
		
	}
}
