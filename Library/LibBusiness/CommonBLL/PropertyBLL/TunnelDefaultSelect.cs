using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LibEntity;
using LibCommon;
using LibBusiness;
using LibDatabase;

namespace LibBusiness
{
    public class TunnelDefaultSelect
    {
        public static LibEntity.TunnelDefaultSelect 
            selectDefaultTunnel(string tableName)
        {
            string sql = "SELECT * FROM "+TunnelDefaultSelectDbConstNames.TABLE_NAME+
                " WHERE "+TunnelDefaultSelectDbConstNames.TABLE_NAME_USE+" = '"+
                tableName+"'";
            ManageDataBase db = new 
                ManageDataBase(LibDatabase.DATABASE_TYPE.GeologyMeasureDB);
            DataSet ds = db.ReturnDS(sql);
            LibEntity.TunnelDefaultSelect tunnelDefaultSelectEntity = new 
                LibEntity.TunnelDefaultSelect();
            if (ds.Tables[0].Rows.Count > 0)
            {
                tunnelDefaultSelectEntity.TableName = 
                    ds.Tables[0].Rows[0][TunnelDefaultSelectDbConstNames.TABLE_NAME_USE].ToString();
                tunnelDefaultSelectEntity.MineID = 
                    Convert.ToInt32(ds.Tables[0].Rows[0][TunnelDefaultSelectDbConstNames.MINE_ID].ToString());
                tunnelDefaultSelectEntity.HorizontalID = 
                    Convert.ToInt32(ds.Tables[0].Rows[0][TunnelDefaultSelectDbConstNames.HORIZONTAL_ID].ToString());
                tunnelDefaultSelectEntity.MiningAreaID = 
                    Convert.ToInt32(ds.Tables[0].Rows[0][TunnelDefaultSelectDbConstNames.MINING_AREA_ID].ToString());
                tunnelDefaultSelectEntity.WorkingFaceID = 
                    Convert.ToInt32(ds.Tables[0].Rows[0][TunnelDefaultSelectDbConstNames.WORKING_FACE_ID].ToString());
                return tunnelDefaultSelectEntity;
            }
            return null;
        }
    }
}
