// ******************************************************************
// 概  述：队别成员实体
// 作  者：宋英杰
// 日  期：2013/11/2
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
    public class TeamMemberInfoEntity
    {
        // 队员ID
        private int teamMemberID;

        /// <summary>
        /// 队员ID
        /// </summary>
        public int TeamMemberID
        {
            get { return teamMemberID; }
            set { teamMemberID = value; }
        }

        // 队员姓名
        private string teamMemberName;
        
        /// <summary>
        /// 队员姓名
        /// </summary>
        public string TeamMemberName
        {
            get { return teamMemberName; }
            set { teamMemberName = value; }
        }
    }
}
