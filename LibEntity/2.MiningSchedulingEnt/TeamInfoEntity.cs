// ******************************************************************
// 概  述：队别实体
// 作  者：宋英杰
// 日  期：2013/12/3
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class TeamInfoEntity
    {
        // 队别ID
        private int teamID;

        /// <summary>
        /// 队别ID
        /// </summary>
        public int TeamID
        {
            get { return teamID; }
            set { teamID = value; }
        }

        // 队别名称
        private string teamName;

        /// <summary>
        /// 队别名称
        /// </summary>
        public string TeamName
        {
            get { return teamName; }
            set { teamName = value; }
        }

        // 队长姓名
        private string teamLeader;

        /// <summary>
        /// 队长姓名
        /// </summary>
        public string TeamLeader
        {
            get { return teamLeader; }
            set { teamLeader = value; }
        }

        // 队员姓名
        private string teamMember;

        /// <summary>
        /// 队员姓名
        /// </summary>
        public string TeamMember
        {
            get { return teamMember; }
            set { teamMember = value; }
        }

    }
}
