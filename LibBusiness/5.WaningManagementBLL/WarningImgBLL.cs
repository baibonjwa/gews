using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using LibDatabase;
using LibEntity;
using System.Data.SqlClient;

namespace LibBusiness
{
    public class WarningImgBLL
    {
        //向某预警信息添加图片
        public static bool InsertWarningImg(WarningImg ent)
        {
            StringBuilder sb = new StringBuilder();

            int length = 4;
            String[] props = new string[length];
            SqlDbType[] types = new SqlDbType[length];
            Object[] contents = new Object[length];
            string tableName = WarningImgDbConstNames.TABLE_NAME;

            props[0] = WarningImgDbConstNames.IMG_FILENAME;
            props[1] = WarningImgDbConstNames.WARNING_ID;
            props[2] = WarningImgDbConstNames.REMARKS;
            props[3] = WarningImgDbConstNames.IMG;

            types[0] = SqlDbType.VarChar;
            types[1] = SqlDbType.VarChar;
            types[2] = SqlDbType.VarChar;
            types[3] = SqlDbType.Image;


            contents[0] = ent.FileName;
            contents[1] = ent.WarningId;
            contents[2] = ent.Remarks;
            contents[3] = ent.Img;
            //sb.Append("INSERT INTO " + WarningImgDbConstNames.TABLE_NAME + "VALUES");
            //sb.Append("('" + ent.FileName + "','" + ent.WarningId + "','" + ent.Remarks + "',"")");

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            db.OperateDBWithPropertys(props, WarningImgDbConstNames.TABLE_NAME, length, types, contents);
            return false;
        }

        public static List<String> GetFilsNameListWithWarningId(string warningId)
        {
            string sqlStr = "SELECT " + WarningImgDbConstNames.IMG_FILENAME + " FROM " + WarningImgDbConstNames.TABLE_NAME;
            sqlStr += " WHERE " + WarningImgDbConstNames.WARNING_ID + " LIKE '%" + warningId + "%'";
            var strList = new List<String>();

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            SqlDataReader sdr = db.GetDataReader(sqlStr);
            while (sdr.Read())
            {
                strList.Add(sdr.GetValue(0).ToString());
            }
            sdr.Close();
            return strList;
        }

        public static bool IsRepeatWithWarningIdAndFileName(string warningId, string fileName)
        {
            string sqlStr = "SELECT " + WarningImgDbConstNames.IMG_FILENAME + " FROM " + WarningImgDbConstNames.TABLE_NAME;
            sqlStr += " WHERE " + WarningImgDbConstNames.WARNING_ID + " LIKE '%" + warningId + "%'";
            sqlStr += " AND " + WarningImgDbConstNames.IMG_FILENAME + "='" + fileName + "'";

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            SqlDataReader sdr = db.GetDataReader(sqlStr);
            while (sdr.Read())
            {
                return true;
            }
            return false;
        }

        public static void DeleteWarningImgWithWarningIdAndFileName(string warningId, string fileName)
        {
            string sqlStr = "DELETE " + WarningImgDbConstNames.TABLE_NAME;
            sqlStr += " WHERE " + WarningImgDbConstNames.WARNING_ID + " LIKE '%" + warningId + "%'";
            sqlStr += " AND " + WarningImgDbConstNames.IMG_FILENAME + "='" + fileName + "'";

            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            db.OperateDB(sqlStr);
        }

        public static byte[] GetImageWithWarningIdAndFileName(string warningId, string fileName)
        {
            string sqlStr = "SELECT " + WarningImgDbConstNames.IMG + " FROM " + WarningImgDbConstNames.TABLE_NAME;
            sqlStr += " WHERE " + WarningImgDbConstNames.WARNING_ID + " LIKE '%" + warningId + "%'";
            sqlStr += " AND " + WarningImgDbConstNames.IMG_FILENAME + "='" + fileName + "'";

            byte[] result = new byte[] { };
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            SqlDataReader sdr = db.GetDataReader(sqlStr);
            while (sdr.Read())
            {
                result = (byte[])sdr.GetValue(0);
            }
            return result;
        }
    }
}
