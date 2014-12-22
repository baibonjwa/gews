// ******************************************************************
// 概  述：
// 作  者：
// 创建日期：
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibCommon
{
    public class FileExport
    {
        public const string FILTER = "Excel 2007|*.xlsx|Excel 2003兼容|*.xls|PDF文档|*.pdf|TXT文档|*.txt";

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="fpread">farpoint</param>
        /// <param name="hideFirstColumn">是否隐藏第一列</param>
        /// <returns>是否导出成功</returns>
         public static bool fileExport(FarPoint.Win.Spread.FpSpread fpread,bool hideFirstColumn)
        {
            bool bResult = false;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = FILTER;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                if (fileName != "")
                {
                    //removeColumn();
                    if (hideFirstColumn)
                    {
                        fpread.Sheets[0].Columns[0].Visible = false;
                    }
                    fpread.ActiveSheet.Protect = false;
                    if (fileName.Substring(fileName.IndexOf('.') + 1) == "xlsx")
                    {
                        bResult = fpread.SaveExcel(fileName, FarPoint.Excel.ExcelSaveFlags.UseOOXMLFormat);
                    }
                    if (fileName.Substring(fileName.IndexOf('.') + 1) == "xls")
                    {
                        bResult = fpread.SaveExcel(fileName);
                    }
                    if (fileName.Substring(fileName.IndexOf('.') + 1) == "pdf")
                    {
                        FarPoint.Win.Spread.PrintInfo printset = new FarPoint.Win.Spread.PrintInfo();
                        printset.PrintToPdf = true;
                        printset.UseSmartPrint = true;
                        printset.PdfFileName = fileName;
                        fpread.Sheets[0].PrintInfo = printset;
                        fpread.PrintSheet(0);

                        bResult = printset.PrintToPdf;
                    }
                    if (fileName.Substring(fileName.IndexOf('.') + 1) == "txt")
                    {
                        bResult = fpread.ActiveSheet.SaveTextFile(fileName, FarPoint.Win.Spread.TextFileFlags.None);
                    }
                    fpread.ActiveSheet.Protect = true;
                    if (hideFirstColumn)
                    {
                        fpread.Sheets[0].Columns[0].Visible = true;
                    }
                    //addColumn();

                }
            }
            return bResult;
        }
    }
}
