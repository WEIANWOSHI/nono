using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Common
{
    public class DataValidata
    {
        /// <summary>
        /// 验证正整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInteger(string str)
        {
            Regex reg = new Regex(@"^[1-9]\d*$");//只能为整数,*前出现零次或多次
            return reg.IsMatch(str);
        }
        /// <summary>
        /// 身份证验证
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>

        public static bool IsIdentity(string str)
        {
            Regex regex = new Regex(@"(^\d{ 18 }$)| (^\d{ 17} (\d | X | x)$)");
            return regex.IsMatch(str);
        }
        /// <summary>
        /// 电话验证
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPhone(string str)
        {
            Regex regex1 = new Regex(@"^1[345789]\\d{9}$");
            return regex1.IsMatch(str);
        }
        /// <summary>
        /// 日期格式验证
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool Data(int str)
        {
            Regex regex1 = new Regex(@"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
            return regex1.IsMatch(str.ToString());
        }
    }
}
