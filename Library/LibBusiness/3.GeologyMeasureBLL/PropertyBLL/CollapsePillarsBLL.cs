// ******************************************************************
// 概  述：陷落柱业务逻辑
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
using System.Data;
using LibDatabase;
using LibEntity;

namespace LibBusiness
{
    public class CollapsePillarsBLL
    {
        /// <summary>
        /// 查询陷落柱数据
        /// </summary>
        /// <returns>陷落柱数据</returns>
        public static DataSet selectCollapsePillars()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM "+CollapsePillarsInfoDbConstNames.TABLE_NAME);
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }
        /// <summary>
        /// 分布用查询陷落柱数据
        /// </summary>
        /// <param name="iStartIndex">开始ID</param>
        /// <param name="iEndIndex">结束ID</param>
        /// <returns>区域间查询结果</returns>
        public static DataSet selectCollapsePillars(int iStartIndex, int iEndIndex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY "+CollapsePillarsInfoDbConstNames.ID+") AS rowid, * ");
            sb.Append("FROM "+CollapsePillarsInfoDbConstNames.TABLE_NAME+" ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 查询陷落柱名称是否存在
        /// </summary>
        /// <param name="collapsePillarsEnt">陷落柱实体</param>
        /// <returns>是否存在？是true:否false</returns>
        public static bool selectCollapseName(CollapsePillarsEnt collapsePillarsEnt)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT * FROM "+CollapsePillarsInfoDbConstNames.TABLE_NAME+" WHERE "+CollapsePillarsInfoDbConstNames.COLLAPSE_PILLARS+" = '"+collapsePillarsEnt.CollapsePillarsName+"'";
            DataSet ds = db.ReturnDS(sql);
            if(ds.Tables[0].Rows.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 20140509 lyf
        /// 根据陷落柱名称查询陷落柱ID（陷落柱绑定ID）
        /// </summary>
        /// <param name="collapsePillarsEnt">陷落柱实体</param>
        /// <returns>是否存在？是true:否false</returns>
        public static string selectCollapseIDByCollapseName(CollapsePillarsEnt collapsePillarsEnt)
        {
            string sID = "";
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "SELECT " + CollapsePillarsInfoDbConstNames.ID + " FROM " +
                CollapsePillarsInfoDbConstNames.TABLE_NAME + " WHERE " +
                CollapsePillarsInfoDbConstNames.COLLAPSE_PILLARS +
                " = '" + collapsePillarsEnt.CollapsePillarsName + "'";
            DataSet ds = db.ReturnDS(sql);

            sID = ds.Tables[0].Rows[0][0].ToString();
            return sID;
        }

        /// <summary>
        /// 查询最新插入的陷落柱数据
        /// </summary>
        /// <returns>陷落柱数据</returns>
        public static DataSet selectMaxCollapsePillars()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT * FROM "+CollapsePillarsInfoDbConstNames.TABLE_NAME+" WHERE ");
            sqlStr.Append(CollapsePillarsInfoDbConstNames.ID + " = ");
            sqlStr.Append("(SELECT MAX(" + CollapsePillarsInfoDbConstNames.ID + ") FROM ");
            sqlStr.Append(CollapsePillarsInfoDbConstNames.TABLE_NAME + ")");
            DataSet ds = db.ReturnDS(sqlStr.ToString());
            return ds;
        }

        /// <summary>
        /// 通过陷落柱编号查找关键点信息
        /// </summary>
        /// <param name="id">陷落柱ID</param>
        /// <returns>陷落柱下关键点</returns>
        public static DataSet selectCollapsePillarsPoint(int id)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ");
            sb.Append(CollapsePillarsPointDbConstNames.TABLE_NAME);
            sb.Append(" WHERE ");
            sb.Append(CollapsePillarsPointDbConstNames.COLLAPSE_PILLARS_ID);
            sb.Append(" = ");
            sb.Append(id);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 通过陷落柱编号查找关键点信息
        /// </summary>
        /// <param name="id">陷落柱关键点ID</param>
        /// <returns>关键点</returns>
        public static DataSet selectCollapsePillarsPointByPointID(int id)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ");
            sb.Append(CollapsePillarsPointDbConstNames.TABLE_NAME);
            sb.Append(" WHERE ");
            sb.Append(CollapsePillarsPointDbConstNames.ID);
            sb.Append(" = ");
            sb.Append(id);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 添加陷落柱信息
        /// </summary>
        /// <param name="collapsePillars">陷落柱实体</param>
        /// <returns>是否添加成功?true:false</returns>
        public static bool insertCollapsePillars(CollapsePillarsEnt collapsePillars)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + CollapsePillarsInfoDbConstNames.TABLE_NAME + "(");
            sb.Append(CollapsePillarsInfoDbConstNames.COLLAPSE_PILLARS + ",");
            sb.Append(CollapsePillarsInfoDbConstNames.DISCRIBE + ") VALUES ('");
            sb.Append(collapsePillars.CollapsePillarsName+"','");
            sb.Append(collapsePillars.Discribe+"')");
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }

        /// <summary>
        /// 修改陷落柱信息
        /// </summary>
        /// <param name="collapsePillarsEnt">陷落柱实体</param>
        /// <returns>是否成功修改?true:false</returns>
        public static bool updateCollapsePillars(CollapsePillarsEnt collapsePillarsEnt)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            db.Open();
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE " + CollapsePillarsInfoDbConstNames.TABLE_NAME + " SET " + CollapsePillarsInfoDbConstNames.COLLAPSE_PILLARS + " = '");
            sqlStr.Append(collapsePillarsEnt.CollapsePillarsName + "',"+CollapsePillarsInfoDbConstNames.DISCRIBE+" = '");
            sqlStr.Append(collapsePillarsEnt.Discribe + "' WHERE ");
            sqlStr.Append(CollapsePillarsInfoDbConstNames.ID + " = ");
            sqlStr.Append(collapsePillarsEnt.Id);
            bool bResult = db.OperateDBNotOpenAndClose(sqlStr.ToString());
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE " + CollapsePillarsPointDbConstNames.TABLE_NAME + " SET " + CollapsePillarsPointDbConstNames.COORDINATE_X + " = ");
            sb.Append(collapsePillarsEnt.CoordinateX + "," + CollapsePillarsPointDbConstNames.COORDINATE_Y + " = ");
            sb.Append(collapsePillarsEnt.CoordinateY + "," + CollapsePillarsPointDbConstNames.COORDINATE_Z + " = ");
            sb.Append(collapsePillarsEnt.CoordinateZ+" WHERE ");
            sb.Append(CollapsePillarsPointDbConstNames.ID + " = ");
            sb.Append(collapsePillarsEnt.PointId);
            bResult = db.OperateDBNotOpenAndClose(sb.ToString());
            db.Close();
            return bResult;
        }

        public static bool insertCollapsePillarsPoint(CollapsePillarsEnt collapsePillars)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + CollapsePillarsPointDbConstNames.TABLE_NAME + "(");
            sb.Append(CollapsePillarsPointDbConstNames.COORDINATE_X+ ",");
            sb.Append(CollapsePillarsPointDbConstNames.COORDINATE_Y + ",");
            sb.Append(CollapsePillarsPointDbConstNames.COORDINATE_Z + ",");
            sb.Append(CollapsePillarsPointDbConstNames.COLLAPSE_PILLARS_ID + ",");
            sb.Append(CollapsePillarsPointDbConstNames.BINDINGID+ ") VALUES ('");
            sb.Append(collapsePillars.CoordinateX + "','");
            sb.Append(collapsePillars.CoordinateY + "','");
            sb.Append(collapsePillars.CoordinateZ + "','");
            sb.Append(collapsePillars.Id + "','");
            sb.Append(collapsePillars.BindingID + "')");
            bool bResult = db.OperateDB(sb.ToString());
            return bResult;
        }
        
        /// <summary>
        /// 删除陷落柱信息
        /// </summary>
        /// <param name="collapsePillarsEnt">陷落柱实体</param>
        /// <returns>是否成功删除?true:false</returns>
        public static bool deleteCollapsePillars(CollapsePillarsEnt collapsePillarsEnt)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            db.Open();
            string sql = "DELETE FROM " + CollapsePillarsInfoDbConstNames.TABLE_NAME + " WHERE " + CollapsePillarsInfoDbConstNames.ID + " = " + collapsePillarsEnt.Id;
            bool bResult = db.OperateDBNotOpenAndClose(sql);
            sql = "DELETE FROM " + CollapsePillarsPointDbConstNames.TABLE_NAME + " WHERE " + CollapsePillarsPointDbConstNames.COLLAPSE_PILLARS_ID + " = " + collapsePillarsEnt.Id;
            bResult = db.OperateDBNotOpenAndClose(sql);
            db.Close();
            return bResult;
        }

        /// <summary>
        /// 删除关键点信息
        /// </summary>
        /// <param name="collapsePillarsEnt"></param>
        /// <returns></returns>
        public static bool deleteCollapsePillarsPoint(CollapsePillarsEnt collapsePillarsEnt)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            string sql = "DELETE FROM " + CollapsePillarsPointDbConstNames.TABLE_NAME + " WHERE " + CollapsePillarsPointDbConstNames.ID + " = " + collapsePillarsEnt.PointId;
            bool bResult = db.OperateDB(sql);
            return bResult;
        }
    }
}
