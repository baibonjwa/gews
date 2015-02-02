using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_TEAM_INFO")]
    public class Team : ActiveRecordBase<Team>
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

        public static Team FindById(int id)
        {
            return (Team)FindByPrimaryKey(typeof(Team), id);
        }

        public static int GetTotalCount()
        {
            return Count();
        }


    }
}