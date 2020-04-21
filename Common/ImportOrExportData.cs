﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace Common
{
    /// <summary>
    /// 数据化Excel工作表的导出
    /// </summary>
    public class ImportOrExportData
    {
        /// <summary>
        /// Excel数据导出
        /// </summary>
        /// <param name="dataTable">数据源</param>
        /// <param name="path">目标地址</param>
        /// <returns></returns>
        public static bool ExportToExcel(System.Data.DataTable dataTable, string path)
        {
            //第一步：创建一个Excel连接，实例化一个从当前应用程序到Office办公软件中Excel的一个接口
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            //第二步：创建一个空的工作簿
            Workbook excelWb = excelApp.Workbooks.Add(System.Type.Missing);
            //第三步：创建一个工作表
            Worksheet excelSheet = excelWb.Sheets[1];
            try
            {
                excelSheet.Name = "学生信息表";
                excelSheet.Cells[1, 1] = "学号";
                excelSheet.Cells[1, 2] = "姓名";
                excelSheet.Cells[1, 3] = "性别";
                excelSheet.Cells[1, 4] = "生日";
                excelSheet.Cells[1, 5] = "身份证号";
                excelSheet.Cells[1, 6] = "打卡号";
                excelSheet.Cells[1, 7] = "年龄";
                excelSheet.Cells[1, 8] = "电话号";
                excelSheet.Cells[1, 9] = "地址";
                excelSheet.Cells[1, 10] = "班级";
                excelSheet.Cells.NumberFormat = "@";
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        excelSheet.Cells[i + 2, j + 1] = dataTable.Rows[i][j].ToString();
                    }
                }
                excelSheet.Columns.AutoFit();
                excelWb.SaveAs(path);
                excelWb.Close();
                excelApp.Quit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取导入数据表的第一张表名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string SheetName(string path)
        {
            Microsoft.Office.Interop.Excel.Application _excelApp = new Microsoft.Office.Interop.Excel.Application();
            if (_excelApp == null)
            {
                return null;
            }
            Workbooks _books = _excelApp.Workbooks;
            Workbook _book = _books.Add(path);
            Sheets _sheets = _book.Sheets;
            Worksheet _sheet = (Worksheet)_sheets.get_Item(1);
            string sheetName = _sheet.Name;
            ReleaseCOM(_sheet);
            ReleaseCOM(_sheets);
            ReleaseCOM(_book);
            ReleaseCOM(_books);
            _excelApp.Quit();
            ReleaseCOM(_excelApp);
            return sheetName;
        }
        /// <summary>
        /// 释放COM对象
        /// </summary>
        /// <param name="pObj"></param>
        private static void ReleaseCOM(object pObj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pObj);
            }
            catch
            {
                throw new Exception("释放资源时发生错误！");
            }
            finally
            {
                pObj = null;
            }
        }
    }
}
