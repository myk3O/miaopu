using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Model
{
    /// <summary>
    /// 最底层的操作数据库代码
    /// </summary>
    public class SqlHelperModel
    {

        public SqlConnection cn;
        public SqlHelperModel()
        {
            cn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["strCon"]);
        }
        #region ExecuteNonQuery 增删改方法

        /// <summary>
        /// 执行SQL语句的增删改方法
        /// </summary>
        /// <param name="SQLObject">SQL语句</param>
        /// <param name="paramerts">参数数组</param>
        /// <returns>受影响行数</returns>
        public bool ExecuteNonQuery(string SQLObject, params  SqlParameter[] paramerts)
        {
            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
            SqlCommand cmd = InitCommand(SQLObject, paramerts);
            int rows;
            try
            {
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                cmd.Parameters.Clear();
                cn.Close();
                cmd.Dispose();
            }
            return rows > 0;
        }

        /// <summary>
        /// 执行存储过程的增删改方法
        /// </summary>
        /// <param name="SQLObject">存储过程名称</param>
        /// <param name="paramerts">参数数组</param>
        /// <returns>存储过程的返回值</returns>
        public int ExecuteNonQueryProc(string SQLObject, params  SqlParameter[] paramerts)
        {
            cn.Open();
            SqlCommand cmd = InitCommand(SQLObject, paramerts);
            cmd.CommandType = CommandType.StoredProcedure;
            int rows;
            try
            {
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }
            return rows;
        }

        /// <summary>
        /// 使用事务执行SQL语句的增删改方法
        /// </summary>
        /// <param name="SQLObject">SQL语句</param>
        /// <param name="paramerts">参数数组</param>
        /// <returns>事务是否执行成功</returns>
        public bool ExecuteNonQueryTrans(string[] SQLObject, params  SqlParameter[][] paramerts)
        {
            bool success;
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            SqlTransaction ta = cn.BeginTransaction();
            cmd.Transaction = ta;
            try
            {
                for (int i = 0; i < SQLObject.Length; i++)
                {
                    cmd = InitCommand(SQLObject[i], paramerts[i]);
                    cmd.ExecuteNonQuery();
                }
                ta.Commit();
            }
            catch
            {
                ta.Rollback();
                success = false;
            }
            finally
            {
                cn.Close();
                success = true;
            }
            return success;
        }

        #endregion

        #region ExecuteScalar 获取标量值

        /// <summary>
        /// 执行SQL语句获取标量值方法
        /// </summary>
        /// <param name="SQLObject">SQL语句</param>
        /// <param name="paramerts">参数数组</param>
        /// <returns>标量值</returns>
        public object ExecuteScalar(string SQLObject, params SqlParameter[] paramerts)
        {
            SqlCommand cmd = InitCommand(SQLObject, paramerts);
            cn.Open();
            object o;
            try
            {
                o = cmd.ExecuteScalar();
            }
            catch
            {
                return null;
            }
            finally
            {
                cn.Close();
            }
            return o;
        }

        /// <summary>
        /// 执行存储过程获取标量值方法
        /// </summary>
        /// <param name="SQLObject">存储过名称程</param>
        /// <param name="paramerts">参数数组</param>
        /// <returns>标量值</returns>
        public object ExecuteScalarProc(string SQLObject, params SqlParameter[] paramerts)
        {
            SqlCommand cmd = InitCommand(SQLObject, paramerts);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            object o;
            try
            {
                o = cmd.ExecuteScalar();
            }
            catch
            {
                return null;
            }
            finally
            {
                cn.Close();
            }
            return o;
        }

        #endregion

        #region ExecuteDataSet 获取数据集

        /// <summary>
        /// 执行SQL语句获取数据集的方法
        /// </summary>
        /// <param name="SQLObject"></param>
        /// <param name="paramerts"></param>
        /// <returns>返回DataTable</returns>
        public DataTable ExecuteDataTable(string SQLObject, params SqlParameter[] paramerts)
        {
            DataTable ds = new DataTable();
            SqlCommand cmd = InitCommand(SQLObject, paramerts);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 执行SQL语句获取数据集的方法
        /// </summary>
        /// <param name="SQLObject"></param>
        /// <param name="paramerts"></param>		
        /// <returns>返回DataSet</returns>
        public DataSet ExecuteDataSet(string SQLObject, params SqlParameter[] paramerts)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = InitCommand(SQLObject, paramerts);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 执行存储过程获取数据集的方法
        /// </summary>
        /// <param name="SQLObject">存储过程名称</param>		
        /// <returns></returns>
        public DataSet ExecuteDataSetProc(string SQLObject, params SqlParameter[] paramerts)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = InitCommand(SQLObject, paramerts);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 执行视图获取数据集的方法
        /// </summary>
        /// <param name="SQLObject">视图或者表的名称</param>		
        /// <returns></returns>
        public DataSet ExecuteDataSetView(string SQLObject, SqlParameter[] paramerts)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = InitCommand(SQLObject, paramerts);
            cmd.CommandType = CommandType.TableDirect;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        #endregion

        #region ExecuteDataReader 获取流水游标

        /// <summary>
        /// 执行SQL语句获取流水游标的方法
        /// </summary>
        /// <param name="SQLObject">SQL语句</param>
        /// <param name="paramerts">参数数组</param>
        /// <returns>指向查询结果的流水游标</returns>		
        public SqlDataReader ExecuteDataReader(string SQLObject, params SqlParameter[] paramerts)
        {
            try
            {
                SqlCommand cmd = InitCommand(SQLObject, paramerts);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch
            {
                cn.Close();
                return null;
            }
        }
        /// <summary>
        ///执行存储过程获取流水游标的方法
        /// </summary>
        /// <param name="SQLObject">存储过程名称</param>
        /// <param name="paramerts">参数数组</param>
        /// <returns>指向查询结果的流水游标</returns>		
        public SqlDataReader ExecuteDataReaderProc(string SQLObject, params SqlParameter[] paramerts)
        {
            try
            {
                SqlCommand cmd = InitCommand(SQLObject, paramerts);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch
            {
                cn.Close();
                return null;
            }
        }
        #endregion

        #region InitCommand 设置命令集参数
        /// <summary>
        /// //扩展QuerryCMD
        /// </summary>
        /// <param name="SQLObject"></param>
        /// <param name="paramerts"></param>
        /// <returns></returns>
        private SqlCommand InitCommand(string SQLObject, params SqlParameter[] paramerts)
        {
            SqlCommand cmd = new SqlCommand(SQLObject, cn);
            foreach (SqlParameter pt in paramerts)//往Command里添加参数
            {
                cmd.Parameters.Add(pt);
            }
            return cmd;
        }
        private SqlCommand InitCommandProc(string SQLObject, SqlParameter[] paramerts)
        {
            SqlCommand cmd = new SqlCommand(SQLObject, cn);
            foreach (SqlParameter pt in paramerts)//往Command里添加参数
            {
                cmd.Parameters.Add(pt);
            }
            SqlParameter pa = new SqlParameter("ReturnValue", SqlDbType.Int, 6, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null);//添加参数映射返回值
            cmd.Parameters.Add(pa);
            return cmd;
        }

        #endregion

        #region SqlParameter 含NULL类型实例
        /// <summary>
        /// 设置含NULL类型Int参数
        /// </summary>
        /// <param name="PName">Sql参数名</param>
        /// <param name="str">参数值</param>
        /// <returns></returns>
        public static SqlParameter setIntSqlParameter(string PName, string str)
        {
            if (str.Trim() == "")
            {
                return new SqlParameter(PName, DBNull.Value);
            }
            else
            {
                return new SqlParameter(PName, int.Parse(str));
            }
        }

        /// <summary>
        /// 设置含NULL类型String参数
        /// </summary>
        /// <param name="PName">Sql参数名</param>
        /// <param name="str">参数值</param>
        /// <returns></returns>
        public static SqlParameter setStrSqlParameter(string PName, string str)
        {
            if (str.Trim() == "")
            {
                return new SqlParameter(PName, DBNull.Value);
            }
            else
            {
                return new SqlParameter(PName, str.Trim());
            }
        }

        /// <summary>
        /// 设置含NULL类型Float参数
        /// </summary>
        /// <param name="PName">Sql参数名</param>
        /// <param name="str">参数值</param>
        /// <returns></returns>
        public static SqlParameter setFloatSqlParameter(string PName, string str)
        {
            if (str.Trim() == "")
            {
                return new SqlParameter(PName, DBNull.Value);
            }
            else
            {
                return new SqlParameter(PName, float.Parse(str));
            }
        }

        /// <summary>
        /// 设置含NULL类型Float参数
        /// </summary>
        /// <param name="PName">Sql参数名</param>
        /// <param name="str">参数值</param>
        /// <returns></returns>
        public static SqlParameter setDoubleSqlParameter(string PName, string str)
        {
            if (str.Trim() == "")
            {
                return new SqlParameter(PName, DBNull.Value);
            }
            else
            {
                return new SqlParameter(PName, double.Parse(str));
            }
        }

        /// <summary>
        /// 设置含NULL类型Float参数
        /// </summary>
        /// <param name="PName">Sql参数名</param>
        /// <param name="str">参数值</param>
        /// <returns></returns>
        public static SqlParameter setDecimalSqlParameter(string PName, string str)
        {
            if (str.Trim() == "")
            {
                return new SqlParameter(PName, DBNull.Value);
            }
            else
            {
                return new SqlParameter(PName, decimal.Parse(str));
            }
        }

        /// <summary>
        /// 设置含NULL类型DateTime参数
        /// </summary>
        /// <param name="PName">Sql参数名</param>
        /// <param name="str">参数值</param>
        /// <returns></returns>
        public static SqlParameter setDateTimeSqlParameter(string PName, string str)
        {
            if (str.Trim() == "")
            {
                return new SqlParameter(PName, DBNull.Value);
            }
            else
            {
                return new SqlParameter(PName, str);
            }
        }
        #endregion

        /// DES加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public string DesEncrypt(string encryptString)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes("terminat");
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }
        /// DES解密
        /// </summary>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public string DesDecrypt(string decryptString)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes("terminat");
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
    }
}
