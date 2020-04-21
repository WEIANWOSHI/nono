using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagerModer.ObjExt
{
    [Serializable]
    public class AttendanceExt: Attendance
    {
        public string StudentName { get; set; }
    }
}
