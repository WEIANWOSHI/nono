using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StudentsManagerModer;
using StudentsManagerModer.ObjExt;
using StudentManagerBLL;
using Common;
using System.IO;

namespace 学员管理系统_ADO.NET_.view
{
    /// <summary>
    /// FrmScoreListinfor.xaml 的交互逻辑
    /// </summary>
    public partial class FrmScoreListinfor : Window
    {
        StudentClassManager csm = new StudentClassManager();
        StudentManagerBLL.ScoreListManager manager = new StudentManagerBLL.ScoreListManager();
        public ScoreListExt ScoreList { get; set; }
        public FrmScoreListinfor(ScoreListExt stu)
        {
            InitializeComponent();
            ScoreList = stu;
            textcshap.Text = stu.CSharp.ToString();
            textsql.Text = stu.SQLServer.ToString();
        }
        /// <summary>
        /// 确认修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSureUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInfor())
            {
                ScoreList.CSharp = Convert.ToInt32(textcshap.Text);
                ScoreList.SQLServer = Convert.ToInt32(textsql.Text);
                if (manager.UpdateStudentInfor(ScoreList))
                {
                    System.Windows.MessageBox.Show("修改成功！", "提示");
                    this.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("修改失败，请稍后再试！", "提示");
                }
            }
        }
        /// <summary>
        /// 取消修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        bool CheckInfor() 
        {
            if (string.IsNullOrEmpty(textcshap.Text))
            {
                System.Windows.MessageBox.Show("c#不能为空！");
                textcshap.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(textsql.Text))
            {
                System.Windows.MessageBox.Show("sql不能为空！");
                textsql.Focus();
                return false;
            }
            return true;
        }
    }
}
