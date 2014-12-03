// ******************************************************************
// 概  述：设置Farpoint过滤类
// 作  者：杨小颖
// 创建日期：2014/03/27
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Drawing;

namespace LibCommon
{
    public static class FarpointFilterSetter
    {
        /// <summary>
        /// 清空Farpoint过滤条件
        /// </summary>
        /// <param name="fp"></param>
        static public void ClearFpFilter(FarPoint.Win.Spread.FpSpread fp)
        {
            if (fp.ActiveSheet.RowFilter != null)
            {
                fp.ActiveSheet.RowFilter.ResetFilter();//.ClearRowFilter();
            }
        }
    }
}
