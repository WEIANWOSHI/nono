using StudentManagerBLL;
using StudentsManagerModer.ObjExt;
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

namespace 学员管理系统_ADO.NET_.view
{
    /// <summary>
    /// FrmscorelistAdd.xaml 的交互逻辑
    /// </summary>
    public partial class FrmscorelistAdd : Window
    {
        StudentClassManager csm = new StudentClassManager();
        StudentManagerBLL.ScoreListManager manager = new StudentManagerBLL.ScoreListManager();
        public ScoreListExt ScoreList { get; set; }
        public FrmscorelistAdd(ScoreListExt stu)
        {
            InitializeComponent();
            ScoreList = stu;
            textcshap.Text = stu.CSharp.ToString();
            textsql.Text = stu.SQLServer.ToString();
            time.Text = stu.UpdateTime.ToString("yyyy-MM-dd");
            smclassCmb.DisplayMemberPath = "Studentid";
            smclassCmb.SelectedIndex = stu.Studentid - 1;
        }

        private void jia_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInfor())
            {
                ScoreList.CSharp = Convert.ToInt32(textcshap.Text);
                ScoreList.SQLServer = Convert.ToInt32(textsql.Text);
                ScoreList.UpdateTime = Convert.ToDateTime(time.Text);
                ScoreList.Studentid= Convert.ToInt32(smclassCmb.SelectedItem);
                if (manager.Getdaa(ScoreList)>0)
                {
                    System.Windows.MessageBox.Show("添加成功！", "提示");
                    this.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("添加失败，请稍后再试！", "提示");
                }
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
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
