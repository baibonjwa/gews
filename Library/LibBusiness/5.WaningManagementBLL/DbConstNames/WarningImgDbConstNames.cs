using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibBusiness
{
    public class WarningImgDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_EARLY_WARNING_IMG";
        //图片文件名
        public const string IMG_FILENAME = "IMG_FILENAME";
        //图片预警ID
        public const string WARNING_ID = "WARNING_ID";
        //图片备注
        public const string REMARKS = "REMARKS";//如：断层、陷落柱等构造不需要考虑时间及巷道ID等约束条件。代码中转换为bool
        //图片
        public const string IMG = "IMG";//如：断层、陷落柱等构造不需要考虑时间及巷道ID等约束条件。代码中转换为bool
    }
}
