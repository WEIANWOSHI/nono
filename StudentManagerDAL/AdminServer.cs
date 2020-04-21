using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentsManagerModer;
using System.Data.SqlClient;

namespace StudentManagerDAL
{
    public class AdminServer
    {
        //管理员登录
        public Admins GetAdmins(Admins adm)
        {
            string sql = string.Format("SELECT * FROM Admins WHERE Loginld={0}", adm.Loginld);
            SqlDataReader reader = DBHelper.SQLHelper.GetReader(sql);
            Admins use = null;
            while (reader.Read())
            {
                use = new Admins()
                {
                    AdminName = reader["AdminName"].ToString(),
                    Loginld = Convert.ToInt32(reader["Loginld"]),
                    LoginPwd = reader["LoginPwd"].ToString()
                };
            }
            reader.Close();
            return use;
        }
        //public Admins GetAdmins(Admins adm)
        //{
        //    string procName = "AdminLog";
        //    SqlParameter[] parameters =
        //    {
        //        new SqlParameter("@id", System.Data.SqlDbType.Int),
        //        new SqlParameter("@Pwd", System.Data.SqlDbType.VarChar,50)
        //    };
        //    parameters[0].Value = adm.Loginld;
        //    parameters[1].Value = adm.LoginPwd;
        //    SqlDataReader reader = DBHelper.SQLHelper.GetReaderByPROC(procName, parameters);
        //    Admins use = null;
        //    while (reader.Read())
        //    {
        //        use = new Admins()
        //        {
        //            AdminName = reader["AdminName"].ToString(),
        //            Loginld = Convert.ToInt32(reader["LoginId"]),
        //            LoginPwd = reader["LoginPwd"].ToString()
        //        };
        //    }
        //    reader.Close();
        //    return use;
        //}
    }
}
