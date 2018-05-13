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
	public class SItemWeb:IItemWeb
	{
		private const string ID = "@ID";
		private const string WID="@WID";
		private const string IID = "@IID";

		private const string StrWhere = "@StrWhere";

		private SqlCommand GetCommand(MItemWeb model)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.Parameters.Add(new SqlParameter(ID, SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter(WID, SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter(IID, SqlDbType.BigInt));

			cmd.Parameters[ID].Value = model.ID;
			cmd.Parameters[WID].Value = model.WID;
			cmd.Parameters[IID].Value = model.IID;
			return cmd;
		}
		/// <summary>
		/// 实体信息
		/// </summary>
		private MItemWeb GetModel(DataRow row)
		{
			MItemWeb model = new MItemWeb();
			model.ID = Convert.ToInt32(row["ID"]);
			model.WID = Convert.ToInt32(row["WID"]);
			model.IID = Convert.ToInt64(row["IID"]);

			model.WName = Convert.ToString(row["WName"]);
			return model;
		}
        #region 注释
        /// <summary>
        /// 增加
        /// </summary>
        public int Insert(MItemWeb model, string ConnStr)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append(" INSERT Web_ItemWeb (");
			sql.Append(" WID,IID )");
			sql.Append(" SELECT");
			sql.Append(" @WID,@IID");
			sql.Append("  SELECT @ID=@@IDENTITY");
			SqlCommand cmd = GetCommand(model);
			DbHelperSQL.RunSqlText(sql.ToString(), cmd, 3600, ConnStr);

			return Convert.ToInt32(cmd.Parameters[ID].Value);
		}
		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int Update(MItemWeb model, string strWhere, string ConnStr)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append(" INSERT Web_ItemWeb");
			sql.Append(" SET");
			sql.Append("	WID = @WID, ");
			sql.Append("	IID = @IID ");
			sql.Append("WHERE ID = @ID  ");
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
			sql.Append(" DELETE Web_ItemWeb ");
			sql.Append(" WHERE 1=1"+strWhere);
			if (strWhere == "")
			{
				//清空列表
				sql = new StringBuilder();
				sql.Append(" TRUNCATE TABLE Web_ItemWeb ");
			}
			return DbHelperSQL.RunSqlText(sql.ToString(), 3600, ConnStr);
		}
        #endregion
        /// <summary>
        /// 获取数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string ConnStr)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("SELECT");
			sql.Append(" a.ID, ");
			sql.Append(" a.WID, ");
			sql.Append(" a.IID, ");
			sql.Append(" b.WName ");
			sql.Append(" FROM Web_ItemWeb a ");
			sql.Append(" LEFT JOIN Web_Website b ON b.WID = a.WID ");
            sql.Append("  LEFT JOIN rpt_ItemsInfo c ON c.ID = a.IID ");
			sql.Append("    WHERE 1=1 " + strWhere);
			return DbHelperSQL.RunSqlText(sql.ToString(), pageIndex, pageSize, 3600, ConnStr);
		}

		public MItemWeb GetModel(string strWhere, string ConnStr)
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
		public List<MItemWeb> GetModelList(int pageSize, int pageIndex, string strWhere, string ConnStr)
		{
			List<MItemWeb> modelList = new List<MItemWeb>();
			using (DataTable dTable = GetList(pageSize, pageIndex, strWhere, ConnStr).Tables[0])
			{
				foreach (DataRow row in dTable.Rows)
				{
					modelList.Add(GetModel(row));
				}
			}
			return modelList;
		}

		#region 个性函数
		public List<MItemWeb> GetModelListByTop(int pageSize, int pageIndex, string strWhere, int top, int top2, string ConnStr)
		{
			List<MItemWeb> modelList = new List<MItemWeb>();
			StringBuilder sql = new StringBuilder();
			sql.Append("SELECT");
			sql.Append(" TOP " + top);
			sql.Append(" a.ID, ");
			sql.Append(" a.WID, ");
			sql.Append(" a.IID, ");
			sql.Append(" b.WName ");
			sql.Append(" FROM Web_ItemWeb a ");
			sql.Append(" LEFT JOIN Web_Website b ON b.WID = a.WID ");
			sql.Append("    WHERE 1=1 " + strWhere);
			sql.Append(" AND a.ID NOT IN (SELECT TOP " + top2 + " ID FROM Web_ItemWeb WHERE 1=1 " + strWhere.Replace("a.", "") + " ORDER BY a.ID DESC )");
			using (DataTable dTable = DbHelperSQL.RunSqlText(sql.ToString(), pageIndex, pageSize, 3600, ConnStr).Tables[0])
			{
				foreach (DataRow row in dTable.Rows)
				{
					modelList.Add(GetModel(row));
				}
			}
			return modelList;
		}
		/// <summary>
		/// 获取指定条件的数据数量
		/// </summary>
		public int GetModelCount(int pageSize, int pageIndex, string strWhere, string ConnStr)
		{
			int rankCount = 0;
			StringBuilder sql = new StringBuilder();
			sql.Append(" SELECT COUNT(a.ID) AS RankCount ");
			sql.Append(" FROM Web_ItemWeb a");
			sql.Append(" WHERE 1 = 1 " + strWhere);
			using (SqlDataReader reader = DbHelperSQL.ExecuteReader(sql.ToString(), ConnStr))
			{
				while (reader.Read())
				{
					rankCount = Convert.ToInt32(reader["RankCount"]);
					break;
				}
			}
			return rankCount;
		}
		#endregion

	}
}
