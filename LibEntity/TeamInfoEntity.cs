// ******************************************************************
// 概  述：队别实体
// 作  者：宋英杰
// 日  期：2013/12/3
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

namespace LibEntity
{
    public class TeamInfoEntity
    {
        // 队别ID

        /// <summary>
        ///     队别ID
        /// </summary>
        public int TeamID { get; set; }

        // 队别名称

        /// <summary>
        ///     队别名称
        /// </summary>
        public string TeamName { get; set; }

        // 队长姓名

        /// <summary>
        ///     队长姓名
        /// </summary>
        public string TeamLeader { get; set; }

        // 队员姓名

        /// <summary>
        ///     队员姓名
        /// </summary>
        public string TeamMember { get; set; }
    }
}