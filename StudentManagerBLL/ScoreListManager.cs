using StudentManagerDAL;
using StudentsManagerModer.ObjExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagerBLL
{
    /// <summary>
    /// 成绩管理的业务逻辑
    /// </summary>
    public class ScoreListManager
    {
        /// <summary>
        /// 全部查询
        /// </summary>
        ScoreListServer server = new ScoreListServer();
        public List<ScoreListExt> GetScoreList(int cid)
        {
            return server.GetScoreList(cid);
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public List<ScoreListExt> GetStudentByldOrName(string target)
        {
            return server.GetStudentByldOrName(target);
        }
        /// <summary>
        /// 删除成绩
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public bool DeleteStudentById(int sid)
        {
            if (server.DeleteStudentById(sid) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 查看个人成绩
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ScoreListExt GetStudentById(int id)
        {
            return server.GetStudentById(id);
        }
        /// <summary>
        /// 修改成绩
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public bool UpdateStudentInfor(ScoreListExt student)
        {
            if (server.UpdateStudentInfor(student) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 工作簿导出
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public DataTable GetDataTable(int cid)
        {
            return server.GetDataTable(cid);
        }
        public int Getdaa(ScoreListExt student) 
        {
            return server.Getdaa(student);
        }
    }
}
