using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using LibCommon;
using System.Windows.Forms;

namespace LibCommon
{
    public class FpUtil
    {
        //加载突出的图片
        private static Image _pngOrange        = Image.FromFile(Application.StartupPath + Const_WM.PICTURE_ORANGE);
        private static Image _pngRed           = Image.FromFile(Application.StartupPath + Const_WM.PICTURE_RED);
        private static Image _pngGreen         = Image.FromFile(Application.StartupPath + Const_WM.PICTURE_GREEN);
        private static Image _pngNot_Available = Image.FromFile(Application.StartupPath + Const_WM.PICTURE_NULL);
        private static Image _pngNull          = Image.FromFile(Application.StartupPath + Const_WM.PICTURE_NULL);

        /// <summary>
        /// 根据数据，给出对应的显示
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="value"></param>
        public static void setCellImg(FarPoint.Win.Spread.Cell cell, int value)
        {
            cell.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            cell.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            FarPoint.Win.Spread.CellType.ImageCellType imageCell = new FarPoint.Win.Spread.CellType.ImageCellType();
            imageCell.Style = FarPoint.Win.RenderStyle.Normal;
            cell.CellType = imageCell;

            if (value == (int)WarningResult.YELLOW)
            {
                cell.Value = _pngOrange;
            }
            else if (value == (int)WarningResult.RED)
            {
                cell.Value = _pngRed;
            }
            else if (value == (int)WarningResult.GREEN)
            {
                cell.Value = _pngGreen;
            }
            else if (value == (int)WarningResult.NOT_AVAILABLE)
            {
                cell.Value = _pngGreen;
            }
            else
            {
                cell.Value = _pngGreen;
            }
        }
    }
}
