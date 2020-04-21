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
    /// 登陆的页面逻辑
    /// </summary>
    public class AdminManager
    {
        AdminServer server = new AdminServer();
        public Admins GetAdmins(Admins adm)
        {
            return server.GetAdmins(adm);
        }
    }
}
