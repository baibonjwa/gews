// ******************************************************************
// 概  述：回采巷道数据库常量名
// 作  者：宋英杰
// 创建日期：2014/03/11
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibBusiness
{
    public class TunnelHCDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_TUNNEL_HC";

        //主键
        public const string ID = "ID";

        //巷道1ID 主运顺槽
        public const string TUNNEL_ID1 = "TUNNEL_ID1";

        //巷道2ID 辅运顺槽
        public const string TUNNEL_ID2 = "TUNNEL_ID2";

        //巷道3ID 开切眼巷道
        public const string TUNNEL_ID3 = "TUNNEL_ID3";

        //其它关联巷道
        public const string TUNNEL_ID = "TUNNEL_ID";

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
        public const string TUNNEL_TYPE = "STOPING";
    }
}
