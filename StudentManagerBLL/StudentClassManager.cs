using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentsManagerModer;
using StudentManagerDAL;

namespace StudentManagerBLL
{
    /// <summary>
    /// 班级的业务逻辑
    /// </summary>
    public class StudentClassManager
    {
        StudentClassServer server = new StudentClassServer();
        public List<StudentClass> GetClasses()
        {
            return server.GetClasses();
        }
    }
}
