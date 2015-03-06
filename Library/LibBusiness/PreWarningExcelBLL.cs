using System;
using System.Data;
using LibCommon;
using LibEntity;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace LibBusiness
{
    public class PreWarningExcelBLL
    {
        private static string _excelFilePath =
            System.Windows.Forms.Application.StartupPath + "\\" +
            "PreWarningRules.xlsx";

        public static bool ImportExcelRules2Db(string excelPath)
        {
            string[] sheetNames =
                PreWarningExcelBLL.GetExcelSheetNames(excelPath);
            if (sheetNames != null)
            {
                PreWarningRulesBLL.ClearPreWarningDB();
                int n = sheetNames.Length;
                for (int i = 0; i < n; i++)
                {
                    DataSet dsSheet =
                        LibExcelHelper.importExcelSheetToDataSet(excelPath,
                        sheetNames[i]);
                    if (dsSheet == null)
                    {
                        return false;
                    }
                    for (int j = 0; j < dsSheet.Tables[0].Rows.Count; j++)
                    {
                        string errorMsg = "";
                        PreWarningRules ent =
                            PreWarningExcelBLL.ConvertExcelDataRow2PreWarningEntity(dsSheet.Tables[0].Rows[j],
                            out errorMsg);
                        if (errorMsg != "")
                        {
                            if (DialogResult.Yes ==
                                MessageBox.Show("导入Excel预警规则失败：" + errorMsg +
                                "\r\n是否继续？", "Sheet：" + sheetNames[i] + " 行:" +
                                (j + 1).ToString(), MessageBoxButtons.YesNo))
                            {
                                continue;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        if
                            (!PreWarningRulesBLL.InsertPreWarningRulesInfo(ent))
                        {
                            if (DialogResult.Yes ==
                                MessageBox.Show("写入Excel预警规则至数据库失败!\r\n是否继续？",
                                "Sheet：" + sheetNames[i] + " 行:" + (j +
                                1).ToString(), MessageBoxButtons.YesNo))
                            {
                                continue;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 获取Excel中所有的Sheet名
        /// 注：可能返回null
        /// </summary>
        /// <returns></returns>
        public static string[] GetExcelSheetNames(string excelPath)
        {
            string[] ret = null;
            Excel.Application objExcelApp = null;
            Excel.Workbooks objExcelWorkBooks = null;
            Excel.Workbook objExcelWorkBook = null;

            try
            {
                objExcelApp = new Excel.ApplicationClass();
                objExcelWorkBooks = objExcelApp.Workbooks;
                objExcelWorkBook = objExcelWorkBooks.Open(excelPath, 0,
                    true, 5, "", "", true,
                    Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                        true, false, 0, true, false, false);
                int n = objExcelWorkBook.Sheets.Count;
                ret = new string[n];
                //注：Excel索引是从1开始的
                for (int i = 1; i <= n; i++)
                {
                    Excel.Worksheet objExcelWorkSheet =
                        (Worksheet)objExcelWorkBook.Worksheets[i]; //(Worksheet)objExcelWorkBook.Sheets[i];
                    ret[i - 1] = objExcelWorkSheet.Name;
                }
                objExcelApp.DisplayAlerts = false;
            }
            catch (Exception ex)
            {
                Alert.alert("该Excel文件的工作表的名字不正确," + ex.Message);
            }
            finally
            {
                if (objExcelApp != null)
                {
                    objExcelApp.Quit();
                    GC.Collect();//不加该行代码Excel不会退出！
                }
            }
            return ret;
        }

        /// <summary>
        /// 将从Excel读取出的一行数据转换为预警规则实体
        /// 注意：从Excel中读取出来的数据不能使用列名，只能使用索引！
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="success">是否处理成功</param>
        /// <returns></returns>
        public static PreWarningRules
            ConvertExcelDataRow2PreWarningEntity(DataRow dr, out string
            errorMsg)
        {
            PreWarningRules ret = new PreWarningRules();
            errorMsg = "";
            //MARK FIELD注意：由于数据是从Excel中读取出来的，因此不能使用列名，只能使用索引！
            try
            {
                if (dr[0].ToString().Trim() == "")
                {
                    errorMsg =
                        "预警规则Excel表中规则编码为空，请检查并删除Excel表中的空行等无效数据！";//删除方法：光标移至数据的最后一行，然后按Ctrl+Shift+End选择空行数据，右键鼠标-删除
                    return null;
                }
                //规则编码
                ret.RuleCode = dr[0].ToString();
                //规则类别
                ret.RuleType = dr[1].ToString();
                //预警类型
                ret.WarningType = dr[2].ToString();
                //预警级别
                ret.WarningLevel = dr[3].ToString();
                //适用位置
                ret.SuitableLocation = dr[4].ToString();
                //规则描述
                ret.RuleDescription = dr[5].ToString();
                //指标类型
                ret.IndicatorType = dr[6].ToString();
                //比较符
                ret.Operator = dr[7].ToString();
                //修改日期
                try
                {
                    ret.ModifyDate = Convert.ToDateTime(dr[8]);
                }
                catch
                {
                    errorMsg = "预警规则Excel表中指定的修改日期格式不正确，将使用当前日期！【规则编码:" +
                        dr[0].ToString() + "】";
                    ret.ModifyDate = DateTime.Now;
                }
                //备注
                ret.Remarks = dr[9].ToString();

                //绑定数据库表名称
                ret.BindingTableName = dr[10].ToString();

                //绑定数据库表字段名称
                ret.BindingColumnName = dr[11].ToString();

                //字段使用方式
                ret.UseType = dr[12].ToString();

                //组合规则绑定的单一规则，瑞该规则为单一规则，则该项为空
                ret.StrBindingSingleRuleName = dr[14].ToString();
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return null;
            }
            return ret;
        }

        /// <summary>
        /// ！！！！！！！！！！！！！！！！！！！
        /// 注意：该函数存在问题！未进行完善
        /// 写入excel数据
        /// </summary>
        /// <param name="ent">预警规则实体</param>
        /// <param name="selectedRowID">选择行编号</param>
        /// <returns>是否插入成功?true:false</returns>
        public static bool updateInfoToExcelSheet(PreWarningRules ent, int
            selectedRowID, string sheetName)
        {
            bool ret = false;
            Excel.Application objExcelApp = null;
            Excel.Workbooks objExcelWorkBooks = null;
            Excel.Workbook objExcelWorkBook = null;
            Excel.Worksheet objExcelWorkSheet = null;

            try
            {
                objExcelApp = new Excel.ApplicationClass();
                objExcelWorkBooks = objExcelApp.Workbooks;
                objExcelWorkBook = objExcelWorkBooks.Open(_excelFilePath, 0,
                    false, 5, "", "", true,
                    Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                        true, false, 0, true, false, false);
                objExcelWorkSheet =
                    (Worksheet)objExcelWorkBook.Worksheets[sheetName];
                objExcelWorkSheet.Select(Type.Missing);

                Worksheet objExcelWorkSheetTemp =
                    (Worksheet)objExcelApp.ActiveSheet;
                objExcelWorkSheetTemp.Cells[selectedRowID + 1, 1] =
                    ent.RuleCode;
                objExcelWorkSheetTemp.Cells[selectedRowID + 1, 2] =
                    ent.RuleType;
                objExcelWorkSheetTemp.Cells[selectedRowID + 1, 3] =
                    ent.WarningType;
                objExcelWorkSheetTemp.Cells[selectedRowID + 1, 4] =
                    ent.WarningLevel;
                objExcelWorkSheetTemp.Cells[selectedRowID + 1, 5] =
                    ent.SuitableLocation;
                objExcelWorkSheetTemp.Cells[selectedRowID + 1, 6] =
                    ent.RuleDescription;
                objExcelWorkSheetTemp.Cells[selectedRowID + 1, 7] =
                    ent.IndicatorType;
                objExcelWorkSheetTemp.Cells[selectedRowID + 1, 8] =
                    ent.Operator;
                objExcelWorkSheetTemp.Cells[selectedRowID + 1, 9] =
                    ent.ModifyDate.ToString();
                objExcelWorkSheetTemp.Cells[selectedRowID + 1, 10] =
                    ent.Remarks;//MARK FIELD
                //objExcelApp.Visible = true;
                objExcelApp.DisplayAlerts = false;
                objExcelApp.SaveWorkspace();
                ret = true;
            }
            catch (Exception ex)
            {
                Alert.alert(ex.Message);
                ret = false;
            }
            finally
            {
                if (objExcelApp != null)
                {
                    objExcelApp.Quit();
                    GC.Collect();//不加该行代码Excel不会退出！
                }
            }
            return ret;
        }

    }
}
