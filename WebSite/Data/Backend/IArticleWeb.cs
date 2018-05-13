using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CM.Model.Web;
using System.Data;

namespace CM.IDAL.Web
{
	public interface IArticleWeb
	{
		/// <summary>
		/// 增加
		/// </summary>
		int Insert(MArticleWeb model, string ConnStr);
		/// <summary>
		/// 删除
		/// </summary>
		int Delete(string strWhere, string ConnStr);
		/// <summary>
		/// 更新
		/// </summary>
		int Update(MArticleWeb model, string ConnStr);
		/// <summary>
		/// 获取数据列表
		/// </summary>
		DataSet GetList(int pageSize, int pageIndex, string strWhere, string ConnStr);
		/// <summary>
		/// 获取实体信息
		/// </summary>
		MArticleWeb GetModel(string strWhere, string ConnStr);
		/// <summary>
		/// 获取实体列表
		/// </summary>
		List<MArticleWeb> GetModelList(int pageSize, int pageIndex, string strWhere, string ConnStr);
	}
}
