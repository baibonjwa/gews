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

namespace LibCommon
{
    public class FarPointSortRange
    {
        /// <summary>
        /// FarPoint区域排序
        /// </summary>
        /// <param name="fpread">farpoint</param>
        /// <param name="index">列序号集合</param>
        /// <param name="rowDetailStartIndex">开始行号</param>
        /// <param name="rowsCount">排序行数</param>
        /// <param name="columnsCount">排序列数</param>
        public static void farpointSortRange(FarPoint.Win.Spread.FpSpread fpread,int []index,int rowDetailStartIndex,int rowsCount,int columnsCount)
        {
            fpread.ActiveSheet.Protect = false;
            FarPoint.Win.Spread.SortInfo[] sorter = new FarPoint.Win.Spread.SortInfo[index.Length];
            for (int i = 0; i < index.Length; i++)
            {
                sorter[i] = new FarPoint.Win.Spread.SortInfo(index[i], false, System.Collections.Comparer.Default);
            }
            fpread.ActiveSheet.SortRange(rowDetailStartIndex, index[0], rowDetailStartIndex + rowsCount, columnsCount, true, sorter);
        }
    }
}
