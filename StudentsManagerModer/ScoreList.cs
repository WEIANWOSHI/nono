using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagerModer
{
    /// <summary>
    /// 成绩实体
    /// </summary>
    [Serializable]
    public class ScoreList
    {
        public int Id { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public int Studentid { get; set; }
        /// <summary>
        /// c#成绩
        /// </summary>
        public int CSharp { get; set; }
        /// <summary>
        /// DB成绩
        /// </summary>
        public int SQLServer { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
