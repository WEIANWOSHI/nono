using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagerDAL;
using StudentsManagerModer.ObjExt;

namespace StudentManagerBLL
{
    /// <summary>
    /// 学生管理的业务逻辑
    /// </summary>
    public class StudentManager
    {
        /// <summary>
        /// 全部查询
        /// </summary>
        StudentServer server = new StudentServer();
        public List<StudentExt> GetStudents(int cid)
        {
            return server.GetStudents(cid);
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public List<StudentExt> GetStudentByldOrName(string target)
        {
            return server.GetStudentByldOrName(target);
        }
        /// <summary>
        /// 查看个人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StudentExt GetStudentById(int id)
        {
            return server.GetStudentById(id);
        }
        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public bool UpdateStudentInfor(StudentExt student)
        {
            if (server.UpdateStudentInfor(student) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 添加学员
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int addStudentInfor(StudentExt student)
        {
            return server.addStudentInfor(student);
        }
        /// <summary>
        /// 删除学员
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public bool DeleteStudentById(int sid)
        {
            if (server.DeleteStudentById(sid) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 导出Excel表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetDataTable(int cid)
        {
            return server.GetDataTable(cid);
        }
        /// <summary>
        /// 导入Excel表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<StudentExt> GetStudentByExcel(string path)
        {
            return server.GetStudentByExcel(path);
        }
        /// <summary>
        /// 将数据导入数据库
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        public int InsertStudent(StudentExt stu)
        {
            if (server.CheckStuId(stu.StudentidNo) > 0)
            {
                return -1;
            }
            return server.InsertStudent(stu);
        }
    }
}
