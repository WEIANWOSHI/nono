using StudentsManagerModer.ObjExt;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentsManagerModer;
using System.Data;

namespace StudentManagerDAL
{
    /// <summary>
    /// 成绩管理的数据访问服务
    /// </summary>
    public class ScoreListServer
    {
        /// <summary>
        /// 根据班级查询
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<ScoreListExt> GetScoreList(int cid)
        {
            string sql = "SELECT ScoreList.Studentid, Students.StudentName ,CSharp,SQLServer,UpdateTime,StudentClass.ClassName FROM ScoreList INNER JOIN Students ON Students.Studentid=ScoreList.Studentid INNER JOIN StudentClass ON StudentClass.Classid=Students.Classid WHERE StudentClass.Classid=" + cid;
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            List<ScoreListExt> list = DataReturnObj(reader);
            reader.Close();
            return list;
            //string procName = "banjichaxun";
            //SqlParameter[] parameters =
            //  {
            //       new SqlParameter("@cid", System.Data.SqlDbType.Int),

            //   };
            //parameters[0].Value = cid;
            //SqlDataReader reader = DBHelper.SQLHelper.GetReaderByPROC(procName, parameters);
            //List<ScoreListExt> list = DataReturnObj(reader);
            //reader.Close();
            //return list;
        }
        private static List<ScoreListExt> DataReturnObj(SqlDataReader reader)
        {
            List<ScoreListExt> list = new List<ScoreListExt>();
            while (reader.Read())
            {
                list.Add(new ScoreListExt()
                {
                    Studentid = Convert.ToInt32(reader["Studentid"]),
                    StudentName = reader["StudentName"].ToString(),
                    CSharp = Convert.ToInt32(reader["CSharp"]),
                    ClassName = reader["ClassName"].ToString(),
                    SQLServer = Convert.ToInt32(reader["SQLServer"]),
                    UpdateTime = Convert.ToDateTime(reader["UpdateTime"])
                });
            }

            return list;
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        public List<ScoreListExt> GetStudentByldOrName(string target)
        {
            string sql = string.Format("SELECT ScoreList.Studentid,Students.StudentName ,CSharp,SQLServer,UpdateTime,StudentClass.ClassName FROM ScoreList INNER JOIN Students ON Students.Studentid=ScoreList.Studentid INNER JOIN StudentClass ON StudentClass.Classid=Students.Classid WHERE ScoreList.Studentid LIKE '%{0}%' OR StudentName LIKE '%{0}%'", target);
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            List<ScoreListExt> list = DataReturnObj(reader);
            reader.Close();
            return list;
            //string procName = "mohuchaxun";
            //SqlParameter[] parameters =
            //  {
            //       new SqlParameter("@cid", System.Data.SqlDbType.Int),

            //   };
            //parameters[0].Value = target;
            //SqlDataReader reader = DBHelper.SQLHelper.GetReaderByPROC(procName, parameters);
            //List<ScoreListExt> list = DataReturnObj(reader);
            //reader.Close();
            //return list;
        }
        /// <summary>
        /// 删除学员
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public int DeleteStudentById(int sid)
        {
            string sql = "DELETE FROM ScoreList WHERE Studentid=" + sid;
            return DBHelper.SQLHelper.ExecuteNonQuery(sql);
            //string procName = "shanchuchaxun";
            //SqlParameter[] parameters =
            //  {
            //       new SqlParameter("@cid", System.Data.SqlDbType.Int),

            //   };
            //parameters[0].Value = sid;
            //return DBHelper.SQLHelper.ExecuteNonQueryByPROC(procName, parameters);
        }
        public ScoreListExt GetStudentById(int id)
        {
            string sql = "SELECT ScoreList.Studentid, Students.StudentName ,CSharp,SQLServer,UpdateTime,StudentClass.ClassName FROM ScoreList INNER JOIN Students ON Students.Studentid=ScoreList.Studentid INNER JOIN StudentClass ON StudentClass.Classid=Students.Classid WHERE ScoreList.Studentid=" + id;
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            //string procName = "mohuchaxun";
            //SqlParameter[] parameters =
            //  {
            //       new SqlParameter("@cid", System.Data.SqlDbType.Int),

            //   };
            //parameters[0].Value = id;
            //SqlDataReader reader = DBHelper.SQLHelper.GetReaderByPROC(procName, parameters);
            ScoreListExt student = null;
            while (reader.Read())
            {
                student = (new ScoreListExt()
                {
                    Studentid = Convert.ToInt32(reader["Studentid"]),
                    StudentName = reader["StudentName"].ToString(),
                    CSharp = Convert.ToInt32(reader["CSharp"]),
                    ClassName = reader["ClassName"].ToString(),
                    SQLServer = Convert.ToInt32(reader["SQLServer"]),
                    UpdateTime = Convert.ToDateTime(reader["UpdateTime"])
                });
            }
            return student;

        }
        /// <summary>
        /// 修改学员信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int UpdateStudentInfor(ScoreListExt student)
        {
            string sql = string.Format("UPDATE ScoreList SET CSharp={0},SQLServer={1} WHERE Studentid={2}", student.CSharp, student.SQLServer, student.Studentid);
            return DBHelper.SQLHelper.ExecuteNonQuery(sql);
            //string procName = "xiugaiScore";
            //SqlParameter[] parameters =
            //  {
            //       new SqlParameter("@id", System.Data.SqlDbType.Int),
            //       new SqlParameter("@csharp", System.Data.SqlDbType.Int),
            //       new SqlParameter("@Sql", System.Data.SqlDbType.Int)
            //   };
            //parameters[0].Value = student.Studentid;
            //parameters[1].Value = student.CSharp;
            //parameters[2].Value = student.SQLServer;
            //return DBHelper.SQLHelper.ExecuteNonQueryByPROC(procName, parameters);
        }
        /// <summary>
        /// 导出Excel表查询
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetDataTable(int cid)
        {
            string sql = "SELECT ScoreList.Studentid, Students.StudentName ,CSharp,SQLServer,UpdateTime,StudentClass.ClassName FROM ScoreList INNER JOIN Students ON Students.Studentid=ScoreList.Studentid INNER JOIN StudentClass ON StudentClass.Classid=Students.Classid WHERE StudentClass.Classid=" + cid;
            DataTable table = DBHelper.SQLHelper.GetDataSet(sql).Tables[0];
            return table;
            //string procName = "dayin";
            //SqlParameter[] parameters =
            //  {
            //       new SqlParameter("@target", System.Data.SqlDbType.Int),

            //   };
            //parameters[0].Value = cid;
            //DataTable table = DBHelper.SQLHelper.GetDataSetByPROC(procName, parameters).Tables[0];
            //return table;
        }
        /// <summary>
        /// 成绩录入
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int Getdaa(ScoreListExt student)
        {
            string sql = string.Format("INSERT INTO ScoreList VALUES ({0}, {1}, {2}, {3})",student.Studentid,student.CSharp,student.SQLServer,student.UpdateTime);
            return DBHelper.SQLHelper.ExecuteNonQuery(sql);
        }
    }
}
