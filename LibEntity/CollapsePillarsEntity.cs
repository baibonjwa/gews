// ******************************************************************
// 概  述：陷落柱实体
// 作  者：宋英杰
// 创建日期：2013/11/28
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class CollapsePillarsEntity
    {
        /**陷落柱编号**/
        private int collapsePillarsID;
        /**陷落柱名称**/
        private string collapsePillarsName;
        /**位置X**/
        private string collapsePillarsLocationX;
        /**位置Y**/
        private string collapsePillarsLocationY;
        /**位置Z**/
        private string collapsePillarsLocationZ;
        /**陷落柱长轴长**/
        private string collapsePillarsLongAxisLength;
        /**陷落柱短轴长**/
        private string collapsePillarsShortAxialLength;
        /**陷落柱描述**/
        private string collapsePillarsDescribe;

        public int CollapsePillarsID
        {
            get { return collapsePillarsID; }
            set { collapsePillarsID = value; }
        }

        public string CollapsePillarsName
        {
            get { return collapsePillarsName; }
            set { collapsePillarsName = value; }
        }

        public string CollapsePillarsLocationX
        {
            get { return collapsePillarsLocationX; }
            set { collapsePillarsLocationX = value; }
        }

        public string CollapsePillarsLocationY
        {
            get { return collapsePillarsLocationY; }
            set { collapsePillarsLocationY = value; }
        }

        public string CollapsePillarsLocationZ
        {
            get { return collapsePillarsLocationZ; }
            set { collapsePillarsLocationZ = value; }
        }

        public string CollapsePillarsLongAxisLength
        {
            get { return collapsePillarsLongAxisLength; }
            set { collapsePillarsLongAxisLength = value; }
        }

        public string CollapsePillarsShortAxialLength
        {
            get { return collapsePillarsShortAxialLength; }
            set { collapsePillarsShortAxialLength = value; }
        }

        public string CollapsePillarsDescribe
        {
            get { return collapsePillarsDescribe; }
            set { collapsePillarsDescribe = value; }
        }
    }
}
