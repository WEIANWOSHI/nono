using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StudentManagerDAL.DBHelper
{
    class SQLHelper
    {
        static string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        /// <summary>
        /// 执行增，删，该
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 执行单一结果查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 查询结果返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql, con);
            DataSet set = new DataSet();
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(set);
                return set;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 查询结果用DataReader读取
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                con.Close();
                throw ex;
            }
        }
        private static SqlConnection sqlcon = null;
        private static void Initconnection()
        {
            if (sqlcon == null)
            {
                sqlcon = new SqlConnection(constr);
            }
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            if (sqlcon.State == ConnectionState.Broken)
            {
                sqlcon.Close();
                sqlcon.Open();
            }
        }
        public static DataTable GetDataTable(string sqlstr)
        {
            Initconnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlstr, sqlcon);
            DataTable Table = new DataTable();
            adapter.Fill(Table);
            return Table;
        }
        /// <summary>
        /// 查询结果用DataReader读取
        /// </summary>
        /// <param name="procName">存储过程的名称</param>
        /// <param name="parameters">SQL语句中的所有参数</param>
        /// <returns></returns>
        public static SqlDataReader GetReaderByPROC(string procName, SqlParameter[] parameters)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            //调用存储过程
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            if (parameters != null)
            {
                //将SQL语句中的所有参数对象接收
                cmd.Parameters.AddRange(parameters);
            }
            try
            {
                con.Open();
                //不需要手动关闭con，当DataReader关闭时，con自动跟着关闭
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                con.Close();
                //记入系统日志
                throw ex;
            }
        }
        /// <summary>
        /// 查询结果用ExecuteNonQuery读取
        /// </summary>
        /// <param name="procName">存储过程的名称</param>
        /// <param name="parameters">SQL语句中的所有参数</param>
        /// <returns></returns>
        public static int ExecuteNonQueryByPROC(string procName, SqlParameter[] parameters)
        {
            SqlConnection Con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con;
            //调用存储过程
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);//将SQL语句中的所有参数对象接收
            }
            try
            {
                Con.Open();
                return cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //记录日志
                throw ex;
            }
            finally
            {
                Con.Close();
            }
        }
        /// <summary>
        /// 查询结果用DataSet读取
        /// </summary>
        /// <param name="procName">存储过程的名称</param>
        /// <param name="parameters">SQL语句中的所有参数</param>
        /// <returns></returns>
        public static DataSet GetDataSetByPROC(string procName, SqlParameter[] parameters)
        {
            SqlConnection Con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con;
            DataSet set = new DataSet();
            //调用存储过程
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);//将SQL语句中的所有参数对象接收
            }
            try
            {
                Con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(set);
                return set;
            }
            catch (Exception ex)
            {
                //记录日志
                throw ex;
            }
            finally
            {
                Con.Close();
            }
        }
        /// <summary>
        /// 查询结果用ExecuteScalar读取
        /// </summary>
        /// <param name="procName">存储过程的名称</param>
        /// <param name="parameters">SQL语句中的所有参数</param>
        /// <returns></returns>
        public static object ExecuteScalarByPROC(string procName, SqlParameter[] parameters)
        {
            SqlConnection Con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Con;
            //调用存储过程
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);//将SQL语句中的所有参数对象接收
            }
            try
            {
                Con.Open();
                return cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                //记录日志
                throw ex;
            }
            finally
            {
                Con.Close();
            }
        }


    }
}
