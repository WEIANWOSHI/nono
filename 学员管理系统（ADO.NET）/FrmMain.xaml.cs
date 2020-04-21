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
using System.Windows.Threading;
using System.Configuration;
using StudentsManagerModer.ObjExt;
using StudentsManagerModer;
using StudentManagerBLL;

namespace 学员管理系统_ADO.NET_
{
    /// <summary>
    /// FrmMain.xaml 的交互逻辑
    /// </summary>
    public partial class FrmMain : Window
    {
        //状态栏时间
        DispatcherTimer timer = null;
        public FrmMain()
        {
            InitializeComponent();
            //登录窗体验证
            FrmLogin login = new FrmLogin();
            login.ShowDialog();
            if (login.DialogResult != true)
            {
                Environment.Exit(0);
            }
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
            timer.Start();
            try
            {
                statusAdminLb.Content = "操作管理员【" + App.CurrentAdmin.AdminName + "】";
                statusVersionLb.Content = "版本号：" + ConfigurationManager.AppSettings["version"].ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string week = "星期天";
            switch (now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    week = "星期天";
                    break;
                case DayOfWeek.Monday:
                    week = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    week = "星期四";
                    break;
                case DayOfWeek.Friday:
                    week = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    week = "星期六";
                    break;
                default:
                    break;
            }
            statusTimeLb.Content = string.Format("{0}年{1}月{2}日    {3}:{4}:{5}    {6}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, week);
        }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 缩小窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// 添加学员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addsMenu_Click(object sender, RoutedEventArgs e)
        {
            StudentExt student = new StudentExt();
            view.FrmaddStuInfor updateStuInfor = new view.FrmaddStuInfor(student);
            updateStuInfor.ShowDialog();
        }
        /// <summary>
        /// 信息管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void smMenu_Click(object sender, RoutedEventArgs e)
        {
            Gird_Content.Children.Clear();
            view.FrmstuManger jser = new view.FrmstuManger();
            Gird_Content.Children.Add(jser);
        }
        /// <summary>
        /// 成绩录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void writesMenu_Click(object sender, RoutedEventArgs e)
        {
            //Gird_Content.Children.Clear();
            //ScoreListExt ext = new ScoreListExt();
            //view.FrmscorelistAdd jser = new view.FrmscorelistAdd(ext);
            //Gird_Content.Children.Add(jser);
        }
        /// <summary>
        /// 成绩分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checksMenu_Click(object sender, RoutedEventArgs e)
        {
            Gird_Content.Children.Clear();
            view.FrmScoreList jser = new view.FrmScoreList();
            Gird_Content.Children.Add(jser);
        }
        /// <summary>
        /// 成绩查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectsMenu_Click(object sender, RoutedEventArgs e)
        {
            Gird_Content.Children.Clear();
            view.scorelistFENGXI jser = new view.scorelistFENGXI();
            Gird_Content.Children.Add(jser);
        }
        /// <summary>
        /// 考勤查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queryaMenu_Click(object sender, RoutedEventArgs e)
        {
            Gird_Content.Children.Clear();
            view.FrmAttendance jser = new view.FrmAttendance();
            Gird_Content.Children.Add(jser);
        }
        /// <summary>
        /// 考勤打卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void adakaMenu_Click(object sender, RoutedEventArgs e)
        {
            
        }
        /// <summary>
        /// 链接网站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inlinehMenu_Click(object sender, RoutedEventArgs e)
        {
            Gird_Content.Children.Clear();
            view.FrmWebBrowser webBrowser = new view.FrmWebBrowser();
            Gird_Content.Children.Add(webBrowser);
        }
        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Escape)
                {
                    this.WindowState = WindowState.Minimized;
                }
                else if (e.Key == Key.S)
                {
                    xitong.IsSubmenuOpen = true;
                }
                else if (e.Key == Key.M)
                {
                    menuStuMan.IsSubmenuOpen = true;
                }
                else if (e.Key == Key.J)
                {
                    GREBD.IsSubmenuOpen = true;
                }
                else if (e.Key == Key.A)
                {
                    KAO.IsSubmenuOpen = true;
                }
                else if (e.Key == Key.Z)
                {
                    smMenu_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("快捷键无效") ;
            }
        }
        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void piliang_Click(object sender, RoutedEventArgs e)
        {
            Gird_Content.Children.Clear();
            view.FrmImportData importData = new view.FrmImportData();
            Gird_Content.Children.Add(importData);
        }
    }
}
