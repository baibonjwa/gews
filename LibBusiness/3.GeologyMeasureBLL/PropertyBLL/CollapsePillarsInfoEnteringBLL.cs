// ******************************************************************
// 概  述：陷落柱业务逻辑
// 作  者：宋英杰
// 创建日期：2013/11/28
// 版本号：1.0
//         废弃
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibEntity;
using LibDatabase;

namespace LibBusiness
{
    public class CollapsePillarsInfoEnteringBLL
    {
        /// <summary>
        /// 陷落柱信息登录
        /// </summary>
        /// <param name="collapsePillarsEntity">陷落柱实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertCollapsePillarsInfo(CollapsePillarsEntity collapsePillarsEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO T_COLLAPSE_PILLARS (COLLAPSE_PILLAR_NAME, LOCATION_X, LOCATION_Y, LOCATION_Z, LONG_AXIS_LENGTH, SHORT_AXIAL_LENGTH, DESCRIBE)");
            sqlStr.Append("VALUES ('");
            sqlStr.Append(collapsePillarsEntity.CollapsePillarsName + "','");
            sqlStr.Append(collapsePillarsEntity.CollapsePillarsLocationX + "','");
            sqlStr.Append(collapsePillarsEntity.CollapsePillarsLocationY + "','");
            sqlStr.Append(collapsePillarsEntity.CollapsePillarsLocationZ + "','");
            sqlStr.Append(collapsePillarsEntity.CollapsePillarsLongAxisLength + "','");
            sqlStr.Append(collapsePillarsEntity.CollapsePillarsShortAxialLength + "','");
            sqlStr.Append(collapsePillarsEntity.CollapsePillarsDescribe + "')");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }
    }
}
