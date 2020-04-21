using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagerModer
{
    /// <summary>
    /// 打卡实体
    /// </summary>
    [Serializable]
    public class Attendance
    {
        public int Id { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public int CardNo { get; set; }
        /// <summary>
        /// 打卡时间
        /// </summary>
        public string DTime { get; set; }

        public string Notes { get; set; }
    }
}
