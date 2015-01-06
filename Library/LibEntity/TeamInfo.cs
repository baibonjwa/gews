// ******************************************************************
// 概  述：队别实体
// 作  者：宋英杰
// 日  期：2013/12/3
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_TEAM_INFO")]
    public class TeamInfo : ActiveRecordBase<TeamInfo>
    {
        /// <summary>
        ///     队别ID
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "TEAM_ID")]
        public int TeamId { get; set; }

        // 队别名称

        /// <summary>
        ///     队别名称
        /// </summary>
        [Property("TEAM_NAME")]
        public string TeamName { get; set; }

        // 队长姓名

        /// <summary>
        ///     队长姓名
        /// </summary>
        [Property("TEAM_LEADER")]
        public string TeamLeader { get; set; }

        // 队员姓名

        /// <summary>
        ///     队员姓名
        /// </summary>
        [Property("TEAM_MEMBER")]
        public string TeamMember { get; set; }

        public static TeamInfo FindById(int id)
        {
            return (TeamInfo)FindByPrimaryKey(typeof(TeamInfo), id);
        }

        public static int GetTotalCount()
        {
            return Count();
        }


    }
}