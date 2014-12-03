// ******************************************************************
// 概  述：工作面数据库常量名
// 作  者：伍鑫
// 创建日期：2014/02/25
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
namespace LibBusiness
{
    public static class WorkingFaceDbConstNames
    {
        // 表名
        public const string TABLE_NAME = "T_WORKINGFACE_INFO"; // 工作面信息表

        // 工作面编号
        public const string WORKINGFACE_ID = "WORKINGFACE_ID"; // 主键

        // 工作面名称
        public const string WORKINGFACE_NAME = "WORKINGFACE_NAME";

        // 采区编号
        public const string MININGAREA_ID = "MININGAREA_ID"; // 外键

        //工作面点坐标X分量
        public const string COORDINATE_X = "COORDINATE_X";//坐标点X值

        //工作面点坐标Y分量
        public const string COORDINATE_Y = "COORDINATE_Y";//坐标点Y值

        //工作面点坐标Z分量
        public const string COORDINATE_Z = "COORDINATE_Z";//坐标点Z值
	
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

        public const string WORKINGFACE_TYPE = "WORKINGFACE_TYPE";
    }
}
