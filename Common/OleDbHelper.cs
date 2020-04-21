using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Common
{
    public class OleDbHelper
    {
        private static string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0";
        /// <summary>
        /// 读取数据到DataSet中
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql, string path)
        {
            OleDbConnection conn = new OleDbConnection(string.Format(connString, path));
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            //创建数据适配器对象
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            //创建一个内存数据集
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                //使用数据适配器填充数据集
                da.Fill(ds);
                //返回数据集
                return ds;  
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
