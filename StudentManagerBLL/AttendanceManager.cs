using StudentManagerDAL;
using StudentsManagerModer.ObjExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagerBLL
{
    public class AttendanceManager
    {
        AttendanceService attservice = new AttendanceService();
        public List<AttendanceExt> GetAttendances()
        {
            return attservice.Attendances;
        }
    }
}
