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
    /// FrmScoreList.xaml 的交互逻辑
    /// </summary>
    public partial class FrmScoreList : UserControl
    {
        StudentClassManager csm = new StudentClassManager();
        StudentManagerBLL.ScoreListManager sm = new StudentManagerBLL.ScoreListManager();
        List<ScoreListExt> students = null;
        ScoreListExt selectStu = null;
        public FrmScoreList()
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
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 修改成绩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gai_Click(object sender, RoutedEventArgs e)
        {
            selectStu = smDgStudentLsit.SelectedItem as ScoreListExt;
            if (IdList.Contains(selectStu.Studentid))
            {
                MessageBox.Show("请关闭正在查看的学员成绩信息界面", "提示");
                return;
            }
            if (selectStu == null)
            {
                MessageBox.Show("请选择要修改的学员成绩！", "提示");
                return;
            }
            ScoreListExt objStu = sm.GetStudentById(selectStu.Studentid);
            FrmScoreListinfor updateStuInfor = new FrmScoreListinfor(objStu);
            updateStuInfor.ShowDialog();
            students = sm.GetScoreList(Convert.ToInt32(smclassCmb.SelectedValue));
            smDgStudentLsit.ItemsSource = students;
        }
        List<int> IdList = new List<int>();
        /// <summary>
        /// 删除成绩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shan_Click(object sender, RoutedEventArgs e)
        {
            selectStu = smDgStudentLsit.SelectedItem as ScoreListExt;
            if (IdList.Contains(selectStu.Studentid))
            {
                MessageBox.Show("请关闭正在查看的学员信息界面", "提示");
                return;
            }
            if (selectStu == null)
            {
                MessageBox.Show("请选择要删除的学员！", "提示");
                return;
            }
            ScoreListExt student = sm.GetStudentById(selectStu.Studentid);
            if (student != null)
            {
                MessageBox.Show("您选择的学员信息已经被删除！", "提示");
                return;
            }
            MessageBoxResult mbr = MessageBox.Show("您确定要删除【" + student.StudentName + "】", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (mbr == MessageBoxResult.OK)
            {
                if (sm.DeleteStudentById(student.Studentid))
                {
                    MessageBox.Show("删除成功！", "提示");
                }
                else
                {
                    MessageBox.Show("删除失败请稍后再试！", "提示");
                }
            }
        }
        /// <summary>
        /// 导出导入Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog();
            fileDialog.Filter = "Excel工作簿(*.xlsx;*.xls)|*.xlsx;*.xls";
            fileDialog.FileName = "学生成绩表.xlsx";
            fileDialog.Title = "导出到Excel表";
            if (fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;
                System.Data.DataTable table = sm.GetDataTable((int)smclassCmb.SelectedValue);
                if (table.Rows.Count <= 0)
                {
                    MessageBox.Show("该班级暂无学生数据！", "提示");
                    return;
                }
                if (Common.ImportOrExportData.ExportToExcel(table, path))
                {
                    MessageBox.Show("导出数据完成！", "提示");
                }
                else
                {
                    MessageBox.Show("导出数据失败，请稍后再试！", "提示");
                }
            }
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectBySIN_Click_1(object sender, RoutedEventArgs e)
        {
            smclassCmb.SelectedIndex = -1;
            string target = mstxtIdorName.Text.Trim();
            List<ScoreListExt> liststu = sm.GetStudentByldOrName(target);
            if (liststu.Count <= 0)
            {
                MessageBox.Show("未查询到相关信息", "提示");
                return;
            }
            students = liststu;
            smDgStudentLsit.ItemsSource = null;
            smDgStudentLsit.ItemsSource = students;
        }
    }
    class ScoreListIdDESC : IComparer<ScoreListExt>
    {
        public ScoreListIdDESC(bool b)
        {
            B = b;
        }
        public bool B { get; set; }
        /// <summary>
        /// 升序
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(ScoreListExt x, ScoreListExt y)
        {
            if (B)
            {
                return x.Studentid.CompareTo(y.Studentid);
            }
            else
            {
                return y.Studentid.CompareTo(x.Studentid);
            }
        }
    }
    /// <summary>
    /// 比较声明器
    /// </summary>
    class StudentsNameDESC : IComparer<ScoreListExt>
    {
        public StudentsNameDESC(bool b)
        {
            B = b;
        }
        public bool B { get; set; }
        /// <summary>
        /// 降序
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(ScoreListExt x, ScoreListExt y)
        {
            if (B)
            {
                return y.StudentName.CompareTo(x.StudentName);
            }
            else
            {
                return x.StudentName.CompareTo(y.StudentName);
            }
        }
    }
}
