using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using StudentsManagerModer;
using StudentsManagerModer.ObjExt;
using StudentManagerDAL;

namespace StudentManagerDAL
{
    public class AttendanceService
    {
        public List<AttendanceExt> Attendances
        {
            get
            {
                List<AttendanceExt> attendances = new List<AttendanceExt>();
                string sqlstr = "select a.Id,a.CardNo,s.StudentName,a.DTime from Attendance a ,Students s where a.CardNo=s.CardNo";
                DataTable table = DBHelper.SQLHelper.GetDataTable(sqlstr);
                foreach (DataRow item in table.Rows)
                {
                    AttendanceExt attendance = new AttendanceExt()
                    {
                        Id = Convert.ToInt32(item["Id"]),
                        CardNo = Convert.ToInt32(item["CardNo"]),
                        DTime = item["DTime"].ToString(),
                        Notes = item["Notes"].ToString(),
                        StudentName = item["StudentName"].ToString()
                    };
                    attendances.Add(attendance);
                }
                if (attendances == null) return null;
                return attendances;
            }
        }
    }
}
