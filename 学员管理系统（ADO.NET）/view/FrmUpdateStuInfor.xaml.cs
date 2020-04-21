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
    /// FrmUpdateStuInfor.xaml 的交互逻辑
    /// </summary>
    public partial class FrmUpdateStuInfor : Window
    {
        StudentClassManager csm = new StudentClassManager();
        StudentManagerBLL.StudentManager manager = new StudentManagerBLL.StudentManager();
        common.BitmapImg image = null;
        public StudentExt student { get; set; }
        /// <summary>
        /// 修改界面设置
        /// </summary>
        /// <param name="stu"></param>
        public FrmUpdateStuInfor(StudentExt stu)
        {
            InitializeComponent();
            student = stu;
            txtAddress.Text = stu.StudentAddress;
            txtAge.Content = stu.Age.ToString();
            txtCardNo.Text = stu.CardNo;
            txtName.Text = stu.StudentName;
            txtPhoneNumber.Text = stu.PhinoNumber;
            txtStuNoId.Text = stu.StudentidNo;
            if (stu.Gender == "男")
            {
                radBoy.IsChecked = true;
            }
            else
            {
                radGirl.IsChecked = true;
            }
            datePkBirthday.Content = stu.Birthday.ToString("yyyy-MM-dd");
            if (string.IsNullOrEmpty(stu.Stuimage))
            {
                stuImg.Source = new BitmapImage(new Uri("/img/bg/zwzp.jpg", UriKind.RelativeOrAbsolute));
            }
            else
            {
                image = SerializeObjectTostring.DeserializeObject(stu.Stuimage) as common.BitmapImg;
                img.Buffer = image.Buffer;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(image.Buffer);
                bitmap.EndInit();
                stuImg.Source = bitmap;
            }
            List<StudentClass> classes = csm.GetClasses();
            cmbClassName.ItemsSource = classes;
            cmbClassName.DisplayMemberPath = "ClassName";
            cmbClassName.SelectedValuePath = "Classid";
            cmbClassName.SelectedIndex = stu.Classid - 1;
        }
        common.BitmapImg img = new common.BitmapImg();
        public static string imgPath;
        /// <summary>
        /// 打开摄像头拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenVideo_Click(object sender, RoutedEventArgs e)
        {
            FrmStudentPhoto photo = new FrmStudentPhoto();
            photo.ShowDialog();
            if (!string.IsNullOrEmpty(imgPath))
            {
                //照片刷新后对新照片进行序列化
                stuImg.Source = new BitmapImage(new Uri(imgPath, UriKind.RelativeOrAbsolute));
                img.Buffer = File.ReadAllBytes(imgPath);
            }
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUploadPic_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "图像文件(*.jpg;*.jpeg;*.gif;*.png;*.bmp)|*.jpg;*.jpeg;*.gif;*.png,*.bmp";
            if (fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;
                stuImg.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                stuImg.Stretch = Stretch.UniformToFill;
                img.Buffer = File.ReadAllBytes(path);
            }
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
                student.StudentName = txtName.Text;
                student.Age = (int)txtAge.Content;
                student.Birthday = (DateTime)datePkBirthday.Content;
                student.CardNo = txtCardNo.Text;
                student.Classid = (int)cmbClassName.SelectedValue;
                student.Gender = (radBoy.IsChecked == true ? "男" : "女");
                student.PhinoNumber = txtPhoneNumber.Text;
                student.StudentAddress = (string.IsNullOrEmpty(txtAddress.Text) ? null : txtAddress.Text);
                student.StudentidNo = txtStuNoId.Text;
                if (stuImg.Source == new BitmapImage(new Uri("/img/bg/zwzp.jpg", UriKind.RelativeOrAbsolute)))
                {
                    student.Stuimage = null;
                }
                else
                {
                    if (image != null && img.Buffer == image.Buffer)
                    {
                        student.Stuimage = Common.SerializeObjectTostring.SerializeObject(image);
                    }
                    else
                    {
                        student.Stuimage = Common.SerializeObjectTostring.SerializeObject(img);
                    }

                }
                if (manager.UpdateStudentInfor(student))
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
        /// 输入的数据检测
        /// </summary>
        /// <returns></returns>
        bool CheckInfor()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                System.Windows.MessageBox.Show("姓名不能为空！");
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAge.Content.ToString()))
            {
                System.Windows.MessageBox.Show("年龄不能为空！");
                txtAge.Focus();
                return false;
            }
            else if (!DataValidata.IsInteger(txtAge.Content.ToString()))
            {
                System.Windows.MessageBox.Show("年龄必须是纯数字！");
                txtAge.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCardNo.Text))
            {
                System.Windows.MessageBox.Show("打卡号不能为空！");
                txtCardNo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtStuNoId.Text))
            {
                System.Windows.MessageBox.Show("身份证号不能为空！");
                txtStuNoId.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                System.Windows.MessageBox.Show("联系方式不能为空！");
                txtPhoneNumber.Focus();
                return false;
            }
            else if (DataValidata.IsPhone(txtPhoneNumber.Text))
            {
                System.Windows.MessageBox.Show("电话号输入错误！");
                txtPhoneNumber.Focus();
                return false;
            }
            if (DataValidata.Data(Convert.ToInt32(datePkBirthday.Content)))
            {
                System.Windows.MessageBox.Show("日期格式错误！");
                datePkBirthday.Focus();
                return false;
            }
            return true;
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
        /// <summary>
        /// 联系方式判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                System.Windows.MessageBox.Show("联系方式不能为空！");
                txtPhoneNumber.Focus();
            }
            else if (DataValidata.IsPhone(txtPhoneNumber.Text))
            {
                System.Windows.MessageBox.Show("电话号输入错误！");
                txtPhoneNumber.Focus();
            }
        }
        /// <summary>
        /// 姓名判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                System.Windows.MessageBox.Show("姓名不能为空！");
                txtName.Focus();
            }
        }
        /// <summary>
        /// 年龄判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAge_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAge.Content.ToString()))
            {
                System.Windows.MessageBox.Show("年龄不能为空！");
                txtAge.Focus();
            }
            else if (!DataValidata.IsInteger(txtAge.Content.ToString()))
            {
                System.Windows.MessageBox.Show("年龄必须是纯数字！");
                txtAge.Focus();
            }
        }
        /// <summary>
        /// 身份证号判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtStuNoId_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtStuNoId.Text))
            {
                System.Windows.MessageBox.Show("身份证号不能为空！");
                txtStuNoId.Focus();
            }
            else if (Common.DataValidata.IsIdentity(txtStuNoId.Text.Trim()))
            {
                System.Windows.MessageBox.Show("身份证号非法！");
                txtStuNoId.Focus();
            }
            else
            {
                datePkBirthday.Content = Common.GetValueById.GetBirthday(txtStuNoId.Text.Trim()).ToString("yyyy-MM-dd");

                txtAge.Content = Common.GetValueById.GetAge(Common.GetValueById.GetBirthday(txtStuNoId.Text.Trim()));
            }
        }
    }
}
