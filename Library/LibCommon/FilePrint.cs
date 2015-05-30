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
using System.Drawing;

namespace LibCommon
{
    public class FilePrint
    {
        static PrintDialog pd = new PrintDialog();
        /// <summary>
        /// farpoint打印
        /// </summary>
        /// <param name="fp"></param>
        /// <param name="index"></param>
        public static void CommonPrint(FarPoint.Win.Spread.FpSpread fp,int index)
        {
            try
            {
                FarPoint.Win.Spread.PrintInfo pi = new FarPoint.Win.Spread.PrintInfo();
                FarPoint.Win.Spread.PrintMargin pm = new FarPoint.Win.Spread.PrintMargin();

                pd = printerSet(fp);

                if (DialogResult.OK == pd.ShowDialog())
                {
                    pi.Preview = true;
                    pi.FirstPageNumber = 0;
                    pm.Top = pd.PrinterSettings.DefaultPageSettings.Margins.Top;
                    pm.Bottom = pd.PrinterSettings.DefaultPageSettings.Margins.Bottom;
                    pm.Left = pd.PrinterSettings.DefaultPageSettings.Margins.Left;
                    pm.Right = pd.PrinterSettings.DefaultPageSettings.Margins.Right;
                    pi.Margin = pm;
                    pi.PageStart = pd.PrinterSettings.FromPage;
                    pi.PageEnd = pd.PrinterSettings.ToPage;
                    pi.PaperSize = pd.PrinterSettings.DefaultPageSettings.PaperSize;
                    if (pd.PrinterSettings.DefaultPageSettings.Landscape)
                    {
                        pi.Orientation = FarPoint.Win.Spread.PrintOrientation.Landscape;
                    }
                    else
                    {
                        pi.Orientation = FarPoint.Win.Spread.PrintOrientation.Portrait;
                    }
                    fp.Sheets[0].PrintInfo = pi;
                    fp.PrintSheet(index);
                }
                
            }
            catch
            {
                MessageBox.Show("打印发生错误,请确认是否有连接好打印机");
            }
 
        }

        /// <summary>
        /// 默认打印机设置
        /// </summary>
        /// <param name="fp"></param>
        /// <returns></returns>
        public static PrintDialog printerSet(FarPoint.Win.Spread.FpSpread fp)
        {
            PrintDialog pd = new PrintDialog();
            pd.AllowSomePages = true;
            pd.AllowCurrentPage = true;
            pd.AllowPrintToFile = true;
            pd.AllowSelection = true;
            //pd.PrinterSettings.DefaultPageSettings.PaperSize.PaperName = "A4";
            pd.PrinterSettings.DefaultPageSettings.Margins.Top = 100;
            pd.PrinterSettings.DefaultPageSettings.Margins.Bottom = 100;
            pd.PrinterSettings.DefaultPageSettings.Margins.Left = 100;
            pd.PrinterSettings.DefaultPageSettings.Margins.Right = 100;
            pd.PrinterSettings.DefaultPageSettings.Landscape = true;
            pd.PrinterSettings.FromPage = 1;
            pd.PrinterSettings.ToPage = fp.Sheets.Count;

            return pd;
        }



    }
}
