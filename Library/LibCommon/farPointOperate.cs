// ******************************************************************
// 概  述：Farpoint操作共通类
// 作  者：宋英杰
// 创建日期：2014/3/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace LibCommon
{
    public class FarPointOperate
    {
        /// <summary>
        /// farpoint清空（绑定数据开始时调用）
        /// </summary>
        /// <param name="fp">farpoint控件名</param>
        /// <param name="frozenRowCounts">表头冻结行数</param>
        /// <param name="rowsCount">清空行数</param>
        public static void farPointClear(FarPoint.Win.Spread.FpSpread fp, int frozenRowCounts, int rowsCount)
        {
            fp.Sheets[0].Rows.Remove(frozenRowCounts, rowsCount);
        }
        /// <summary>
        /// farpoint重新绘制（在获取到数据行数后调用）
        /// </summary>
        /// <param name="fp">faropint控件名</param>
        /// <param name="frozenRowCounts">表头冻结行数</param>
        /// <param name="rowsCount">添加数据行数</param>
        public static void farPointReAdd(FarPoint.Win.Spread.FpSpread fp, int frozenRowCounts, int rowsCount)
        {
            fp.Sheets[0].Rows.Count = frozenRowCounts + rowsCount;
        }
        /// <summary>
        /// farpoint添加后焦点设置
        /// </summary>
        /// <param name="fp">faropint控件名</param>
        /// <param name="frozenRowCounts">表头冻结行数</param>
        /// <param name="rowsCount">添加数据行数</param>
        public static void farPointFocusSetAdd(FarPoint.Win.Spread.FpSpread fp,int frozenRowCounts,int rowsCount)
        {
            fp.Sheets[0].SetActiveCell(rowsCount + frozenRowCounts, 0);
        }
        /// <summary>
        /// farpoint修改后焦点设置
        /// </summary>
        /// <param name="fp">farpoint控件名</param>
        /// <param name="changeRowIndex">修改数据行号</param>
        public static void farPointFocusSetChange(FarPoint.Win.Spread.FpSpread fp,int changeRowIndex)
        {
            fp.Sheets[0].SetActiveCell(changeRowIndex, 0);
        }
        /// <summary>
        /// farpoint删除后焦点设置
        /// </summary>
        /// <param name="fp">farpoint控件名</param>
        /// <param name="frozenRowCounts">表头冻结行数</param>
        public static void farPointFocusSetDel(FarPoint.Win.Spread.FpSpread fp,int deleteRowIndex)
        {
            fp.Sheets[0].SetActiveCell(deleteRowIndex, 0);
        }
        /// <summary>
        /// farpoint行背景颜色设置（For循环中使用）
        /// </summary>
        /// <param name="fp">farpoint控件名</param>
        /// <param name="i">循环次数</param>
        /// <param name="frozenRowCounts">表头冻结行数</param>
        /// <param name="columnCounts">表列数</param>
        public static void farPointRowColorChange(FarPoint.Win.Spread.FpSpread fp, int loopIndex, int frozenRowCounts, int columnCounts,Color color)
        {
            for (int i = 0; i < columnCounts; i++)
            {
                fp.Sheets[0].Cells[frozenRowCounts + loopIndex, i].BackColor = color;
            }
        }

        /// <summary>
        /// farpoint行背景颜色设置
        /// </summary>
        /// <param name="fp">farpoint控件名</param>
        /// <param name="rowIndex">修改行号</param>
        /// <param name="startIndex">开始列编号</param>
        /// <param name="endIndex">结束列编号</param>
        /// <param name="color">背景颜色</param>
        public static void farPointRowBackColorChange(FarPoint.Win.Spread.FpSpread fp, int rowIndex,int startIndex,int endIndex, Color color)
        {
            for (int i = startIndex; i < endIndex+1 ; i++)
            {
                fp.Sheets[0].Cells[rowIndex,i].BackColor = color;
            }
        }

        /// <summary>
        /// 颜色交替
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color changeColor(Color color)
        {
            if (color == Color.White)
            {
                return Color.Silver;
            }
            return Color.White;
        }
    }
}
