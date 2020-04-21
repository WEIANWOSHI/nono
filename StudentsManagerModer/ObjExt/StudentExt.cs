using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagerModer.ObjExt
{
    /// <summary>
    /// 被继承学生的班级信息
    /// </summary>
    public class StudentExt: Students
    {
        public string ClassName { get; set; }
        public string ImgPath { get; set; }
    }
}
