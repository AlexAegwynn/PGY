using System;
using System.Collections.Generic;

using CM.IDAL;
using CM.DALFactory;
using CM.Model;
using CM.SQLServerDAL;

namespace CM.BLL
{
	/// <summary>
	/// 商品基本信息
	/// </summary>
    public class ItemsInfo
    {
        private readonly IItemsInfo dal = DataAccess.CreateItemsInfo();

        private string ConnStr = "";

        public ItemsInfo(string _ConnStr)
        {
            ConnStr = _ConnStr;
        }

        #region Insert

        public long Insert(MItemsInfo model)
        {
            return dal.Insert(model, ConnStr);
        }
        #endregion

        #region Delete

        public int DeleteItemsInfoID(long numIID)
        {
            string strWhere = "AND NumIID =" + numIID;
            return dal.Delete(strWhere, ConnStr);
        }

		public int DeleteItemsInfoKey(string KeyWord)
		{
			string strWhere = "AND KeyWord ='" + KeyWord + "'";
			return dal.Delete(strWhere, ConnStr);
		}

        public int DeleteItemsInfo()
        {
            string strWhere = "";
            strWhere += " AND TimeEnd <" + SPublic.GetTimeByDateTime(DateTime.Now);
            return dal.Delete(strWhere, ConnStr);
        }

        #endregion

        #region Update

        /// <summary>
        /// 根据 主键编号 更新
        /// </summary>
        public int UpdateByItemsInfoID(MItemsInfo model)
        {
            string strWhere = " AND NumIID = " + model.NumIID;
            return dal.Update(model, strWhere, "", ConnStr);
        }

		public int UpdateByStr(MItemsInfo model, string entity)
		{
			string strWhere = " AND NumIID = " + model.NumIID;
			return dal.Update(model, strWhere, entity, ConnStr);
		}

        #endregion

        #region Model
        /// <summary>
        /// 根据商品编号获取数据
        /// </summary>
        public MItemsInfo GetModelByItemsInfoID(long numIID)
        {
            string strWhere = " AND a.NumIID = " + numIID;
            return dal.GetModel(strWhere, "", ConnStr);
        }
        /// <summary>
        /// 根据编号获取数据
        /// </summary>
        public MItemsInfo GetModelByID(long ID)
        {
            string strWhere = " AND a.ID = " + ID;
            return dal.GetModel(strWhere, "", ConnStr);
        }
        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        public List<MItemsInfo> GetModelListAll()
        {
            return dal.GetModelList(0, 0, "", "", ConnStr);
        }

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        public List<MItemsInfo> GetModelListAll(string strWhere)
        {
            return dal.GetModelList(0, 0, strWhere, "", ConnStr);
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        public MItemsInfo GetModel(string strWhere)
        {
            return dal.GetModel(strWhere, "", ConnStr);
        }
        /// <summary>
        /// 获取全部商品信息
        /// </summary>
        public MItemsInfo GetModel()
        {
            return dal.GetModel("", "", ConnStr);
        }
        #endregion

        /// <summary>
        /// 模糊搜索，关键词，宝贝名称
        /// </summary>
        public List<MItemsInfo> GetModelListByTitle(string title)
        {
            string strWhere = "";
            strWhere += " AND Title LIKE '%" + title + "%'";
            strWhere += " AND (a.UrlShort !='' OR a.ClickUrl !='') ";
            return dal.GetModelList(0, 0, strWhere, "", ConnStr);
        }

        public List<MItemsInfo> GetModelRptList(decimal tkRateStart, decimal tkRateEnd, decimal persentStart, decimal persentEnd, decimal priceStart, decimal priceEnd, int saleStart, int saleEnd, int IsTmall, string catName)
        {
            string strWhere = "";
            strWhere += " AND a.TimeEnd > " + SPublic.GetTimeByDateTime(DateTime.Now);
            strWhere += " AND a.CouponMoney >0";

            if (IsTmall > -1)
            {
                strWhere += " AND a.IsTmall =" + IsTmall;
            }
            if (catName != null)
            {
                if (catName == "全部")
                {

                }
                else
                {
                    strWhere += " AND b.CatName ='" + catName + "'";
                }
            }

            if (tkRateStart >= 0 && tkRateEnd > 0)
            {
                strWhere += " AND a.Commission BETWEEN " + tkRateStart + " AND " + tkRateEnd;
            }
            if (persentStart >= 0 && persentEnd > 0)
            {
                strWhere += " AND a.CouponMoney BETWEEN " + persentStart + " AND " + persentEnd;
            }
            if (priceStart >= 0 && priceEnd > 0)
            {
                strWhere += " AND a.PriceNow BETWEEN " + priceStart + " AND " + priceEnd;
            }
            if (saleStart >= 0 && saleEnd > 0)
            {
                strWhere += " AND a.SalesCount BETWEEN " + saleStart + " AND " + saleEnd;
            }
            return dal.GetModelList(0, 0, strWhere, "", ConnStr);
        }

        public List<MItemsInfo>GetModelRptListS(long TimeEnd1, long TimeEnd2, decimal tkRateStart, decimal tkRateEnd, decimal persentStart, decimal persentEnd, decimal priceStart, decimal priceEnd, int saleStart, int saleEnd, int IsTmall, string catName)
        {
            string strWhere = "";
            strWhere += " AND a.IsEnable=1";

            if (TimeEnd1 >= 0 && TimeEnd2 > 0)
            {
                strWhere += " AND a.TimeEnd BETWEEN " + TimeEnd1 + " AND " + TimeEnd2;
            }
            else if (TimeEnd1 > 0 && TimeEnd2 <= 0)
            {
                strWhere += " AND a.TimeEnd >=" + TimeEnd1;
            }

            if (IsTmall > -1)
            {
                strWhere += " AND a.IsTmall =" + IsTmall;
            }
            if (catName != null)
            {
                if (catName == "全部")
                {

                }
                else
                {
                    strWhere += " AND b.CatName ='" + catName + "'";
                }
            }

            if (tkRateStart >= 0 && tkRateEnd > 0)
            {
                strWhere += " AND a.CommissionRate BETWEEN " + tkRateStart + " AND " + tkRateEnd;
            }
            if (persentStart >= 0 && persentEnd > 0)
            {
                strWhere += " AND a.CouponMoney BETWEEN " + persentStart + " AND " + persentEnd;
            }
            if (priceStart >= 0 && priceEnd > 0)
            {
                strWhere += " AND a.PriceNow BETWEEN " + priceStart + " AND " + priceEnd;
            }
            if (saleStart >= 0 && saleEnd > 0)
            {
                strWhere += " AND a.SalesCount BETWEEN " + saleStart + " AND " + saleEnd;
            }
            strWhere += " AND (a.UrlShort !='' OR a.ClickUrl !='') ";
            return dal.GetModelList(0, 0, strWhere, "", ConnStr);
        }

        public List<MItemsInfo> GetModelListByTime(long TimeStart1, long TimeStart2, long TimeEnd1, long TimeEnd2, long CreateTime1, long CreateTime2)
        {
            string strWhere = "";
            strWhere += " AND a.TimeEnd > " + SPublic.GetTimeByDateTime(DateTime.Now);
            strWhere += " AND a.CouponMoney >0";

            if (TimeStart1 >= 0 && TimeStart2 > 0)
            {
                strWhere += " AND a.TimeStart BETWEEN " + TimeStart1 + " AND " + TimeStart2;
            }
            if (TimeEnd1 >= 0 && TimeEnd2 > 0)
            {
                strWhere += " AND a.TimeEnd BETWEEN " + TimeEnd1 + " AND " + TimeEnd2;
            }
            if (CreateTime1 >= 0 && CreateTime2 > 0)
            {
                strWhere += " AND a.CreateTime BETWEEN " + CreateTime1 + " AND " + CreateTime2;
            }

            return dal.GetModelList(0, 0, strWhere, "", ConnStr);
        }

        public List<MItemsInfo> GetModelListByOverdue()
        {
            string strWhere = "";
            strWhere += " AND a.TimeEnd <" + SPublic.GetTimeByDateTime(DateTime.Now);
            strWhere += " AND a.CouponMoney >0";
            return dal.GetModelList(0, 0, strWhere, "", ConnStr);
        }

		/// <summary>
		/// 根据店铺名称获取数据
		/// </summary>
		public List<MItemsInfo> GetModelListByNick(string Nick)
		{
			string strWhere = "";
			if (Nick.Trim() != "")
				strWhere += " AND a.Nick = '" + Nick + "'";
			return dal.GetModelList(0, 0, strWhere, "", ConnStr);
		}
		
		public List<MItemsInfo> GetModelListByNick(string date,string Nick)
		{
			string strWhere = "";
			if (date.Trim() != "")
			{
				strWhere += " AND (a.TimeEnd >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date).AddDays(15)) + " OR a.TimeEnd=0) ";
				strWhere += " AND a.Commission!=0 ";
			}
			if (Nick.Trim() != "")
				strWhere += " AND a.Nick like '%" + Nick + "%'";
			return dal.GetModelList(0, 0, strWhere, "", ConnStr);
		}

        /// <summary>
        /// 根据商品编号和PID获取数据
        /// </summary>
        public MItemsInfo GetListByNumIIDorPID(long NumIID, string PID)
        {
            string strWhere = "";
			if (NumIID > 0)
				strWhere += " AND a.NumIID=" + NumIID;
			if(PID!="")
				strWhere += " AND a.PID='" + PID + "'";
            return dal.GetModel(strWhere, "", ConnStr);
        }
        /// <summary>
        /// 根据类目编号获取数据
        /// </summary>
        public List<MItemsInfo> GetModelListByCatID(long CatID)
        {
            string strWhere = " AND a.CatID=" + CatID;
            return dal.GetModelList(0, 0, strWhere, "", ConnStr);
        }
        /// <summary>
        /// 根据优惠时间和佣金比率获取数据
        /// </summary>
        public List<MItemsInfo> GetModelListByOther(string date, float CommissionRate)
        {
            string strWhere = "";
            if (date != "")
                strWhere += " AND a.TimeEnd>=" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date));
            if (CommissionRate > 0)
                strWhere += " AND a.CommissionRate>=" + CommissionRate;
            return dal.GetModelList(0, 0, strWhere, "", ConnStr);
        }
        /// <summary>
        /// 根据商品编号字符串获取数据
        /// </summary>
        public List<MItemsInfo> GetModelListByNumIIDS(string numiids)
        {
            string strWhere = "";
            if (numiids != "")
                strWhere += " AND a.NumIID IN (" + numiids + ")";
            return dal.GetModelList(0, 0, strWhere, "", ConnStr);
        }
		/// <summary>
		/// 根据编号字符串获取数据
		/// </summary>
		public List<MItemsInfo> GetModelListByIDS(string ids)
		{
			string strWhere = "";
			if (ids != "")
				strWhere += " AND a.ID IN (" + ids + ")";
			return dal.GetModelList(0, 0, strWhere, "", ConnStr);
		}
		/// <summary>
		/// 创建时间不超过7天和结束时间不是当前时间的天猫商品
		/// </summary>
		public List<MItemsInfo> GetModelListByCreateANDEndTime(string date)
		{
			string strWhere = "";
			if (date.Trim() != "")
			{
				strWhere += " AND a.TimeEnd >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date));
				strWhere += " AND a.CreateTime >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date).AddDays(-7));
			}
			//strWhere += " AND a.IsTmall = 1 ";
			return dal.GetModelList(0, 0, strWhere, "", ConnStr);
		}

		public List<MItemsInfo> GetModelListByTitleANDTime(string date, string title)
		{
			string strWhere = "";
			if (date.Trim() != "")
			{
				//strWhere += " AND (a.TimeEnd >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date)) + " OR a.TimeEnd=0 )";
				strWhere += " AND (a.TimeEnd >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date).AddDays(15)) + " OR a.TimeEnd=0) ";
				strWhere += " AND a.Commission!=0 ";
				//strWhere += " AND (a.CreateTime >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date).AddDays(-7)) + " OR a.UpdateTime >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date).AddDays(-7)) + ") ";
			}
			if (title.Trim() != "")
			{
				//strWhere += " AND (";
				string[] tarr = title.Split(',');
				for (int i = 0; i < tarr.Length; i++)
				{
					if (i == 0)
						strWhere += " AND ((";
					else
						strWhere += " OR ((";
					char[] TitleStr = tarr[i].ToCharArray();

					for (int a = 0; a <= TitleStr.Length - 1; a++)
					{
						if (a < TitleStr.Length - 1)
							strWhere += " a.Title LIKE '%" + TitleStr[a] + "%' AND ";
						if (a == TitleStr.Length - 1)
							strWhere += " a.Title LIKE '%" + TitleStr[a] + "%' ";
					}
					
					strWhere += " )OR a.Title LIKE '%" + tarr[i] + "%' ) ";
				}
				strWhere += " AND a.Title not LIKE '%2017%' AND a.SalesCount>=10 ";
				//strWhere += ") ";
			}
			strWhere += " AND a.IsEnable = 1 ";
			return dal.GetModelList(0, 0, strWhere, "", ConnStr);
		}

		public List<MItemsInfo> GetModelListByTitleANDTime(string date, string title, bool TitleDescribe)
		{
			string strWhere = "";
			if (date.Trim() != "")
			{
				//strWhere += " AND (a.TimeEnd >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date)) + " OR a.TimeEnd=0 )";
				strWhere += " AND (a.TimeEnd >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date).AddDays(15)) + " OR a.TimeEnd=0) ";
				strWhere += " AND a.Commission!=0 ";
				//strWhere += " AND (a.CreateTime >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date).AddDays(-7)) + " OR a.UpdateTime >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date).AddDays(-7))+") ";
			}
			if (title.Trim() != "")
			{
				//strWhere += " AND (";
				string[] tarr = title.Split(',');
				for (int i = 0; i < tarr.Length;i++ )
				{
					//if (i > 0)
					//    strWhere += " OR ";
					//strWhere += " a.Title LIKE '%" + tarr[i] + "%' AND a.Title not LIKE '%2017%'";
					strWhere += " AND ((";
					char[] TitleStr = tarr[i].ToCharArray();

					for (int a = 0; a <= TitleStr.Length - 1; a++)
					{
						if (a < TitleStr.Length - 1)
							strWhere += " a.Title LIKE '%" + TitleStr[a] + "%' AND ";
						if (a == TitleStr.Length - 1)
							strWhere += " a.Title LIKE '%" + TitleStr[a] + "%' ";
					}

					strWhere += " )OR a.Title LIKE '%" + tarr[i] + "%' AND a.Title not LIKE '%2017%' ) AND a.SalesCount>=10 ";
				}
				//strWhere += ") ";
				
			}
			if (TitleDescribe)
				strWhere += " AND a.TitleDescribe!='' ";
			strWhere += " AND a.IsEnable = 1 ";
			return dal.GetModelList(0, 0, strWhere, "", ConnStr);
		}

		public List<MItemsInfo> GetModelListByTitleText(string date,string title)
		{
			string strWhere = "";
			//if (date.Trim() != "")
			//{
			//    strWhere += " AND (a.TimeEnd >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date)) + " OR a.TimeEnd=0 )";
			//    strWhere += " AND a.CreateTime >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date).AddDays(-7));
			//}
			strWhere += " AND (";
			char[] TitleStr = title.ToCharArray();
			 
			for (int i = 0; i <= TitleStr.Length-1; i++)
			{
				if (i < TitleStr.Length-1)
					strWhere += " a.Title LIKE '%" + TitleStr[i] + "%' AND ";
				if (i == TitleStr.Length-1)
					strWhere += " a.Title LIKE '%" + TitleStr[i] + "%' ";
			}

				strWhere += " )OR a.Title LIKE '%" + title + "%' AND a.Title not LIKE '%2017%'";
			return dal.GetModelList(0, 0, strWhere, "", ConnStr);
		}

		public List<MItemsInfo> GetModelListByTitleANDTimeC(string date, string title,string catid)
		{
			string strWhere = "";
			if (date.Trim() != "")
			{
				strWhere += " AND (a.TimeEnd >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date)) + " OR a.TimeEnd=0 )";
				strWhere += " AND a.CreateTime >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date).AddDays(-7));
			}
			if (title.Trim() != "")
			{
				strWhere += " AND (";
				string[] tarr = title.Split(',');
				for (int i = 0; i < tarr.Length; i++)
				{
					if (i > 0)
						strWhere += " OR ";
					strWhere += " a.Title LIKE '%" + tarr[i] + "%' ";
				}
				strWhere += ") ";
			}
			if (catid.Trim() != "")
				strWhere += " AND a.CatID IN (" + catid + ")";
			return dal.GetModelList(0, 0, strWhere, "", ConnStr);
		}

		public List<MItemsInfo> GetModelListByTitleANDCatStr(string date, string title, string catid)
		{
			string strWhere = "";
			if (date.Trim() != "")
			{
				strWhere += " AND (a.TimeEnd >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(date).AddDays(15)) + " OR a.TimeEnd=0) ";
				strWhere += " AND a.Commission!=0 ";
			}
			if (title.Trim() != "")
			{
				string[] tarr = title.Split(',');
				for (int i = 0; i < tarr.Length; i++)
				{
					if (i == 0)
						strWhere += " AND ((";
					else
						strWhere += " OR ((";
					char[] TitleStr = tarr[i].ToCharArray();

					for (int a = 0; a <= TitleStr.Length - 1; a++)
					{
						if (a < TitleStr.Length - 1)
							strWhere += " a.Title LIKE '%" + TitleStr[a] + "%' AND ";
						if (a == TitleStr.Length - 1)
							strWhere += " a.Title LIKE '%" + TitleStr[a] + "%' ";
					}

					strWhere += " )OR a.Title LIKE '%" + tarr[i] + "%' ) ";
				}
				strWhere += " AND a.Title not LIKE '%2017%' AND a.SalesCount>=10 ";
			}
			if (catid.Trim() != "")
				strWhere += " AND a.CatID IN (" + catid + ")";
			strWhere += " AND a.IsEnable = 1 ";
			return dal.GetModelList(0, 0, strWhere, "", ConnStr);
		}

		public List<MItemsInfo> GetModelListByTitleANDTimeC(decimal tkRateStart, decimal tkRateEnd, string title, string catidstr,int tss)
		{
			string strWhere = "";
			if (tss == 1)
				strWhere += " AND a.TimeEnd >" + SPublic.GetTimeByDateTime(DateTime.Now.AddDays(15));
			else
				strWhere += " AND (a.TimeEnd >" + SPublic.GetTimeByDateTime(DateTime.Now.AddDays(15)) + " OR a.TimeEnd=0) ";
			if (tkRateStart >= 0 && tkRateEnd > 0)
			{
				strWhere += " AND a.CommissionRate BETWEEN " + tkRateStart + " AND " + tkRateEnd;
			}

			if (title.Trim() != "")
			{
				strWhere += " AND (";
				string[] tarr = title.Split(',');
				for (int i = 0; i < tarr.Length; i++)
				{
					if (i > 0)
						strWhere += " OR ";
					strWhere += " a.Title LIKE '%" + tarr[i] + "%' ";
				}
				strWhere += ") ";
			}
			if (catidstr.Trim() != "")
				strWhere += " AND a.CatID IN (" + catidstr + ")";
            strWhere += " AND a.IsEnable = 1 ";
			return dal.GetModelList(0, 0, strWhere, "", ConnStr);
		}

		public List<MItemsInfo> GetModelListByTime(string endtime, string creattime)
		{
			string strWhere = "";
			if(endtime.Trim()!="")
				strWhere += " AND a.TimeEnd >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(endtime));
			if (creattime.Trim() != "")
				strWhere += " AND a.CreateTime >" + SPublic.GetTimeByDateTime(Convert.ToDateTime(creattime));
			return dal.GetModelList(0, 0, strWhere, "", ConnStr);
		}
		/// <summary>
		/// 获取商品类目并按商品数量排序
		/// </summary>
		public List<MItemsInfo> GetModelCat(string title)
		{
			string strWhere = "";
			if (title.Trim() != "")
			{
				strWhere += " AND (";
				string[] tarr = title.Split(',');
				for (int i = 0; i < tarr.Length; i++)
				{
					if (i > 0)
						strWhere += " OR ";
					strWhere += " Title LIKE '%" + tarr[i] + "%' ";
				}
				strWhere += ") ";
			}
			strWhere += " AND IsEnable = 1 ";
			return dal.GetModelCat(0, 0, strWhere, ConnStr);
		}

		/// <summary>
		/// 商品转图文
		/// </summary>
		/// <param name="keystr"></param>
		/// <param name="number"></param>
		/// <param name="top"></param>
		/// <returns></returns>
		public List<MItemsInfo> GetModelListnumber(string keystr, int number, int top, string catid)
		{
			string strWhere = "";
			if (keystr.Trim() != "")
			{
				strWhere += " AND (";
				string[] keyarr = keystr.Split(',');
				for (int i = 0; i < keyarr.Length; i++)
				{
					if (i > 0)
						strWhere += " OR ";
					strWhere += " a.Title Like '%" + keyarr[i] + "%'";
				}
				strWhere += ")";
			}
			//strWhere += " AND a.Count=0";
			return dal.GetModelListnumber(0, 0, strWhere, number, top, catid, ConnStr);
		}
    }
}
