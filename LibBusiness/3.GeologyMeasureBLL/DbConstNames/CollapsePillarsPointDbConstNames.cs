// ******************************************************************
// 概  述：陷落柱关键点数据库常名
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

namespace LibBusiness
{
    public class CollapsePillarsPointDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_COLLAPSE_PILLARS_POINT_INFO";

        //主键
        public const string ID = "ID";

        //坐标X
        public const string COORDINATE_X = "COORDINATE_X";

        //坐标Y
        public const string COORDINATE_Y = "COORDINATE_Y";

        //坐标Z
        public const string COORDINATE_Z = "COORDINATE_Z";

        //陷落柱ID
        public const string COLLAPSE_PILLARS_ID = "COLLAPSE_PILLARS_ID";

        //BID
        public const string BINDINGID = "BINDINGID";
    }
}
