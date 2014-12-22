// ******************************************************************
// 概  述：停采线业务逻辑
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
using LibEntity;
using LibDatabase;
using System.Data;

namespace LibBusiness
{
    public class StopLineBLL
    {
        /// <summary>
        /// 返回停采区所有信息
        /// </summary>
        /// <returns>停采区所有信息</returns>
        public static DataSet selectStopLineInfo()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + StopLineDbConstNames.TABLE_NAME;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 分页用返回停采区所有信息
        /// </summary>
        /// <returns>分页用停采区所有信息</returns>
        public static DataSet selectStopLineInfo(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + StopLineDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + StopLineDbConstNames.TABLE_NAME + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 查询停采线名称是否存在
        /// </summary>
        /// <param name="stopLineEntity">停采线实体</param>
        /// <returns>是否存在？是true：否false</returns>
        public static bool selectStopLineName(string stopLineName)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + StopLineDbConstNames.TABLE_NAME + " WHERE " + StopLineDbConstNames.STOP_LINE_NAME + " = '" + stopLineName+"'";
            DataSet ds = db.ReturnDS(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加停采线信息
        /// </summary>
        /// <param name="stopLineEntity">停采线实体</param>
        /// <returns>是否成功添加?true:false</returns>
        public static bool insertStopLineInfo(StopLine stopLineEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + StopLineDbConstNames.TABLE_NAME + " (");
            sb.Append(StopLineDbConstNames.STOP_LINE_NAME + ", ");
            sb.Append(StopLineDbConstNames.S_COORDINATE_X + ", ");
            sb.Append(StopLineDbConstNames.S_COORDINATE_Y + ", ");
            sb.Append(StopLineDbConstNames.S_COORDINATE_Z + ", ");
            sb.Append(StopLineDbConstNames.F_COORDINATE_X + ", ");
            sb.Append(StopLineDbConstNames.F_COORDINATE_Y + ",");
            sb.Append(StopLineDbConstNames.F_COORDINATE_Z + ", ");
            sb.Append(StopLineDbConstNames.BINDINGID);
            sb.Append(") VALUES ('");
            sb.Append(stopLineEntity.StopLineName + "',");
            sb.Append(stopLineEntity.SCoordinateX + ",");
            sb.Append(stopLineEntity.SCoordinateY + ",");
            sb.Append(stopLineEntity.SCoordinateZ + ",");
            sb.Append(stopLineEntity.FCoordinateX + ",");
            sb.Append(stopLineEntity.FCoordinateY + ",");
            sb.Append(stopLineEntity.FCoordinateZ + ",'");
            sb.Append(stopLineEntity.BindingID + "')");
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 更改停采线信息
        /// </summary>
        /// <param name="stopLineEntity">停采线实体</param>
        /// <returns>是否修改成功?true:false</returns>
        public static bool updateStopLineInfo(StopLine stopLineEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + StopLineDbConstNames.TABLE_NAME + " SET " + StopLineDbConstNames.STOP_LINE_NAME + " = '");
            sb.Append(stopLineEntity.StopLineName + "'," + StopLineDbConstNames.S_COORDINATE_X + " = ");
            sb.Append(stopLineEntity.SCoordinateX + "," + StopLineDbConstNames.S_COORDINATE_Y + " = ");
            sb.Append(stopLineEntity.SCoordinateY + "," + StopLineDbConstNames.S_COORDINATE_Z + " = ");
            sb.Append(stopLineEntity.SCoordinateZ + "," + StopLineDbConstNames.F_COORDINATE_X + " = ");
            sb.Append(stopLineEntity.FCoordinateX + "," + StopLineDbConstNames.F_COORDINATE_Y + " = ");
            sb.Append(stopLineEntity.FCoordinateY + "," + StopLineDbConstNames.F_COORDINATE_Z + " = ");
            sb.Append(stopLineEntity.FCoordinateZ + " WHERE ");
            sb.Append(StopLineDbConstNames.ID + " = " + stopLineEntity.ID);
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 删除停采线信息
        /// </summary>
        /// <param name="stoplineEntity">停采线实体</param>
        /// <returns>是否成功删除?true:false</returns>
        public static bool deleteStopLineInfo(StopLine stoplineEntity)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "DELETE FROM " + StopLineDbConstNames.TABLE_NAME + " WHERE " + StopLineDbConstNames.ID + " =" + stoplineEntity.ID;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }
    }
}
