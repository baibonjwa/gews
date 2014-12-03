// ******************************************************************
// 概  述：设置Farpoint通用默认属性类
// 作  者：杨小颖
// 创建日期：2014/03/08
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Drawing;

namespace LibCommon
{
    public static class FarpointDefaultPropertiesSetter
    {

        /// <summary>
        /// 设置管理界面Farpoint默认属性，注意：该函数仅适用于Farpoint第一列为“选择列”的表格
        /// </summary>
        /// <param name="fp">需要设置的Farpoint实例</param>
        /// <param name="title">Farpoint标题（常量中定义的标题），当传入为空字符串时，不对标题进行设置</param>
        /// <param name="frozenRowCnt">冻结行数（标题所占行数）</param>
        static public void SetFpDefaultProperties(FarPoint.Win.Spread.FpSpread fp, string title, int frozenRowCnt)
        {
            //默认行列数
            fp.ActiveSheet.RowCount = Const.FARPOINT_DEFAULT_ROW_COUNT;
            fp.ActiveSheet.ColumnCount = Const.FARPOINT_DEFAULT_COLUMN_COUNT;
            //行高不可调整
            fp.ActiveSheet.Rows.Default.Resizable = false;
            //列宽可调整（选择列除外），选择列需要在调用完该函数后将其Resizable属性设为false
            fp.ActiveSheet.Columns.Default.Resizable = true;
            //单元格水平居左和垂直居中
            fp.ActiveSheet.DefaultStyle.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            fp.ActiveSheet.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            //水平滚动条和竖直滚动条按需显隐
            fp.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            fp.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            //默认锁定单元格,“选择列”需要将Locked属性设置为false
            fp.ActiveSheet.DefaultStyle.Locked = true;
            //去掉拖拽对比
            fp.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            fp.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;
            //Farpoint标题
            if (title != "")
            {
                fp.ActiveSheet.Cells[0, 0].Text = title;
                fp.ActiveSheet.Cells[0, 0].Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Bold);
            }
            //设置冻结行数
            fp.ActiveSheet.FrozenRowCount = frozenRowCnt;
            //将“选择列"冻结
            fp.ActiveSheet.FrozenColumnCount = 1;
            //设置"选择列"属性
            fp.ActiveSheet.Columns[0].Locked = false;//可编辑
            fp.ActiveSheet.Columns[0].Resizable = false;//不可调整宽度
            //禁止列自动排序
            fp.ActiveSheet.SetColumnAllowAutoSort(0, fp.ActiveSheet.ColumnCount, false);

            //只允许选择行（禁用左上角SheetCorner全选功能）
            fp.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows | FarPoint.Win.Spread.SelectionBlockOptions.Cells | FarPoint.Win.Spread.SelectionBlockOptions.Columns;
            //只能选择一个单元
            fp.ActiveSheet.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
            //只能按照行选择
            fp.ActiveSheet.SelectionUnit =  FarPoint.Win.Spread.Model.SelectionUnit.Cell;
            //显示当前选中行
            fp.ActiveSheet.ShowRowSelector = true;
        }

        /// <summary>
        /// 设置Farpoint过滤功能，不符合过滤条件的行将被隐藏
        /// 注意：Farpoint中的冻结行的数据将不会被过滤；若未设置冻结行，冻结行的内容也会被添加到过滤条件当中！
        /// </summary>
        /// <param name="fp"></param>
        /// <param name="filterColumnIdxs">需要过滤的列的索引</param>
        static public void SetFpFilterHideProperties(FarPoint.Win.Spread.FpSpread fp, int[] filterColumnIdxs)
        {
            if (filterColumnIdxs == null || filterColumnIdxs.Length == 0)
            {
                return;
            }
            FarPoint.Win.Spread.HideRowFilter rowFilter = new FarPoint.Win.Spread.HideRowFilter(fp.ActiveSheet);
            //冻结行不进行过滤
            rowFilter.FilterFrozenRows = false;
            //非空行字符串
            rowFilter.NonBlanksString = Const.NONBLANK_STRING;
            //全部字符串，选择全部时过滤内容会没有符合条件的，因此ALL_STRING定义为“取消”
            rowFilter.AllString = Const.ALL_STRING;
            //空行字符串
            rowFilter.BlanksString = Const.BLANK_STRING;

            int fcLen = filterColumnIdxs.Length;
            string[] curFilters = new string[fcLen];

            for (int i = 0; i < fcLen; i++)
            {
                if (fp.ActiveSheet.RowFilter != null)
                {
                    curFilters[i] = fp.ActiveSheet.RowFilter.GetColumnFilterBy(filterColumnIdxs[i]);
                }
                FarPoint.Win.Spread.FilterColumnDefinition fcd = new FarPoint.Win.Spread.FilterColumnDefinition(filterColumnIdxs[i],
                    ~FarPoint.Win.Spread.FilterListBehavior.NonBlank & ~FarPoint.Win.Spread.FilterListBehavior.Blank);//取消显示“非空白行”和“空白行”过滤条件
                rowFilter.AddColumn(fcd);
            }

            fp.ActiveSheet.RowFilter = rowFilter;


            //按照先前选中的过滤方式过滤
            for (int i = 0; i < fcLen; i++)
            {
                if (curFilters[i] != null && curFilters[i].Length != 0)
                {
                    fp.ActiveSheet.AutoFilterColumn(filterColumnIdxs[i], curFilters[i], 0);
                }
            }
        }

        /// <summary>
        /// 设置Farpoint过滤功能，符合/不符合过滤条件的行将背景色将根据参数中设置的颜色进行显示
        /// </summary>
        /// <param name="fp"></param>
        /// <param name="fitColor">符合过滤条件的数据背景色</param>
        /// <param name="notFitColor">不符合过滤条件的数据背景色</param>
        /// <param name="filterColumnIdxs">需要过滤功能的列</param>
        static public void SetFpCustomFilterProperties(FarPoint.Win.Spread.FpSpread fp, Color fitColor, Color notFitColor, int[] filterColumnIdxs)
        {
            if (filterColumnIdxs == null || filterColumnIdxs.Length == 0)
            {
                return;
            }

            FarPoint.Win.Spread.NamedStyle inStyle = new FarPoint.Win.Spread.NamedStyle();
            FarPoint.Win.Spread.NamedStyle outStyle = new FarPoint.Win.Spread.NamedStyle();
            inStyle.BackColor = fitColor;
            outStyle.BackColor = notFitColor;
            //被过滤掉的行会按照自定义样式进行显示 
            FarPoint.Win.Spread.StyleRowFilter rowFilter = new FarPoint.Win.Spread.StyleRowFilter(fp.ActiveSheet, inStyle, outStyle);
            rowFilter.FilterFrozenRows = false;//冻结行不进行过滤

            //非空行字符串
            rowFilter.NonBlanksString = Const.NONBLANK_STRING;
            //全部字符串，选择全部时过滤内容会没有符合条件的，因此ALL_STRING定义为“取消”
            rowFilter.AllString = Const.ALL_STRING;
            //空行字符串
            rowFilter.BlanksString = Const.BLANK_STRING;


            int fcLen = filterColumnIdxs.Length;
            string[] curFilters = new string[fcLen];
            for (int i = 0; i < fcLen; i++)
            {
                curFilters[i] = fp.ActiveSheet.RowFilter.GetColumnFilterBy(filterColumnIdxs[i]);
                //第二个参数为取消显示“非空白行”过滤条件
                FarPoint.Win.Spread.FilterColumnDefinition fcd = new FarPoint.Win.Spread.FilterColumnDefinition(filterColumnIdxs[i],
                    ~FarPoint.Win.Spread.FilterListBehavior.NonBlank & ~FarPoint.Win.Spread.FilterListBehavior.Blank);//取消显示“非空白行”和“空白行”过滤条件
                rowFilter.AddColumn(fcd);
            }

            fp.ActiveSheet.RowFilter = rowFilter;

            //按照先前选中的过滤方式过滤
            for (int i = 0; i < fcLen; i++)
            {
                if (curFilters[i] != null && curFilters[i].Length != 0)
                {
                    fp.ActiveSheet.AutoFilterColumn(filterColumnIdxs[i], curFilters[i], 0);
                }
            }
        }
    }
}
