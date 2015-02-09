// ******************************************************************
// 概  述：巷道信息业务逻辑
// 作  者：宋英杰
// 日  期：2013/11/28
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Text;
using System.Data;
using LibEntity;
using LibDatabase;

namespace LibBusiness
{
    public class TunnelInfoBLL
    {

        /// <summary>
        /// 查询掘进面巷道数据
        /// </summary>
        /// <returns></returns>
        public static DataSet selectTunnelJJ()
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT * FROM " + TunnelInfoDbConstNames.TABLE_NAME + " WHERE TUNNEL_TYPE=" + (int)TunnelTypeEnum.TUNNELLING;
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 返回掘进巷道所有信息
        /// </summary>
        /// <returns></returns>
        public static DataSet selectTunnelJJ(int iStartIndex, int iEndIndex)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + TunnelInfoDbConstNames.ID + ") AS rowid, * ");
            sb.Append("FROM " + TunnelInfoDbConstNames.TABLE_NAME + " WHERE " +
                TunnelInfoDbConstNames.TUNNEL_TYPE + "=" + (int)TunnelTypeEnum.TUNNELLING + " ) AS TB ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append(" AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 分页用返回回采巷道所有信息
        /// </summary>
        /// <returns></returns>
        public static DataSet selectTunnelHC(int iStartIndex, int iEndIndex)
        {
            //            SELECT ROW_NUMBER() OVER (ORDER BY WORKINGFACE_ID) AS rowid ,*FROM
            //(
            //SELECT  
            //DISTINCT B.*
            //  FROM 
            //  T_TUNNEL_INFO AS A,
            //  T_WORKINGFACE_INFO AS B
            //   WHERE 
            //   TUNNEL_TYPE IN (0, 1, 2,3) AND A.WORKINGFACE_ID=B.WORKINGFACE_ID
            //   ) Base



            //            ;WITH TB AS
            //(
            //SELECT  
            //DISTINCT B.*
            //  FROM 
            //  T_TUNNEL_INFO AS A,
            //  T_WORKINGFACE_INFO AS B
            //   WHERE 
            //   TUNNEL_TYPE IN (0, 1, 2,3) AND A.WORKINGFACE_ID=B.WORKINGFACE_ID
            //   ) 

            //SELECT * FROM(
            //SELECT ROW_NUMBER() OVER (ORDER BY WORKINGFACE_ID) AS rowid ,* FROM
            //TB )
            //AS TB1
            //WHERE rowid>=1 AND rowid<=2


            //ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            //StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT * FROM ( ");
            //sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + TunnelInfoDbConstNames.ID + ") AS rowid, * ");
            //sb.Append("FROM " + TunnelInfoDbConstNames.TABLE_NAME + " ) AS TB ");
            //sb.Append("WHERE rowid >= " + iStartIndex);
            //sb.Append("AND rowid <= " + iEndIndex);
            //DataSet ds = db.ReturnDS(sb.ToString());
            //return ds;



            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            StringBuilder sb = new StringBuilder();
            sb.Append(";WITH TB AS" +
"(" +
"SELECT " +
"DISTINCT B.* " +
"  FROM " +
"  T_TUNNEL_INFO AS A, " +
"  T_WORKINGFACE_INFO AS B " +
"   WHERE " +
"   TUNNEL_TYPE IN (0, 1, 2,3) AND A.WORKINGFACE_ID=B.WORKINGFACE_ID" +
"   ) ");
            sb.Append("SELECT * FROM ( ");
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY " + TunnelInfoDbConstNames.WORKINGFACE_ID + ") AS rowid, * ");
            sb.Append("FROM TB ) AS TB1 ");
            sb.Append("WHERE rowid >= " + iStartIndex);
            sb.Append("AND rowid <= " + iEndIndex);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        /// <summary>
        /// 查询回采面个数
        /// </summary>
        /// <returns></returns>
        public static DataSet selectTunnelHC()
        {
            //
            //            SELECT 
            //DISTINCT B.*
            //  FROM 
            //  T_TUNNEL_INFO AS A,
            //  T_WORKINGFACE_INFO AS B
            //   WHERE 
            //   TUNNEL_TYPE IN (0, 1, 2,3) AND A.WORKINGFACE_ID=B.WORKINGFACE_ID


            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT DISTINCT B.* FROM " + TunnelInfoDbConstNames.TABLE_NAME + " AS A," +
                WorkingFaceDbConstNames.TABLE_NAME + " AS B " +
                "WHERE " + TunnelInfoDbConstNames.TUNNEL_TYPE + " IN (0, 1, 2,3) AND " +
                " A.WORKINGFACE_ID=B.WORKINGFACE_ID";
            DataSet ds = db.ReturnDS(sql);
            return ds;
        }

        /// <summary>
        /// 查询回采面个数
        /// </summary>
        /// <returns></returns>
        public static int selectTunnelHCCount()
        {

            //            SELECT 
            //DISTINCT B.*
            //  FROM 
            //  T_TUNNEL_INFO AS A,
            //  T_WORKINGFACE_INFO AS B
            //   WHERE 
            //   TUNNEL_TYPE IN (0, 1, 2,3) AND A.WORKINGFACE_ID=B.WORKINGFACE_ID

            //               SELECT 
            //COUNT(DISTINCT B.WORKINGFACE_ID)
            //  FROM 
            //  T_TUNNEL_INFO AS A,
            //  T_WORKINGFACE_INFO AS B
            //   WHERE 
            //   TUNNEL_TYPE IN (0, 1, 2,3) AND A.WORKINGFACE_ID=B.WORKINGFACE_ID


            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.MiningSchedulingDB);
            string sql = "SELECT COUNT(DISTINCT B.WORKINGFACE_ID) FROM " + TunnelInfoDbConstNames.TABLE_NAME + " AS A," +
                WorkingFaceDbConstNames.TABLE_NAME + " AS B " +
                "WHERE " + TunnelInfoDbConstNames.TUNNEL_TYPE + " IN (0, 1, 2,3) AND " +
                " A.WORKINGFACE_ID=B.WORKINGFACE_ID";
            DataSet ds = db.ReturnDS(sql);
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }

        /// <summary>
        /// 根据工作面id获取与该工作面关联的所有巷道
        /// </summary>
        /// <param name="iWorkingFaceID">工作面ID</param>
        /// <returns>巷道数据集</returns>
        public static DataSet selectTunnelByWorkingFaceId(int iWorkingFaceID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM " + TunnelInfoDbConstNames.TABLE_NAME);
            sb.Append(" WHERE " + TunnelInfoDbConstNames.WORKINGFACE_ID + " = " + iWorkingFaceID);
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }

        public static DataSet selectTunnelInfoNotShowTunneling(int iWorkingFaceID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT A.*,B." + WorkingFaceDbConstNames.IS_FINISH);
            sb.Append(" FROM " + TunnelInfoDbConstNames.TABLE_NAME + " AS A," + WorkingFaceDbConstNames.TABLE_NAME + " AS B ");
            sb.Append(" WHERE A." + TunnelInfoDbConstNames.WORKINGFACE_ID + " = " + iWorkingFaceID);
            sb.Append(" AND A." + TunnelInfoDbConstNames.WORKINGFACE_ID + " = B." +
                      WorkingFaceDbConstNames.WORKINGFACE_ID);
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sb.ToString());
            return ds;
        }
    }
}
