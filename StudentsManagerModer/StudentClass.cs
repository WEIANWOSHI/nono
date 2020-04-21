using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagerModer
{
    /// <summary>
    /// 班级实体
    /// </summary>
    [Serializable]
    public class StudentClass
    {
        /// <summary>
        /// 班级编号
        /// </summary>
        public int Classid { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }
    }
}
