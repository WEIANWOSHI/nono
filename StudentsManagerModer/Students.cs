using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagerModer
{
    /// <summary>
    /// 学生实体
    /// </summary>
    [Serializable]
    public class Students
    {
        /// <summary>
        /// 学号
        /// </summary>
        public int Studentid { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string StudentidNo { get; set; }
        /// <summary>
        /// 考勤卡号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string Stuimage { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string PhinoNumber { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string StudentAddress { get; set; }
        /// <summary>
        /// 班级编号
        /// </summary>
        public int Classid { get; set; }
    }
}
