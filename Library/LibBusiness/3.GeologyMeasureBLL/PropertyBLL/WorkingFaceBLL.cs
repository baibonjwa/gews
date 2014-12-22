// ******************************************************************
// 概  述：工作面业务逻辑
// 作  者：伍鑫
// 创建日期：2014/02/25
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibDatabase;
using System.Data;
using LibEntity;
using LibCommon;

namespace LibBusiness
{
    public class WorkingFaceBLL
    {
        /// <summary>
        /// 通过<采区编号>，获取该<采区>下所有<工作面>信息
        /// </summary>
        /// <returns><工作面>信息</returns>
        public static DataSet selectWorkingFaceInfoByMiningAreaId(int iMiningAreaId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + WorkingFaceDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNames.MININGAREA_ID + " = " + iMiningAreaId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 通过<采区编号>，获取该<采区>下所有<工作面>信息
        /// </summary>
        /// <returns><工作面>信息</returns>
        public static DataSet selectWorkingFaceInfoByMiningAreaIdAndWorkingfaceType(int iMiningAreaId, WorkingfaceTypeEnum[] workingfaceType)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + WorkingFaceDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNames.MININGAREA_ID + " = " + iMiningAreaId);
            sqlStr.Append(" AND (");
            foreach (WorkingfaceTypeEnum t in workingfaceType)
            {
                sqlStr.Append(WorkingFaceDbConstNames.WORKINGFACE_TYPE + " = " + (int)t + " OR ");
            }
            string str = sqlStr.ToString();
            str = str.Substring(0, str.Length - 3);
            str += ")";

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(str);
            return ds;
        }


        /// <summary>
        /// 通过<工作面>，获取<工作面>信息
        /// </summary>
        /// <returns><工作面>信息</returns>
        public static DataSet selectWorkingFaceInfoByWorkingFaceId(int iWorkingFaceId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + WorkingFaceDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNames.WORKINGFACE_ID + " = " + iWorkingFaceId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 通过<工作面>，获取<工作面>信息
        /// </summary>
        /// <returns><工作面>信息</returns>
        public static DataSet selectWorkingFaceInfoByWorkingFaceIdOrderBy(int iWorkingFaceId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + WorkingFaceDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNames.WORKINGFACE_ID + " = " + iWorkingFaceId + " ORDER BY " + WorkingFaceDbConstNames.WORKINGFACE_ID + " DESC");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// <工作面>信息登录
        /// </summary>
        /// <param name="workingFaceEntity"><工作面>实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertWorkingFaceInfo(WorkingFace workingFaceEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + WorkingFaceDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + WorkingFaceDbConstNames.WORKINGFACE_NAME);
            sqlStr.Append(", " + WorkingFaceDbConstNames.MININGAREA_ID);
            sqlStr.Append(", " + WorkingFaceDbConstNames.COORDINATE_X);
            sqlStr.Append(", " + WorkingFaceDbConstNames.COORDINATE_Y);
            sqlStr.Append(", " + WorkingFaceDbConstNames.COORDINATE_Z);
            sqlStr.Append(", " + WorkingFaceDbConstNames.START_DATE + ",");
            sqlStr.Append(", " + WorkingFaceDbConstNames.IS_FINISH + ",");
            sqlStr.Append(", " + WorkingFaceDbConstNames.STOP_DATE + ",");
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + workingFaceEntity.WorkingFaceName + "'");
            sqlStr.Append(", '" + workingFaceEntity.MiningArea.MiningAreaId + "'");
            sqlStr.Append(", '" + workingFaceEntity.Coordinate.X + "'");
            sqlStr.Append(", '" + workingFaceEntity.Coordinate.Y + "'");
            sqlStr.Append(", '" + workingFaceEntity.Coordinate.Z + "'");
            sqlStr.Append(", '" + workingFaceEntity.StartDate + "','");
            sqlStr.Append(", '" + workingFaceEntity.IsFinish + "','");
            sqlStr.Append(", '" + workingFaceEntity.StopDate + "','");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// <工作面>信息登录
        /// </summary>
        /// <param name="workingFaceEntity"><工作面>实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertWorkingFaceBasicInfo(string workingFaceName, int miningAreaId, int workingfaceType)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + WorkingFaceDbConstNames.TABLE_NAME);
            sqlStr.Append(" (" + WorkingFaceDbConstNames.WORKINGFACE_NAME);
            sqlStr.Append(", " + WorkingFaceDbConstNames.MININGAREA_ID);
            sqlStr.Append(", " + WorkingFaceDbConstNames.WORKINGFACE_TYPE);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + workingFaceName + "'");
            sqlStr.Append(", '" + miningAreaId + "'");
            sqlStr.Append(", '" + workingfaceType + "'");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 工作面x,y,z信息修改
        /// </summary>
        /// <param name="workingFaceEntity"><工作面>实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateWorkingfaceXYZ(WorkingFace workingFaceEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + WorkingFaceDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET ");
            sqlStr.Append(WorkingFaceDbConstNames.COORDINATE_X + " = '" + workingFaceEntity.Coordinate.X + "'");
            sqlStr.Append(", " + WorkingFaceDbConstNames.COORDINATE_Y + " = '" + workingFaceEntity.Coordinate.Y + "'");
            sqlStr.Append(", " + WorkingFaceDbConstNames.COORDINATE_Z + " = '" + workingFaceEntity.Coordinate.Z + "'");
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNames.WORKINGFACE_ID + " = " + workingFaceEntity.WorkingFaceID);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// <工作面>基础信息修改
        /// </summary>
        /// <param name="workingFaceEntity"><工作面>实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateWorkingFaceBasicInfo(int workingFaceId, string workingFaceName, int miningAreaId, int workingfaceType)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + WorkingFaceDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + WorkingFaceDbConstNames.WORKINGFACE_NAME + " = '" + workingFaceName + "'");
            sqlStr.Append(", " + WorkingFaceDbConstNames.MININGAREA_ID + " = " + miningAreaId);
            sqlStr.Append(", " + WorkingFaceDbConstNames.WORKINGFACE_TYPE + " = " + workingfaceType);
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNames.WORKINGFACE_ID + " = " + workingFaceId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// <工作面>信息修改
        /// </summary>
        /// <param name="workingFaceEntity"><工作面>实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateWorkingFaceInfo(WorkingFace workingFaceEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + WorkingFaceDbConstNames.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + WorkingFaceDbConstNames.WORKINGFACE_NAME + " = '" + workingFaceEntity.WorkingFaceName + "'");
            sqlStr.Append(", " + WorkingFaceDbConstNames.MININGAREA_ID + " = " + workingFaceEntity.MiningArea.MiningAreaId);
            sqlStr.Append(" , " + WorkingFaceDbConstNames.COORDINATE_X + " = " + workingFaceEntity.Coordinate == null ? Const.DOUBLE_ZERO : workingFaceEntity.Coordinate.X);
            sqlStr.Append(" , " + WorkingFaceDbConstNames.COORDINATE_Y + " = " + workingFaceEntity.Coordinate == null ? Const.DOUBLE_ZERO : workingFaceEntity.Coordinate.Y);
            sqlStr.Append(" , " + WorkingFaceDbConstNames.COORDINATE_Z + " = " + workingFaceEntity.Coordinate == null ? Const.DOUBLE_ZERO : workingFaceEntity.Coordinate.Z);
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNames.WORKINGFACE_ID + " = " + workingFaceEntity.WorkingFaceID);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// <工作面>信息删除
        /// </summary>
        /// <param name="iWorkingFaceId">删除数据主键</param>
        /// <returns>成功与否：true，false</returns>
        public static bool deleteWorkingFaceInfo(int iWorkingFaceId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("DELETE FROM " + WorkingFaceDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNames.WORKINGFACE_ID + " = " + iWorkingFaceId);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// 获取所有工作面信息
        /// </summary>
        /// <returns></returns>
        public static DataSet selectAllWorkingFace()
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + WorkingFaceDbConstNames.TABLE_NAME);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            return db.ReturnDS(sqlStr.ToString());
        }

        /// <summary>
        /// 删除工作面信息
        /// </summary>
        /// <param name="bid">id键值</param>
        /// <returns></returns>
        public static bool deleteWorkingFaceInfoById(string id)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("DELETE FROM " + WorkingFaceDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNames.WORKINGFACE_ID + " = " + id);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        private static DataSet selectOnlyWorkingFaceInfoByID(int workingFaceID)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + WorkingFaceDbConstNames.TABLE_NAME + " WHERE " + WorkingFaceDbConstNames.WORKINGFACE_ID + " = " + workingFaceID;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 通过<工作面>，获取<工作面>信息
        /// </summary>
        /// <returns><工作面>信息</returns>
        public static WorkingFace selectWorkingFaceInfoByWksId(int iWorkingFaceId)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM " + WorkingFaceDbConstNames.TABLE_NAME);
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNames.WORKINGFACE_ID + " = " + iWorkingFaceId + " ORDER BY " + WorkingFaceDbConstNames.WORKINGFACE_ID + " DESC");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            WorkingFace workingfaceEntity = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    workingfaceEntity = new WorkingFace();
                    double x = dr[WorkingFaceDbConstNames.COORDINATE_X] == DBNull.Value ? 0.0 : Convert.ToDouble(dr[WorkingFaceDbConstNames.COORDINATE_X]);
                    double y = dr[WorkingFaceDbConstNames.COORDINATE_Y] == DBNull.Value ? 0.0 : Convert.ToDouble(dr[WorkingFaceDbConstNames.COORDINATE_Y]);
                    double z = dr[WorkingFaceDbConstNames.COORDINATE_Z] == DBNull.Value ? 0.0 : Convert.ToDouble(dr[WorkingFaceDbConstNames.COORDINATE_Z]);
                    workingfaceEntity.Coordinate = new Coordinate(x, y, z);

                    //workingfaceEntity.MiningAreaID = (int)ds.Tables[0].Rows[0][WorkingFaceDbConstNames.MININGAREA_ID];
                    workingfaceEntity.WorkingFaceID = (int)ds.Tables[0].Rows[0][WorkingFaceDbConstNames.WORKINGFACE_ID];
                    workingfaceEntity.WorkingFaceName = ds.Tables[0].Rows[0][WorkingFaceDbConstNames.WORKINGFACE_NAME].ToString();
                }
                catch
                {
                    return null;
                }
            }
            return workingfaceEntity;
        }

        public static bool CheckIsExist(int WorkingFaceID, string WorkTimeName, DateTime CreateTime)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM " + DayReportHCDbConstNames.TABLE_NAME + " WHERE " + DayReportHCDbConstNames.WORKINGFACE_ID + " = " + WorkingFaceID;
            sql += " AND " + DayReportHCDbConstNames.WORK_TIME + " = '" + WorkTimeName + "'";
            sql += " AND " + DayReportHCDbConstNames.DATETIME + " = '" + CreateTime + "'";
            DataSet ds = db.ReturnDS(sql);

            if ((ds == null) || (ds.Tables == null))
            {
                //为空时进行处理
                return true;
            }
            else
            {
                //不为空时处理
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 删除工作面信息, 其实是解除关联
        /// </summary>
        /// <param name="bid">bid键值</param>
        /// <returns></returns>
        public static bool deleteHCWorkingFace(WorkingFace entity)
        {
            string sql = " BEGIN ";

            foreach (Tunnel tEntity in entity.tunnelSet)
            {
                sql += "UPDATE " + TunnelInfoDbConstNames.TABLE_NAME +
                      " SET TUNNEL_TYPE=" + (int)TunnelTypeEnum.OTHER + " WHERE " + TunnelInfoDbConstNames.ID + " = " + tEntity.TunnelId;
            }
            sql += " END ";

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sql);
            return bResult;
        }
    }
}
