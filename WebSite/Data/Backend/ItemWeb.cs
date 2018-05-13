using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CM.Model.Web;
using CM.IDAL.Web;
using System.Data;
using CM.DALFactory;

namespace CM.BLL.Web
{
	public class ItemWeb
	{
		private readonly IItemWeb dal = DataAccess.CreateItemWeb();

		private string ConnStr = "";

		 public ItemWeb(string _ConnStr)
		 {
			 ConnStr = _ConnStr;
		 }

		 public ItemWeb()
		 {

		 }

		#region Insert
		/// <summary>
		/// 增加
		/// </summary>
		public long Insert(MItemWeb model)
		{
			return dal.Insert(model, ConnStr);
		}
		#endregion

		#region Delete
		/// <summary>
		/// 根据 主键编号 删除
		/// </summary>
		public int DeleteById(int ID)
		{
			string strWhere = " AND ID = " + ID;
			return dal.Delete(strWhere, ConnStr);
		}
		/// <summary>
		/// 根据商品编号删除数据
		/// </summary>
		public int DeleteByIID(long IID)
		{
			string strWhere = " AND IID = " + IID;
			return dal.Delete(strWhere, ConnStr);
		}
		/// <summary>
		/// 清空表
		/// </summary>
		public int DeleteAll()
		{
			return dal.Delete("", ConnStr);
		}
		#endregion

		#region Update
		/// <summary>
		/// 根据 主键编号 更新
		/// </summary>
		public int Update(MItemWeb model)
		{
			return dal.Update(model, "", ConnStr);
		}
		#endregion

		#region Table
		/// <summary>
		/// 获取分页数据列表
		/// </summary>
		private DataSet GetList(int pageSize, int pageIndex, string strWhere)
		{
			return dal.GetList(pageSize, pageIndex, strWhere, ConnStr);
		}
		/// <summary>
		/// 获取所有数据列表
		/// </summary>
		public DataSet GetListAll()
		{
			return dal.GetList(0, 0, "", ConnStr);
		}
		#endregion

		#region Model
		/// <summary>
		/// 获取一个对象实体
		/// </summary>
		public MItemWeb GetModel(int ID)
		{
			string strWhere = " AND a.ID = " + ID;
			return dal.GetModel(strWhere, ConnStr);
		}

		/// <summary>
		/// 获取一个对象实体
		/// </summary>
		public List<MItemWeb> GetModelList(int  WID, int IID)
		{
			string StrWhere = "";
			if (WID > 0)
				StrWhere += " AND a.WID = " + WID;
			if (IID > 0)
				StrWhere += "AND a.IID = " + IID + "";
			return dal.GetModelList(0, 0, StrWhere, ConnStr);
		}
		
		public List<MItemWeb> GetModeListByOther(int WID, string IIDStr)
		{
			string strWhere = "";
			if (WID > 0)
				strWhere += " AND a.WID = " + WID;
			if (IIDStr.Trim() != "")
				strWhere += " AND a.IID IN (" + IIDStr + ")";
			return dal.GetModelList(0, 0, strWhere, ConnStr);
		}

		public List<MItemWeb> GetModeListByOtherTop(int WID, string IIDStr,int top,int top2)
		{
			string strWhere = "";
			if (WID > 0)
				strWhere += " AND a.WID = " + WID;
			if (IIDStr.Trim() != "")
				strWhere += " AND a.IID IN (" + IIDStr + ")";

			return dal.GetModelListByTop(0, 0, strWhere, top, top2, ConnStr);
		}

		public int GetModelCount(int WID, string IIDStr)
		{
			string strWhere = "";
			if (WID > 0)
				strWhere += " AND a.WID = " + WID;
			if (IIDStr.Trim() != "")
				strWhere += " AND a.IID IN (" + IIDStr + ")";
			return dal.GetModelCount(0, 0, strWhere, ConnStr);
		}
		/// <summary>
		/// 获取所有数据列表
		/// </summary>
		public List<MItemWeb> GetModelListAll()
		{
			return dal.GetModelList(0, 0, "", ConnStr);
		}
        #endregion

        public List<MItemWeb> GetModeListByTitle(int WID, int IID,string keyWord)
        {
            string strWhere = "";
            if (WID > 0)
                strWhere += " AND a.WID = " + WID;
            if (IID > 0)
                strWhere += "AND a.IID = " + IID + "";
            if (keyWord.Trim() != "")
                strWhere += " AND c.Title like '%" + keyWord + "%'";
            return dal.GetModelList(0, 0, strWhere, ConnStr);
        }
	}
}
