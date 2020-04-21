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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StudentsManagerModer;
using StudentsManagerModer.ObjExt;
using StudentManagerBLL;
using System.IO;
using Common;

namespace 学员管理系统_ADO.NET_.view
{
    /// <summary>
    /// scorelistFENGXI.xaml 的交互逻辑
    /// </summary>
    public partial class scorelistFENGXI : UserControl
    {
        StudentClassManager csm = new StudentClassManager();
        StudentManagerBLL.ScoreListManager sm = new StudentManagerBLL.ScoreListManager();
        List<ScoreListExt> students = null;
        public scorelistFENGXI()
        {
            InitializeComponent();
            List<StudentClass> classes = csm.GetClasses();
            smclassCmb.ItemsSource = classes;
            //设置下拉框的显示文本
            smclassCmb.DisplayMemberPath = "ClassName";
            //设置下拉框显示文本对应的value
            smclassCmb.SelectedValuePath = "Classid";
            smclassCmb.SelectedIndex = 0;
            //给DataGrid进行对应列数据绑定
            students = sm.GetScoreList(Convert.ToInt32(smclassCmb.SelectedValue));
            smDgStudentLsit.ItemsSource = students;
        }
        /// <summary>
        /// 选择班级查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectByCId_Click(object sender, RoutedEventArgs e)
        {
            if (smclassCmb.SelectedIndex < 0)
            {
                MessageBox.Show("请选择班级！", "提示");
                return;
            }
            //给DataGrid进行对应列数据绑定
            students = sm.GetScoreList(Convert.ToInt32(smclassCmb.SelectedValue));
            smDgStudentLsit.ItemsSource = students;
        }
        /// <summary>
        /// 学号排列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroupBySid_Click(object sender, RoutedEventArgs e)
        {
            if (smDgStudentLsit.ItemsSource == null)
            {
                return;
            }
            if ((sender as Button).Tag.ToString() == "True")
            {
                students.Sort(new ScoreListIdDESC(true));
                groupBySidImg.Source = new BitmapImage(new Uri("/img/ico/up.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "False";
            }
            else if ((sender as Button).Tag.ToString() == "False")
            {
                students.Sort(new ScoreListIdDESC(false));
                groupBySidImg.Source = new BitmapImage(new Uri("/img/ico/down.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "True";
            }
            smDgStudentLsit.ItemsSource = null;
            smDgStudentLsit.ItemsSource = students;
        }
        /// <summary>
        /// 名字排列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroupBySName_Click(object sender, RoutedEventArgs e)
        {
            if (smDgStudentLsit.ItemsSource == null)
            {
                return;
            }
            if ((sender as Button).Tag.ToString() == "True")
            {
                students.Sort(new StudentsNameDESC(true));
                groupBySNameImg.Source = new BitmapImage(new Uri("/img/ico/jiang.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "False";
            }
            else if ((sender as Button).Tag.ToString() == "False")
            {
                students.Sort(new StudentsNameDESC(false));
                groupBySNameImg.Source = new BitmapImage(new Uri("/img/ico/sheng.ico", UriKind.RelativeOrAbsolute));
                (sender as Button).Tag = "True";
            }
            smDgStudentLsit.ItemsSource = null;
            smDgStudentLsit.ItemsSource = students;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
        ScoreListExt selectStu = null;
        /// <summary>
        /// 成绩录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void luruchengji_Click(object sender, RoutedEventArgs e)
        {
            selectStu = smDgStudentLsit.SelectedItem as ScoreListExt;
            ScoreListExt objStu = sm.GetStudentById(selectStu.Studentid);
            FrmScoreListinfor updateStuInfor = new FrmScoreListinfor(objStu);
            updateStuInfor.ShowDialog();
            students = sm.GetScoreList(Convert.ToInt32(smclassCmb.SelectedValue));
            smDgStudentLsit.ItemsSource = students;
        }
    }
}
