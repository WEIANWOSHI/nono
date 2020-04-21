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
using StudentsManagerModer.ObjExt;
using Common;
using System.IO;

namespace 学员管理系统_ADO.NET_.view
{
    /// <summary>
    /// FrmStudentInfor.xaml 的交互逻辑
    /// </summary>
    public partial class FrmStudentInfor : Window
    {
        /// <summary>
        /// 个人详细窗口设计
        /// </summary>
        /// <param name="stu"></param>
        public FrmStudentInfor(StudentExt stu)
        {
            InitializeComponent();
            StuId = stu.Studentid;
            this.Title = stu.StudentName + "-信息";
            lblAddress.Content = stu.StudentAddress;
            lblAge.Content = stu.Age;
            lblBirthday.Content = stu.Birthday.ToString("yyyy-MM-dd");
            lblCardNo.Content = stu.CardNo;
            lblClassName.Content = stu.ClassName;
            lblGender.Content = stu.Gender;
            lblName.Content = stu.StudentName;
            lblPhoneNumber.Content = stu.PhinoNumber;
            lblStuId.Content = stu.Studentid;
            lblStuNoId.Content = stu.StudentidNo;
            if (string.IsNullOrEmpty(stu.Stuimage))
            {
                stuImg.Source = new BitmapImage(new Uri("/img/bg/zwzp.jpg", UriKind.RelativeOrAbsolute));
            }
            else
            {
                common.BitmapImg image = SerializeObjectTostring.DeserializeObject(stu.Stuimage) as common.BitmapImg;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(image.Buffer);
                bitmap.EndInit();
                stuImg.Source = bitmap;
            }
        }
        /// <summary>
        /// 记录绑定学员
        /// </summary>
        public int StuId { get; set; }
    }
}
