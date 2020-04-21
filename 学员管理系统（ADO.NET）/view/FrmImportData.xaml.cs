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
using StudentManagerBLL;
using StudentsManagerModer;
using StudentsManagerModer.ObjExt;

namespace 学员管理系统_ADO.NET_.view
{
    /// <summary>
    /// FrmImportData.xaml 的交互逻辑
    /// </summary>
    public partial class FrmImportData : UserControl
    {
        public FrmImportData()
        {
            InitializeComponent();
        }
        StudentManagerBLL.StudentManager manager = new StudentManagerBLL.StudentManager();
        List<StudentExt> list = null;
        private void btnSelectExcel_Click(object sender, RoutedEventArgs e)
        {
            //导入Excel数据表绑定到DATAGRID控件上
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "工作簿Excel文件(*.xlsx;*.xls)|*.xlsx;*.xls";
            if (fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;
                list = manager.GetStudentByExcel(path);
                dgStudent.ItemsSource = null;
                dgStudent.AutoGenerateColumns = false;
                dgStudent.ItemsSource = list;
            }
        }
        List<StudentExt> lastlist = new List<StudentExt>();
        /// <summary>
        /// 将数据传输到数据库中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportData_Click(object sender, RoutedEventArgs e)
        {
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int res = manager.InsertStudent(list[i]);
                    if (res <= 0)
                    {
                        lastlist.Add(list[i]);
                        continue;
                    }
                }
                //所有成员上传成功
                if (lastlist.Count <= 0)
                {
                    dgStudent.ItemsSource = null;
                    MessageBox.Show("所有数据上传成功！", "提示");
                }
                else
                {
                    dgStudent.ItemsSource = null;
                    dgStudent.ItemsSource = lastlist;
                    MessageBox.Show("剩余学员信息上传失败！请检查！", "提示");
                }
            }
            else
            {
                MessageBox.Show("当前没有任何数据！", "提示");
            }
        }
    }
}
