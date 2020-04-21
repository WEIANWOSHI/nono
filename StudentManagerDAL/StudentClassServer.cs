using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentsManagerModer;
using System.Data.SqlClient;

namespace StudentManagerDAL
{
    public class StudentClassServer
    {
        /// <summary>
        /// 查询所有班级
        /// </summary>
        /// <returns></returns>
        public List<StudentClass> GetClasses()
        {
            string sql = "SELECT * FROM StudentClass";
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            List<StudentClass> list = new List<StudentClass>();
            while (reader.Read())
            {
                list.Add(new StudentClass()
                {
                    Classid = Convert.ToInt32(reader["Classid"]),
                    ClassName = reader["ClassName"].ToString()
                });
            }
            reader.Close();
            return list;
        }
        /// <summary>
        /// 查询班级所对应的id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetClassIdByName(string name)
        {
            string sql = "SELECT Classid from StudentClass Where ClassName='" + name + "'";
            object res = DBHelper.SQLHelper.ExecuteScalar(sql);
            if (res == null)
            {
                return -1;
            }
            else
            {

                return Convert.ToInt32(res);
            }
            //string procName = "chaxunbanji";
            //SqlParameter[] parameters =
            //  {
            //       new SqlParameter("@target", System.Data.SqlDbType.VarChar,50),

            //   };
            //parameters[0].Value = name;
            //object res = DBHelper.SQLHelper.ExecuteScalarByPROC(procName, parameters);

            //if (res == null)
            //{
            //    return -1;
            //}
            //else
            //{

            //    return Convert.ToInt32(res);
            //}
        }
    }
}
