using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CM.Model.Web;
using System.Data;

namespace CM.IDAL.Web
{
	public interface IItemWeb
	{
		#region 增加

		int Insert(MItemWeb model, string ConnStr);

		#endregion

		#region 删除

		int Delete(string strWhere, string ConnStr);

		#endregion

		#region 更新

		int Update(MItemWeb model, string strWhere, string ConnStr);

		#endregion
		/// <summary>
		/// 获取数据列表
		/// </summary>
		DataSet GetList(int pageSize, int pageIndex, string strWhere, string ConnStr);

		#region model

		//获取信息根据编号
		MItemWeb GetModel(string strWhere, string ConnStr);

		//获取实体列表
		List<MItemWeb> GetModelList(int pageSize, int pageIndex, string strWhere, string ConnStr);
		/// <summary>
		/// 获取指定分页数据
		/// </summary>
		List<MItemWeb> GetModelListByTop(int pageSize, int pageIndex, string strWhere, int top, int top2, string ConnStr);
		/// <summary>
		/// 获取指定条件的数据数量
		/// </summary>
		int GetModelCount(int pageSize, int pageIndex, string strWhere, string ConnStr);
		#endregion
	}
}
