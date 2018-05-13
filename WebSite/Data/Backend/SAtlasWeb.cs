using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using CM.Model.Web;
using CM.IDAL.Web;
using System.Data;
using CM.DBUtility;

namespace CM.SQLServerDAL.Web
{
	class SAtlasWeb:IAtlasWeb
	{
		#region 局部变量
		private const string ID = "@ID";
		private const string WID = "@WID";
		private const string ArticleID = "@ArticleID";
		#endregion 

		#region 通用函数
		private SqlCommand GetCommand(MAtlasWeb model)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.Parameters.Add(new SqlParameter(ID, SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter(WID, SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter(ArticleID, SqlDbType.BigInt));

			cmd.Parameters[ID].Value = model.ID;
			cmd.Parameters[WID].Value = model.WID;
			cmd.Parameters[ArticleID].Value = model.ArticleID;
			return cmd;
		}
		/// <summary>
		/// 实体信息
		/// </summary>
		private MAtlasWeb GetModel(DataRow row)
		{
			MAtlasWeb model = new MAtlasWeb();
			model.ID = Convert.ToInt32(row["ID"]);
			model.WID = Convert.ToInt32(row["WID"]);
			model.ArticleID = Convert.ToInt64(row["ArticleID"]);

			model.WName = Convert.ToString(row["WName"]);
			model.Title = Convert.ToString(row["Title"]);
			return model;
		}
		/// <summary>
		/// 增加
		/// </summary>
		public int Insert(MAtlasWeb model, string ConnStr)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append(" INSERT Web_AtlasWeb (");
			sql.Append(" WID,ArticleID )");
			sql.Append(" SELECT");
			sql.Append(" @WID,@ArticleID");
			sql.Append("  SELECT @ID=@@IDENTITY");
			SqlCommand cmd = GetCommand(model);
			DbHelperSQL.RunSqlText(sql.ToString(), cmd, 3600, ConnStr);

			return Convert.ToInt32(cmd.Parameters[ID].Value);
		}
		/// <summary>
		/// 删除
		/// </summary>
		public int Delete(string strWhere, string ConnStr)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append(" DELETE Web_AtlasWeb ");
			sql.Append(" WHERE 1=1" + strWhere);
			if (strWhere == "")
			{
				//清空列表
				sql = new StringBuilder();
				sql.Append(" TRUNCATE TABLE Web_AtlasWeb ");
			}
			return DbHelperSQL.RunSqlText(sql.ToString(), 3600, ConnStr);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public int Update(MAtlasWeb model, string ConnStr)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append(" INSERT Web_AtlasWeb");
			sql.Append(" SET");
			sql.Append("	WID = @WID, ");
			sql.Append("	ArticleID = @ArticleID ");
			sql.Append("WHERE ID = @ID  ");
			SqlCommand cmd = GetCommand(model);
			DbHelperSQL.RunSqlText(sql.ToString(), cmd, 3600, ConnStr);

			return Convert.ToInt32(cmd.Parameters[ID].Value);
		}
		/// <summary>
		/// 获取数据
		/// </summary>
		public DataSet GetList(int pageSize, int pageIndex, string strWhere, string ConnStr)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("SELECT");
			sql.Append(" a.ID, ");
			sql.Append(" a.WID, ");
			sql.Append(" a.ArticleID, ");
			sql.Append(" b.WName, ");
			sql.Append(" c.Title ");
			sql.Append(" FROM Web_AtlasWeb a ");
			sql.Append(" LEFT JOIN Web_Website b ON b.WID = a.WID ");
			sql.Append(" LEFT JOIN wz_Content c ON c.ArticleID = a.ArticleID ");
			sql.Append("    WHERE 1=1 " + strWhere);
			return DbHelperSQL.RunSqlText(sql.ToString(), pageIndex, pageSize, 3600, ConnStr);
		}
		/// <summary>
		/// 获取实体信息
		/// </summary>
		public MAtlasWeb GetModel(string strWhere, string ConnStr)
		{
			using (DataTable dTable = GetList(0, 0, strWhere, ConnStr).Tables[0])
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
		public List<MAtlasWeb> GetModelList(int pageSize, int pageIndex, string strWhere, string ConnStr)
		{
			List<MAtlasWeb> modelList = new List<MAtlasWeb>();
			using (DataTable dTable = GetList(pageSize, pageIndex, strWhere, ConnStr).Tables[0])
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
