// ******************************************************************
// 概  述：将数据库中XYZ值存至文本
// 作  者：
// 创建日期：2014/03/04
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using LibDatabase;
using System.IO;
using System.Text;
using System.Data;

namespace LibBusiness
{
    public class SaveDBXyz2Txt
    {
        /// <summary>
        /// 将X,Y,Z值保存至文本文件
        /// </summary>
        /// <param name="svPath">保存文件路径</param>
        /// <param name="tableName">数据库表名</param>
        /// <param name="xFieldName">x字段名称</param>
        /// <param name="yFieldName">y字段名称</param>
        /// <param name="zFiledName">z字段名称</param>
        /// <param name="coalSeamID">煤层名称ID</param>
        public static void WriteXYZ(string svPath, string tableName, string xFieldName, string yFieldName, string zFiledName, int coalSeamID)
        {
            FileStream fsPointFile = null;
            StreamWriter sw = null;
            if (File.Exists(svPath))
            {
                File.Delete(svPath);
            }
            //创建写入文件 
            fsPointFile = new FileStream(svPath, FileMode.Create, FileAccess.Write);
            sw = new StreamWriter(fsPointFile);

            const string SEPERATOR = " ";
            const string RETURN_STR = "";
            ManageDataBase manaDB = new ManageDataBase(DATABASE_TYPE.OutburstPreventionDB);
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT " + xFieldName + "," + yFieldName + "," + zFiledName);
            sqlStr.Append(" FROM " + tableName);
            sqlStr.Append(" WHERE COAL_SEAMS_ID=" + coalSeamID);
            DataSet ds = manaDB.ReturnDS(sqlStr.ToString());
            int dsRowsCount = ds.Tables[0].Rows.Count;
            //开始写入值
            for (int i = 0; i < dsRowsCount; i++)
            {
                sw.WriteLine(ds.Tables[0].Rows[i][xFieldName] + SEPERATOR + ds.Tables[0].Rows[i][yFieldName] + SEPERATOR + ds.Tables[0].Rows[i][zFiledName] + RETURN_STR);
            }
            //写入完成
            sw.Close();
            fsPointFile.Close();
        }
    }
}
