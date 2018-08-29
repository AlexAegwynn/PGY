using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    public class SqlHelper
    {
        //private static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebSite; uid=sa;pwd=22446688";
        //private static string connectionString = @"Data Source=STONE20\SQL2005;Initial Catalog=WebSite; uid=sa;pwd=pgy3210";
        //private static string connectionString = @"Data Source=ECS-2D04\SQL2005;Initial Catalog=WebSite;User ID=sa;Password=ASDQWE!@#qwe";        
        //private static string connectionString = @"Data Source=192.168.3.11\SQL2005;Initial Catalog=PGY_CM;User ID=sa;Password=pgy123456";
        private static string connectionString = @"Data Source=121.10.200.52,1311\\SQL2005;Initial Catalog=PGY_CM;User ID=sa;Password=pgy123456"; //外网访问内网数据库链接
        #region ExecuteNonQuery

        #region 公共方法

        /// <summary>
        /// 执行返回受影响行数的SqlCommand命令
        /// </summary>
        /// <param name="commandType">SqlCommand命令类型(存储过程，T-SQL语句等。)</param>
        /// <param name="commandText">存储过程名 或 T-SQL 语句</param>
        /// <param name="sqlParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] sqlParameters)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString", "错误：未设置数据库连接字符串！");
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();//打开连接

                return ExecuteNonQuery(sqlConnection, commandType, commandText, sqlParameters);
            }
        }

        /// <summary>
        /// 执行一个不需要返回值的SqlCommand命令
        /// </summary>
        /// <param name="commandType">SqlCommand命令类型(存储过程，T-SQL语句等。)</param>
        /// <param name="commandText">存储过程名 或 T-SQL 语句</param>
        /// <param name="isTrans">是否使用事务</param>
        /// <param name="sqlParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteNonQueryVoid(string commandText, CommandType commandType, bool isTrans, params SqlParameter[] sqlParameters)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString", "错误：未设置数据库连接字符串！");
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();//打开连接

                if (isTrans)
                {
                    return ExecuteNonQueryWithTrans(sqlConnection, commandType, commandText, sqlParameters);
                }
                else
                {
                    return ExecuteNonQuery(sqlConnection, commandType, commandText, sqlParameters);
                }
            }
        }

        /// <summary>
        /// 执行触发器
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static int ExecuteNonQueryTrigger(string commandText)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();//打开连接
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = commandText;
                return sqlCommand.ExecuteNonQuery();
            }
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 执行返回受影响行数的SqlCommand命令
        /// </summary>
        /// <param name="sqlConnection">数据库连接对象</param>
        /// <param name="commandType">SqlCommand命令类型(存储过程，T-SQL语句等。)</param>
        /// <param name="commandText">存储过程名 或 T-SQL 语句</param>
        /// <param name="sqlParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>受影响行数</returns>
        private static int ExecuteNonQuery(SqlConnection sqlConnection, CommandType commandType, string commandText, params SqlParameter[] sqlParameters)
        {
            if (sqlConnection == null)
            {
                throw new ArgumentNullException("sqlConnection", "错误：未创建数据库连接对象:SqlConnection");
            }

            SqlCommand sqlCommand = new SqlCommand();
            PrepareCommand(sqlConnection, sqlCommand, commandType, commandText, null, sqlParameters); //通过PrepareCommand方法将参数逐个加入到SqlCommand的参数集合中

            try
            {
                int result = sqlCommand.ExecuteNonQuery();   //命令执行结果（受影响行数）                      
                sqlCommand.Parameters.Clear();   //清空SqlCommand中的参数列表
                return result;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行返回受影响行数的SqlCommand命令（使用事务）
        /// </summary>
        /// <param name="sqlConnection">数据库连接对象</param>
        /// <param name="commandType">SqlCommand命令类型(存储过程，T-SQL语句等。)</param>
        /// <param name="commandText">储过程名 或 T-SQL 语句</param>
        /// <param name="sqlParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>受影响行数</returns>
        private static int ExecuteNonQueryWithTrans(SqlConnection sqlConnection, CommandType commandType, string commandText, params SqlParameter[] sqlParameters)
        {
            if (sqlConnection == null)
            {
                throw new ArgumentNullException("sqlConnection", "错误：未创建数据库连接对象:SqlConnection");
            }

            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();   //开始事务
            SqlCommand sqlCommand = new SqlCommand();
            PrepareCommand(sqlConnection, sqlCommand, commandType, commandText, sqlTransaction, sqlParameters); //通过PrepareCommand方法将参数逐个加入到SqlCommand的参数集合中

            try
            {
                int result = sqlCommand.ExecuteNonQuery();  //命令执行结果（受影响行数）                 
                sqlCommand.Parameters.Clear();   //清空SqlCommand中的参数列表
                sqlTransaction.Commit();   //提交事务
                return result;
            }
            catch (SqlException ex)
            {
                sqlTransaction.Rollback();   //事务回滚
                throw ex;
            }
        }
        #endregion

        #endregion

        #region ExecuteDataTable

        #region 公共方法
        /// <summary>
        /// 执行一个返回值类型为DataTable的SqlCommand命令
        /// </summary>
        /// <param name="commandText">存储过程名 或 T-SQL 语句</param>
        /// <param name="commandType">SqlCommand命令类型(存储过程，T-SQL语句等。)</param>
        /// <param name="sqlParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>结果集</returns>
        public static DataTable ExecuteDataTable(CommandType commandType, string commandText, params SqlParameter[] sqlParameters)
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ArgumentNullException("connectionString", "错误：未设置数据库连接字符串！");
                }

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();//打开连接

                    return ExecuteDataTable(sqlConnection, commandType, commandText, sqlParameters);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 执行一个返回值类型为DataTable的SqlCommand命令
        /// </summary>
        /// <param name="sqlConnection">数据库连接对象</param>
        /// <param name="commandText">存储过程名 或 T-SQL 语句</param>
        /// <param name="commandType">SqlCommand命令类型(存储过程，T-SQL语句等。)</param>
        /// <param name="sqlParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>结果集</returns>
        private static DataTable ExecuteDataTable(SqlConnection sqlConnection, CommandType commandType, string commandText, params SqlParameter[] sqlParameters)
        {
            if (sqlConnection == null)
            {
                throw new ArgumentNullException("sqlConnection", "错误：未创建数据库连接对象:SqlConnection");
            }

            SqlCommand sqlCommand = new SqlCommand();
            PrepareCommand(sqlConnection, sqlCommand, commandType, commandText, null, sqlParameters);//通过PrepareCommand方法将参数逐个加入到SqlCommand的参数集合中

            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    sqlCommand.Parameters.Clear();   //清空SqlCommand中的参数列表
                    return dt;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region ExecuteDataSet

        #region 公共方法

        /// <summary>
        /// 执行一个返回值类型为DataSet的SqlCommand命令
        /// </summary>
        /// <param name="commandText">存储过程名 或 T-SQL 语句</param>
        /// <param name="commandType">SqlCommand命令类型(存储过程，T-SQL语句等。)</param>
        /// <param name="sqlParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>结果集</returns>
        public static DataSet ExecuteDataSet(CommandType commandType, string commandText, params SqlParameter[] sqlParameters)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString", "错误：未设置数据库连接字符串！");
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();//打开连接

                return ExecuteDataSet(sqlConnection, commandType, commandText, sqlParameters);
            }
        }

        /// <summary>
        /// 执行一个返回值类型为DataSet的SqlCommand命令
        /// </summary>
        /// <param name="commandText">存储过程名 或 T-SQL 语句</param>
        /// <param name="tableName">表名</param>
        /// <param name="commandType">SqlCommand命令类型(存储过程，T-SQL语句等。)</param>
        /// <param name="sqlParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>结果集</returns>
        public static DataSet ExecuteDataSet(CommandType commandType, string commandText, string tableName, params SqlParameter[] sqlParameters)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString", "错误：未设置数据库连接字符串！");
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();//打开连接

                return ExecuteDataSet(sqlConnection, commandType, commandText, tableName, sqlParameters);
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 执行一个返回值类型为DataSet的SqlCommand命令
        /// </summary>
        /// <param name="sqlConnection">数据库连接对象</param>
        /// <param name="commandText">存储过程名 或 T-SQL 语句</param>
        /// <param name="commandType">SqlCommand命令类型(存储过程，T-SQL语句等。)</param>
        /// <param name="sqlParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>结果集</returns>
        private static DataSet ExecuteDataSet(SqlConnection sqlConnection, CommandType commandType, string commandText, params SqlParameter[] sqlParameters)
        {
            if (sqlConnection == null)
            {
                throw new ArgumentNullException("sqlConnection", "错误：未创建数据库连接对象:SqlConnection");
            }

            SqlCommand sqlCommand = new SqlCommand();  //创建命令
            PrepareCommand(sqlConnection, sqlCommand, commandType, commandText, null, sqlParameters);  //通过PrepareCommand方法将参数逐个加入到SqlCommand的参数集合中

            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    sqlCommand.Parameters.Clear();   //清空SqlCommand中的参数列表
                    return ds;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行一个返回值类型为DataSet的SqlCommand命令
        /// </summary>
        /// <param name="sqlConnection">数据库连接对象</param>
        /// <param name="commandText">存储过程名 或 T-SQL 语句</param>
        /// <param name="tableName">表名</param>
        /// <param name="commandType">SqlCommand命令类型(存储过程，T-SQL语句等。)</param>
        /// <param name="sqlParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>结果集</returns>
        private static DataSet ExecuteDataSet(SqlConnection sqlConnection, CommandType commandType, string commandText, string tableName, params SqlParameter[] sqlParameters)
        {
            if (sqlConnection == null)
            {
                throw new ArgumentNullException("sqlConnection", "错误：未创建数据库连接对象:SqlConnection");
            }

            SqlCommand sqlCommand = new SqlCommand();
            PrepareCommand(sqlConnection, sqlCommand, commandType, commandText, null, sqlParameters); //通过PrepareCommand方法将参数逐个加入到SqlCommand的参数集合中

            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, tableName);
                    sqlCommand.Parameters.Clear();   //清空SqlCommand中的参数列表
                    return ds;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region ExecuteReader

        #region 公共方法

        /// <summary>
        /// 执行一个返回值类型为SqlDataReader的SqlCommand命令
        /// </summary>
        /// <param name="commandText">储过程名 或 T-SQL 语句</param>
        /// <param name="commandType">qlCommand命令类型(存储过程，T-SQL语句等。)</param>
        /// <param name="sqlParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] sqlParameters)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString", "错误：未设置数据库连接字符串！");
            }

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            return ExecuteReader(sqlConnection, commandType, commandText, sqlParameters);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 执行一个返回值类型为SqlDataReader的SqlCommand命令
        /// </summary>
        /// <param name="sqlConnection">数据库连接对象</param>
        /// <param name="commandText">储过程名 或 T-SQL 语句</param>
        /// <param name="commandType">qlCommand命令类型(存储过程，T-SQL语句等。)</param>
        /// <param name="sqlParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回SqlDataReader</returns>
        private static SqlDataReader ExecuteReader(SqlConnection sqlConnection, CommandType commandType, string commandText, params SqlParameter[] sqlParameters)
        {
            if (sqlConnection == null)
            {
                throw new ArgumentNullException("sqlConnection", "错误：未创建数据库连接对象:SqlConnection");
            }

            SqlCommand sqlCommand = new SqlCommand();
            PrepareCommand(sqlConnection, sqlCommand, commandType, commandText, null, sqlParameters);//通过PrepareCommand方法将参数逐个加入到SqlCommand的参数集合中

            try
            {
                SqlDataReader reader = sqlCommand.ExecuteReader();
                sqlCommand.Parameters.Clear();   //清空SqlCommand中的参数列表
                return reader;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        #region ExecuteScalar

        #region 公共方法

        /// <summary>
        /// 执行一个返回值类型为Object的SqlCommand命令
        /// </summary>
        /// <param name="commandText">储过程名 或 T-SQL 语句</param>
        /// <param name="commandType">SqlCommand命令类型(存储过程，T-SQL语句等。)</param>
        /// <param name="sqlParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回object</returns>
        public static object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] sqlParameters)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString", "错误：未设置数据库连接字符串！");
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();//打开连接

                return ExecuteScalar(sqlConnection, commandType, commandText, sqlParameters);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 执行一个返回值类型为Object的SqlCommand命令
        /// </summary>
        /// <param name="sqlConnection">数据库连接对象</param>
        /// <param name="commandText">储过程名 或 T-SQL 语句</param>
        /// <param name="commandType">SqlCommand命令类型(存储过程，T-SQL语句等。)</param>
        /// <param name="sqlParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回object</returns>
        private static object ExecuteScalar(SqlConnection sqlConnection, CommandType commandType, string commandText, params SqlParameter[] sqlParameters)
        {
            if (sqlConnection == null)
            {
                throw new ArgumentNullException("sqlConnection", "错误：未创建数据库连接对象:SqlConnection");
            }

            SqlCommand sqlCommand = new SqlCommand();
            PrepareCommand(sqlConnection, sqlCommand, commandType, commandText, null, sqlParameters); //通过PrepareCommand方法将参数逐个加入到SqlCommand的参数集合中

            try
            {
                object obj = sqlCommand.ExecuteScalar();  //执行命令
                sqlCommand.Parameters.Clear();   //清空SqlCommand中的参数列表
                return obj;  //返回对象
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        #region PrepareCommand
        /// <summary>
        /// SqlCommand预处理
        /// </summary>
        /// <param name="sqlCommand">SqlCommand 命令</param>
        /// <param name="sqlConnection">数据库连接</param>
        /// <param name="sqlTransaction">数据库事务处理</param>
        /// <param name="commandType">SqlCommand命令类型(存储过程，T-SQL语句等)</param>
        /// <param name="commandText">存储过程名 或 T-SQL语句等</param>
        /// <param name="commandParameters">命令的参数</param>
        private static void PrepareCommand(SqlConnection sqlConnection, SqlCommand sqlCommand, CommandType commandType, string commandText, SqlTransaction sqlTransaction = null, params SqlParameter[] commandParameters)
        {
            try
            {
                if (sqlCommand == null)
                {
                    throw new ArgumentNullException("sqlCommand", "错误：未创建命令对象:SqlCommand）");
                }

                if (string.IsNullOrEmpty(commandText))
                {
                    throw new ArgumentNullException("commandText", "错误：此命令文本为空！");
                }

                //检查数据库连接状态
                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandTimeout = 30;   // 不设此值为默认值30秒,设置为零是无限长的超时。
                sqlCommand.CommandText = commandText;   //设置命令文本（T-SQL语句 或 存储过程名）

                //判断是否需要事务处理
                if (sqlTransaction != null)
                {
                    if (sqlTransaction.Connection == null)
                    {
                        throw new ArgumentException("错误：事务已提交或回滚，请提供一个开放的事务", "sqlTransaction");
                    }
                    sqlCommand.Transaction = sqlTransaction;
                }

                sqlCommand.CommandType = commandType;   //设置命令类型

                //判断是否有参数，如果有提供参数，则附加参数
                if (commandParameters != null)
                {
                    sqlCommand.Parameters.AddRange(commandParameters);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SqlBulkCopy
        #region 公共方法
        /// <summary>
        /// 批量插入数据库
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="tableName">目标数据库表名</param>
        /// <param name="batchSize">每个批处理中的行数(默认每批10000条数据)</param>
        /// <param name="bulkCopyTimeout">超时之前可用于完成操作的秒数(默认每批操作超时600秒(10分钟))</param>
        public static void BulkToDB(DataTable dt, string tableName, int batchSize = 10000, int bulkCopyTimeout = 600)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString", "错误：未设置数据库连接字符串！");
            }

            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName", "错误：未指定要插入数据库的表名！");
            }

            //防止每批数据量被设置为负数或0
            if (batchSize <= 0)
            {
                batchSize = dt.Rows.Count;
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();//打开连接
                BulkToDB(sqlConnection, dt, tableName, batchSize, bulkCopyTimeout);
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="dt">数据源</param>
        /// <param name="tableName">目标数据库表名</param>
        /// <param name="batchSize">每个批处理中的行数</param>
        /// <param name="bulkCopyTimeout">超时之前可用于完成操作的秒数</param>
        private static void BulkToDB(SqlConnection sqlConnection, DataTable dt, string tableName, int batchSize, int bulkCopyTimeout)
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction))
            {
                sqlBulkCopy.DestinationTableName = tableName;
                sqlBulkCopy.BatchSize = batchSize;
                sqlBulkCopy.BulkCopyTimeout = bulkCopyTimeout;

                try
                {
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            sqlBulkCopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);  //映射数据源的列与目标表的列的关系
                        }

                        sqlBulkCopy.WriteToServer(dt);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion
        #endregion
    }
}
