using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibBusiness
{
    public class TunnelHChuanDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_TUNNEL_HENGCHUAN";

        //主键
        public const string ID = "ID";

        //巷道1ID 主运顺槽
        public const string TUNNEL_ID1 = "TUNNEL_ID1";

        //巷道2ID 辅运顺槽
        public const string TUNNEL_ID2 = "TUNNEL_ID2";
            
        //导线点1X
        public const string TUNNEL_X1 = "X_1";
            
        //导线点1Y
        public const string TUNNEL_Y1 = "Y_1";
            
        //导线点1Z
        public const string TUNNEL_Z1 = "Z_1";
            
        //导线点2X
        public const string TUNNEL_X2 = "X_2";
            
        //导线点2Y
        public const string TUNNEL_Y2 = "Y_2";
                    
        //导线点2Z
        public const string TUNNEL_Z2 = "Z_2";

        //方位角
        public const string TUNNEL_AZIMUTH = "AZIMUTH";

        //队别ID
        public const string TEAM_NAME_ID = "TEAM_NAME_ID";

        //开工日期
        public const string START_DATE = "START_DATE";
        
        //是否掘进、回采完毕
        public const string IS_FINISH = "IS_FINISH";

        //完工日期
        public const string STOP_DATE = "STOP_DATE";

        //工作制式
        public const string WORK_STYLE = "WORK_STYLE";

        //班次
        public const string WORK_TIME = "WORK_TIME";

        //巷道类型
        public const string TUNNEL_TYPE = "HENGCHUAN";

        //当前状态
        public const string TUNNEL_STATE = "STATE";

        //横川的状态
        public const string OPEN_HChuan = "开启";
        public const string DOING_HChuan = "施工";
        public const string CLOSE_HChuan = "封闭";
    }
}
