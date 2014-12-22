// ******************************************************************
// 概  述：队别成员实体
// 作  者：宋英杰
// 日  期：2013/11/2
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

namespace LibEntity
{
    public class TeamMemberInfo
    {
        // 队员ID

        /// <summary>
        ///     队员ID
        /// </summary>
        public int TeamMemberID { get; set; }

        // 队员姓名

        /// <summary>
        ///     队员姓名
        /// </summary>
        public string TeamMemberName { get; set; }
    }
}