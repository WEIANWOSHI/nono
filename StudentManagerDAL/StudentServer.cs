using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentsManagerModer;
using StudentsManagerModer.ObjExt;
using System.Data.SqlClient;
using System.Data;
using Common;

namespace StudentManagerDAL
{
    /// <summary>
    /// 学生管理的数据访问服务
    /// </summary>
    public class StudentServer
    {
        /// <summary>
        /// 根据班级查询
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<StudentExt> GetStudents(int cid)
        {
            //string sql = "SELECT Studentid,StudentName,Gender,Birthday,StudentIdNo,CardNo,StuImage,Age,PhinoNumber,StudentAddress,StudentClass.ClassName FROM Students INNER JOIN StudentClass ON StudentClass.Classid=Students.Classid WHERE StudentClass.Classid=" + cid;
            //SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            string procName = "chaxunxuesheng";
            SqlParameter[] parameters =
              {
                   new SqlParameter("@target", System.Data.SqlDbType.VarChar,50),

               };
            parameters[0].Value = cid;
            SqlDataReader reader = DBHelper.SQLHelper.GetReaderByPROC(procName, parameters);
            List<StudentExt> list = DataReturnObj(reader);
            return list;
        }

        private static List<StudentExt> DataReturnObj(SqlDataReader reader)
        {
            List<StudentExt> list = new List<StudentExt>();
            while (reader.Read())
            {
                list.Add(new StudentExt()
                {
                    Studentid = Convert.ToInt32(reader["Studentid"]),
                    Age = Convert.ToInt32(reader["Age"]),
                    Birthday = Convert.ToDateTime(reader["Birthday"]),
                    CardNo = reader["CardNo"].ToString(),
                    ClassName = reader["ClassName"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    PhinoNumber = reader["PhinoNumber"].ToString(),
                    StudentAddress = reader["StudentAddress"].ToString(),
                    StudentidNo = reader["StudentidNo"].ToString(),
                    StudentName = reader["StudentName"].ToString(),
                    Stuimage = reader["Stuimage"].ToString()
                });
            }

            return list;
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        public List<StudentExt> GetStudentByldOrName(string target)
        {
            //string sql = string.Format("SELECT Studentid,StudentName,Gender,Birthday,StudentIdNo,CardNo,StuImage,Age,PhinoNumber,StudentAddress,StudentClass.ClassName FROM Students INNER JOIN StudentClass ON StudentClass.Classid=Students.Classid WHERE Studentid LIKE '%{0}%' OR StudentName LIKE '%{0}%'", target);
            //SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            string procName = "IDxuesheng";
            SqlParameter[] parameters =
              {
                   new SqlParameter("@target", System.Data.SqlDbType.Int),

               };
            parameters[0].Value = target;
            SqlDataReader reader = DBHelper.SQLHelper.GetReaderByPROC(procName, parameters);
            List<StudentExt> list = DataReturnObj(reader);
            reader.Close();
            return list;
        }
        /// <summary>
        /// 查看学员个人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StudentExt GetStudentById(int id)
        {
            //string sql = string.Format("SELECT Studentid,StudentName,Gender,Birthday,StudentidNo,CardNo,Stuimage,Age,PhinoNumber,StudentAddress,Students.Classid,StudentClass.ClassName FROM Students INNER JOIN StudentClass ON StudentClass.Classid=Students.Classid WHERE Studentid = {0}", id);
            //SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            string procName = "chakanxuesheng";
            SqlParameter[] parameters =
              {
                   new SqlParameter("@target", System.Data.SqlDbType.Int),

               };
            parameters[0].Value = id;
            SqlDataReader reader = DBHelper.SQLHelper.GetReaderByPROC(procName, parameters);
            StudentExt student = null;
            while (reader.Read())
            {
                student=(new StudentExt()
                {
                    Studentid = Convert.ToInt32(reader["Studentid"]),
                    Age = Convert.ToInt32(reader["Age"]),
                    Birthday = Convert.ToDateTime(reader["Birthday"]),
                    CardNo = reader["CardNo"].ToString(),
                    ClassName = reader["ClassName"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    PhinoNumber = reader["PhinoNumber"].ToString(),
                    StudentAddress = reader["StudentAddress"].ToString(),
                    StudentidNo = reader["StudentidNo"].ToString(),
                    StudentName = reader["StudentName"].ToString(),
                    Stuimage = reader["Stuimage"].ToString(),
                    Classid = Convert.ToInt32(reader["Classid"])
                });
            }
            return student;
        }
        /// <summary>
        /// 修改学员信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int UpdateStudentInfor(StudentExt student)
        {
            string sql = string.Format("UPDATE Students SET StudentName='{0}',Gender='{1}',Birthday='{2}',StudentidNo='{3}',CardNo='{4}',Stuimage='{5}',Age={6},PhinoNumber='{7}',StudentAddress='{8}',Classid={9} WHERE Studentid={10}", student.StudentName, student.Gender, student.Birthday, student.StudentidNo, student.CardNo, student.Stuimage, student.Age, student.PhinoNumber, student.StudentAddress, student.Classid, student.Studentid);
            return DBHelper.SQLHelper.ExecuteNonQuery(sql);
            //string procName = "xiugaixueshengxinxi";
            //SqlParameter[] parameters =
            //  {
            //       new SqlParameter("@name", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@sex", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@birthday", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@studentidNo", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@cardNo", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@stuImage", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@age", System.Data.SqlDbType.Int),
            //       new SqlParameter("@phoneNumber", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@studentAddress", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@classlD", System.Data.SqlDbType.Int),
            //       new SqlParameter("@studentlD", System.Data.SqlDbType.Int)

            //   };
            //parameters[0].Value = student.StudentName;
            //parameters[1].Value = student.Gender;
            //parameters[2].Value = student.Birthday;
            //parameters[3].Value = student.StudentidNo;
            //parameters[4].Value = student.CardNo;
            //parameters[5].Value = student.Stuimage;
            //parameters[6].Value = student.Age;
            //parameters[7].Value = student.PhinoNumber;
            //parameters[8].Value = student.StudentAddress;
            //parameters[9].Value = student.Classid;
            //parameters[10].Value = student.Studentid;
            //return DBHelper.SQLHelper.ExecuteNonQueryByPROC(procName, parameters);
        }
        /// <summary>
        /// 添加学员
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int addStudentInfor(StudentExt student)
        {
            string sql = string.Format("INSERT INTO Students VALUES ({0},'{1}','{2}',{3},{4},{5},{6},{7},{8},'{9}',{10})", student.Studentid, student.StudentName, student.Gender, student.Birthday, student.StudentidNo, student.CardNo, student.Stuimage, student.Age, student.PhinoNumber, student.StudentAddress, student.Classid);
            return DBHelper.SQLHelper.ExecuteNonQuery(sql);
            //string procName = "xiugaixueshengxinxi";
            //SqlParameter[] parameters =
            //  {
            //       new SqlParameter("@name", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@sex", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@birthday", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@studentidNo", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@cardNo", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@stuImage", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@age", System.Data.SqlDbType.Int),
            //       new SqlParameter("@phoneNumber", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@studentAddress", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@classlD", System.Data.SqlDbType.Int),
            //       new SqlParameter("@studentlD", System.Data.SqlDbType.Int)

            //   };
            //parameters[0].Value = student.StudentName;
            //parameters[1].Value = student.Gender;
            //parameters[2].Value = student.Birthday;
            //parameters[3].Value = student.StudentidNo;
            //parameters[4].Value = student.CardNo;
            //parameters[5].Value = student.Stuimage;
            //parameters[6].Value = student.Age;
            //parameters[7].Value = student.PhinoNumber;
            //parameters[8].Value = student.StudentAddress;
            //parameters[9].Value = student.Classid;
            //parameters[10].Value = student.Studentid;
            //return DBHelper.SQLHelper.ExecuteNonQueryByPROC(procName, parameters);
        }
        /// <summary>
        /// 删除学员
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public int DeleteStudentById(int sid)
        {
            string sql = "DELETE FROM Students WHERE Studentid=" + sid;
            return DBHelper.SQLHelper.ExecuteNonQuery(sql);
            //string procName = "shanchuxuesheng";
            //SqlParameter[] parameters =
            //  {
            //       new SqlParameter("@target", System.Data.SqlDbType.Int),

            //   };
            //parameters[0].Value = sid;
            //return DBHelper.SQLHelper.ExecuteNonQueryByPROC(procName, parameters);
        }
        /// <summary>
        /// 导出Excel表查询
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetDataTable(int cid)
        {
            string sql = "SELECT Studentid ,StudentName,Gender,Birthday,StudentidNo,CardNo,Age,PhinoNumber,StudentAddress,StudentClass.ClassName FROM Students INNER JOIN StudentClass ON StudentClass.Classid=Students.Classid WHERE StudentClass.Classid=" + cid;
            DataTable table = DBHelper.SQLHelper.GetDataSet(sql).Tables[0];
            return table;
            //string procName = "dayinxuesheng";
            //SqlParameter[] parameters =
            //  {
            //       new SqlParameter("@target", System.Data.SqlDbType.Int),

            //   };
            //parameters[0].Value = cid;
            //DataTable table = DBHelper.SQLHelper.GetDataSetByPROC(procName, parameters).Tables[0];
            //return table;
        }
        StudentClassServer classServer = new StudentClassServer();
        /// <summary>
        /// 导入Excel表查询
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<StudentExt> GetStudentByExcel(string path)
        {
            List<StudentExt> list = new List<StudentExt>();
            string sql = string.Format("select * from [{0}$] ", Common.ImportOrExportData.SheetName(path));
            DataSet ds = Common.OleDbHelper.GetDataSet(sql, path);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new StudentExt()
                {
                    StudentName = row["姓名"].ToString(),
                    Gender = row["性别"].ToString(),
                    Birthday = Convert.ToDateTime(row["生日"]),
                    Age = DateTime.Now.Year - Convert.ToDateTime(row["生日"]).Year,
                    CardNo = row["打卡号"].ToString(),
                    StudentidNo = row["身份证号"].ToString(),
                    PhinoNumber = row["电话号"].ToString(),
                    StudentAddress = row["地址"].ToString(),
                    ClassName = row["班级"].ToString(),
                    Classid = classServer.GetClassIdByName(row["班级"].ToString())
                });
            }
            return list;
        }
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int CheckStuId(string id)
        {
            string sql = "SELECT COUNT(*) FROM Students WHERE StudentidNo='" + id + "'";
            object res = DBHelper.SQLHelper.ExecuteScalar(sql);
            return (int)res;
            //string procName = "chaxunxuesheng";
            //SqlParameter[] parameters =
            //  {
            //       new SqlParameter("@target", System.Data.SqlDbType.Int),

            //   };
            //parameters[0].Value = id;
            //object res = DBHelper.SQLHelper.ExecuteScalarByPROC(procName, parameters);
            //return (int)res;
        }
        /// <summary>
        /// 将数据添加进数据库
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        public int InsertStudent(StudentExt stu)
        {
            string sql = string.Format("INSERT INTO Students(StudentName,Gender,Birthday,StudentidNo,CardNo,Age,PhinoNumber,StudentAddress,Classid) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{6}', '{7}', {8})", stu.StudentName, stu.Gender, stu.Birthday, stu.StudentidNo, stu.CardNo, stu.Age, stu.PhinoNumber, stu.StudentAddress, stu.Classid);
            return DBHelper.SQLHelper.ExecuteNonQuery(sql);
            //string procName = "tianjiaxueshengxinxi";
            //SqlParameter[] parameters =
            //  {
            //       new SqlParameter("@name", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@sex", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@birthday", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@studentidNo", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@cardNo", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@stuImage", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@age", System.Data.SqlDbType.Int),
            //       new SqlParameter("@phoneNumber", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@studentAddress", System.Data.SqlDbType.VarChar,50),
            //       new SqlParameter("@classlD", System.Data.SqlDbType.Int),
            //       new SqlParameter("@studentlD", System.Data.SqlDbType.Int)

            //   };
            //parameters[0].Value = stu.StudentName;
            //parameters[1].Value = stu.Gender;
            //parameters[2].Value = stu.Birthday;
            //parameters[3].Value = stu.StudentidNo;
            //parameters[4].Value = stu.CardNo;
            //parameters[5].Value = stu.Stuimage;
            //parameters[6].Value = stu.Age;
            //parameters[7].Value = stu.PhinoNumber;
            //parameters[8].Value = stu.StudentAddress;
            //parameters[9].Value = stu.Classid;
            //parameters[10].Value = stu.Studentid;
            //return DBHelper.SQLHelper.ExecuteNonQueryByPROC(procName, parameters);
        }
    }
}
