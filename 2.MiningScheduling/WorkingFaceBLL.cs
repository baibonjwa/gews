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

namespace LibBusiness
{
    public class WorkingFaceBLLNew
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
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNames.WORKINGFACE_ID + " = " + iWorkingFaceId +" ORDER BY "+WorkingFaceDbConstNamesNew.WORKINGFACE_ID+" DESC");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// <工作面>信息登录
        /// </summary>
        /// <param name="workingFaceEntity"><工作面>实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool insertWorkingFaceInfo(WorkingFaceEntityNew workingFaceEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("INSERT INTO " + WorkingFaceDbConstNamesNew.TABLE_NAME);
            sqlStr.Append(" (" + WorkingFaceDbConstNamesNew.WORKINGFACE_NAME);
            sqlStr.Append(", " + WorkingFaceDbConstNamesNew.MININGAREA_ID);
            sqlStr.Append(", " + WorkingFaceDbConstNamesNew.COORDINATE_X);
            sqlStr.Append(", " + WorkingFaceDbConstNamesNew.COORDINATE_Y);
            sqlStr.Append(", " + WorkingFaceDbConstNamesNew.COORDINATE_Z);
            sqlStr.Append(", " + WorkingFaceDbConstNamesNew.BID);
            sqlStr.Append(" )");
            sqlStr.Append(" VALUES (");
            sqlStr.Append("  '" + workingFaceEntity.WorkingFaceName + "'");
            sqlStr.Append(", '" + workingFaceEntity.MiningareaId + "'");
            sqlStr.Append(", '" + workingFaceEntity.CoordinateX + "'");
            sqlStr.Append(", '" + workingFaceEntity.CoordinateY + "'");
            sqlStr.Append(", '" + workingFaceEntity.CoordinateZ + "'");
            sqlStr.Append(", '" + workingFaceEntity.BID + "'");
            sqlStr.Append(" )");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            bool bResult = db.OperateDB(sqlStr.ToString());
            return bResult;
        }

        /// <summary>
        /// <工作面>信息修改
        /// </summary>
        /// <param name="workingFaceEntity"><工作面>实体</param>
        /// <returns>成功与否：true，false</returns>
        public static bool updateWorkingFaceInfo(WorkingFaceEntityNew workingFaceEntity)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + WorkingFaceDbConstNamesNew.TABLE_NAME);
            sqlStr.Append(" SET");
            sqlStr.Append("  " + WorkingFaceDbConstNamesNew.WORKINGFACE_NAME + " = '" + workingFaceEntity.WorkingFaceName + "'");
            sqlStr.Append(", " + WorkingFaceDbConstNamesNew.MININGAREA_ID + " = '" + workingFaceEntity.MiningareaId + "'");
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNamesNew.WORKINGFACE_ID + " = " + workingFaceEntity.WorkingfaceId);
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNamesNew.COORDINATE_X + " = " + workingFaceEntity.CoordinateX);
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNamesNew.COORDINATE_Y + " = " + workingFaceEntity.CoordinateY);
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNamesNew.COORDINATE_Z + " = " + workingFaceEntity.CoordinateZ);
            sqlStr.Append(" WHERE " + WorkingFaceDbConstNamesNew.BID + " = " + workingFaceEntity.BID);

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
    }
}
