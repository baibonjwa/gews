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
    public class WorkingFaceSelect
    {
        public static LibEntity.WorkingFaceSelect SelectWorkingFace(string tableName)
        {
            ManageDataBase db = new ManageDataBase(DATABASE_TYPE.GeologyMeasureDB);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ");
            sb.Append(WorkingFaceDbConstNames.TABLE_NAME + " W,");
            sb.Append(MiningAreaDbConstNames.TABLE_NAME + " MA,");
            sb.Append(HorizontalDbConstNames.TABLE_NAME + " H,");
            sb.Append(MineDbConstNames.TABLE_NAME + " M ");
            sb.Append(" WHERE ");
            //sb.Append(" W." + WorkingFaceDbConstNames.WORKINGFACE_ID + " = " + workingFaceID);
            sb.Append("W." + WorkingFaceDbConstNames.MININGAREA_ID + " = MA." + MiningAreaDbConstNames.MININGAREA_ID);
            sb.Append(" AND MA." + MiningAreaDbConstNames.HORIZONTAL_ID + " = H." + HorizontalDbConstNames.HORIZONTAL_ID);
            sb.Append(" AND H." + HorizontalDbConstNames.MINE_ID + " = M." + MineDbConstNames.MINE_ID);
            DataSet ds = db.ReturnDS(sb.ToString());

            LibEntity.WorkingFaceSelect workingFaceSelectEntity = new LibEntity.WorkingFaceSelect();
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    workingFaceSelectEntity.MiningAreaID = ds.Tables[0].Rows[0][MiningAreaDbConstNames.MININGAREA_ID] == null ? -1 : Convert.ToInt32(ds.Tables[0].Rows[0][MiningAreaDbConstNames.MININGAREA_ID].ToString());
                    workingFaceSelectEntity.HorizontalID = ds.Tables[0].Rows[0][HorizontalDbConstNames.HORIZONTAL_ID] == null ? -1 : Convert.ToInt32(ds.Tables[0].Rows[0][HorizontalDbConstNames.HORIZONTAL_ID].ToString());
                    workingFaceSelectEntity.MineID = ds.Tables[0].Rows[0][MineDbConstNames.MINE_ID] == null ? -1 : Convert.ToInt32(ds.Tables[0].Rows[0][MineDbConstNames.MINE_ID].ToString());
                }
                catch
                {
                    return workingFaceSelectEntity;
                }
            }
            return null;
        }
    }
}
