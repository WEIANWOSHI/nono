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
using StudentManagerBLL;
using StudentsManagerModer.ObjExt;

namespace 学员管理系统_ADO.NET_.view
{
    /// <summary>
    /// FrmAttendance.xaml 的交互逻辑
    /// </summary>
    public partial class FrmAttendance : UserControl
    {
        public FrmAttendance()
        {
            InitializeComponent();
        }
        AttendanceManager attmar = new AttendanceManager();
        List<AttendanceExt> attpage = new List<AttendanceExt>();
        List<AttendanceExt> attendance_s;
        int pagesize = 14;  //每一页显示的数据
        int pageindex;  //当前页
        int countpage;  //总页
        private void Attendance_Loaded(object sender, RoutedEventArgs e)
        {
            attendance_s = attmar.GetAttendances();
            if (attendance_s == null) return;
            pageindex = 1;
            DtaGridpaging(attendance_s);
            FenyeCaozuo(attendance_s);
        }
        /// <summary>
        /// 划分页数
        /// </summary>
        /// <param name="stus"></param>
        private void DtaGridpaging(List<AttendanceExt> att)
        {
            if (att.Count % pagesize == 0)
            {
                countpage = att.Count / pagesize;
            }
            else
            {
                countpage = (att.Count / pagesize) + 1;
            }
            allPage.Text = "【共" + countpage + "页】";
        }
        /// <summary>
        /// 分页操作
        /// </summary>
        public void FenyeCaozuo(List<AttendanceExt> stutoclass)
        {
            attpage.Clear();       // 清空分页集合        
            // 拿到页数据 ，前一页的数据（跳过页数-1 ×（页/数据)）再拿到下一页的数据
            attpage = stutoclass.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            AttTable.ItemsSource = attpage;
            pagecount.Text = pageindex.ToString();
            page1.Text = string.Format("【第{0}页】", pageindex);
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_Name_Cardid_Click(object sender, RoutedEventArgs e)
        {
            int cardid = 0; // 考勤卡号
            List<AttendanceExt> attend; // 查找到的新集合
            if (Stu_Select.Text.Trim().Equals("")) return;
            if (int.TryParse(Stu_Select.Text, out cardid))
            {
                attendance_s = attendance_s.Where(s => s.CardNo.ToString().Contains(cardid.ToString())).ToList();
            }
            else
            {
                string str = Stu_Select.Text;
                attendance_s = attendance_s.Where(s => s.StudentName.Contains(str)).ToList();
            }
            if (attendance_s.Count == 0)
            {
                MessageBox.Show("无查找结果，请换个关键字进行查找", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DtaGridpaging(attendance_s);
            FenyeCaozuo(attendance_s);
            AttTable.ItemsSource = attendance_s;
        }
        /// <summary>
        /// 迟到学生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void late_stu_Click(object sender, RoutedEventArgs e)
        {
            string endtime = "09:00:00";
            attendance_s = attendance_s.Where(a => a.Notes == "上班打卡").ToList().Where(a => DateTime.Parse((a.DTime.Split(' '))[1]) > DateTime.Parse(endtime)).ToList();
            DtaGridpaging(attendance_s);
            FenyeCaozuo(attendance_s);
        }
        /// <summary>
        /// 早退学生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void early_stu_Click(object sender, RoutedEventArgs e)
        {
            string endtime = "18:00:00";
            attendance_s = attendance_s.Where(a => a.Notes == "下班打卡").ToList().Where(a => DateTime.Parse((a.DTime.Split(' '))[1]) < DateTime.Parse(endtime)).ToList();
            DtaGridpaging(attendance_s);
            FenyeCaozuo(attendance_s);
        }
        /// <summary>
        /// 日期搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void date_select_Click(object sender, RoutedEventArgs e)
        //{
        //    RefAttend();
        //    // 未选择时间，选择数据类型
        //    if (select_Time.SelectedDate == null)
        //    {
        //        switch (all_data.Text)
        //        {
        //            case "全部数据":
        //                Date_Att.Text = "【所有日期】/";
        //                AllData_Att.Text = "【全部数据】/";
        //                late_stu.IsEnabled = true;
        //                early_stu.IsEnabled = true;
        //                break;
        //            case "上班打卡":
        //                late_stu.IsEnabled = true;
        //                early_stu.IsEnabled = false;
        //                Date_Att.Text = "【所有日期】/";
        //                AllData_Att.Text = "【上班打卡】/";
        //                attendance_s = attendance_s.Where(a => a.Notes == "上班打卡").ToList();
        //                break;
        //            case "下班打卡":
        //                early_stu.IsEnabled = true;
        //                late_stu.IsEnabled = false;
        //                Date_Att.Text = "【所有日期】/";
        //                AllData_Att.Text = "【下班打卡】/";
        //                attendance_s = attendance_s.Where(a => a.Notes == "下班打卡").ToList();
        //                break;
        //        }
        //    }
        //    // 选择时间 
        //    else if (select_Time.SelectedDate != null)
        //    {
        //        switch (all_data.Text)
        //        {
        //            case "全部数据":
        //                Date_Att.Text = "【" + select_Time.SelectedDate.ToString().Split(' ')[0] + "】/";
        //                AllData_Att.Text = "【全部数据】/";
        //                late_stu.IsEnabled = true;
        //                early_stu.IsEnabled = true;
        //                attendance_s = attendance_s.Where(a => a.DTime.Split(' ')[0] == select_Time.SelectedDate.ToString().Split(' ')[0]).ToList();
        //                break;
        //            case "上班打卡":
        //                Date_Att.Text = "【" + select_Time.SelectedDate.ToString().Split(' ')[0] + "】/";
        //                late_stu.IsEnabled = true;
        //                early_stu.IsEnabled = false;
        //                AllData_Att.Text = "【上班打卡】/";

        //                attendance_s = attendance_s.Where(a => a.DTime.Split(' ')[0] == select_Time.SelectedDate.ToString().Split(' ')[0]).ToList().Where(a => a.Notes == "上班打卡").ToList();
        //                break;
        //            case "下班打卡":
        //                Date_Att.Text = "【" + select_Time.SelectedDate.ToString().Split(' ')[0] + "】/";
        //                early_stu.IsEnabled = true;
        //                late_stu.IsEnabled = false;
        //                AllData_Att.Text = "【下班打卡】/";
        //                attendance_s = attendance_s.Where(a => a.DTime.Split(' ')[0] == select_Time.SelectedDate.ToString().Split(' ')[0]).ToList().Where(a => a.Notes == "下班打卡").ToList();
        //                break;
        //        }
        //    }
        //    DtaGridpaging(attendance_s);
        //    FenyeCaozuo(attendance_s);
        //}
        /// <summary>
        /// 全部数据
        /// </summary>
        private void RefAttend()
        {
            attendance_s = attmar.GetAttendances();
            if (attendance_s == null) return;
            AttTable.ItemsSource = attendance_s;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Attendance.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 上一页，下一页，前往页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uppage_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case "uppage":  // 上一页
                    if (pageindex == 1)
                    {
                        FenyeCaozuo(attendance_s);
                    }
                    else
                    {
                        pageindex -= 1;
                        FenyeCaozuo(attendance_s);
                    }
                    break;
                case "downpage":    // 下一页
                    if (pageindex >= countpage)
                    {
                        FenyeCaozuo(attendance_s);
                    }
                    else
                    {
                        pageindex += 1;
                        FenyeCaozuo(attendance_s);
                    }
                    break;
                case "gopage":  // 前进到第几页
                    int index;
                    if (!int.TryParse(pagecount.Text, out index))
                    {
                        MessageBox.Show("页数为纯数字");
                        pagecount.Clear();
                        pagecount.Text = pageindex.ToString();
                        return;
                    }
                    if (index > countpage) // 输入页数大于总页
                    {
                        MessageBox.Show("没有该页数，请重新输入！", "管理系统");
                        pagecount.Text = pageindex.ToString();
                    }
                    else
                    {
                        pageindex = index;   // 当前页数等于文本框输入的页数
                        FenyeCaozuo(attendance_s);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
