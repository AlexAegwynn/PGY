using System;
using System.Collections.Generic;

using CM.Model;

namespace CM.IDAL
{
	/// <summary>
	/// 商品基本信息
	/// </summary>
	public interface IItemsInfo
	{

		#region 增加

        long Insert(MItemsInfo model, string ConnStr);

		#endregion

		#region 删除

        int Delete(string strWhere, string ConnStr);

		#endregion

		#region 更新

        int Update(MItemsInfo model, string strWhere, string updateStr, string ConnStr);

		#endregion

		#region model

		//获取信息根据编号
        MItemsInfo GetModel(string strWhere, string selectStr, string ConnStr);

		//获取实体列表
        List<MItemsInfo> GetModelList(int pageSize, int pageIndex, string strWhere, string selectStr, string ConnStr);
		
		#endregion
		/// <summary>
		/// 获取商品类目并按商品数量排序
		/// </summary>
		List<MItemsInfo> GetModelCat(int pageSize, int pageIndex, string strWhere,  string ConnStr);

		/// <summary>
		/// 商品生成图文
		/// </summary>
		List<MItemsInfo> GetModelListnumber(int pageSize, int pageIndex, string strWhere, int number, int top, string catid, string ConnStr);

	}
}
