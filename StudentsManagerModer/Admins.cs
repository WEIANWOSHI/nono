using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagerModer
{
    /// <summary>
    /// 登录实体
    /// </summary>
    [Serializable]
    public class Admins
    {
        /// <summary>
        /// 登陆账号
        /// </summary>
        public int Loginld { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string LoginPwd { get; set; }
        /// <summary>
        /// 管理员姓名
        /// </summary>
        public string AdminName { get; set; }
    }
}
