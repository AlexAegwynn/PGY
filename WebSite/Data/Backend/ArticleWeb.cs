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
	public class ArticleWeb
	{
		private readonly IArticleWeb dal = DataAccess.CreateArticleWeb();

		 private string ConnStr = "";

		 public ArticleWeb(string _ConnStr)
		 {
			 ConnStr = _ConnStr;
		 }

		 public ArticleWeb()
		 {

		 }

		#region Insert
		/// <summary>
		/// 增加
		/// </summary>
		public long Insert(MArticleWeb model)
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
		public int Update(MArticleWeb model)
		{
			return dal.Update(model, ConnStr);
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
		public MArticleWeb GetModel(int ID)
		{
			string strWhere = " AND a.ID = " + ID;
			return dal.GetModel(strWhere, ConnStr);
		}
		/// <summary>
		/// 获取所有数据列表
		/// </summary>
		public List<MArticleWeb> GetModelListAll()
		{
			return dal.GetModelList(0, 0, "", ConnStr);
		}
		/// <summary>
		/// 根据网站编号获取数据
		/// </summary>
		public List<MArticleWeb> GetModelListByWID(int WID)
		{
			string strWhere = "";
			if (WID > 0)
				strWhere += " AND a.WID =" + WID;
			return dal.GetModelList(0, 0, strWhere, ConnStr);
		}
		#endregion
	}
}
