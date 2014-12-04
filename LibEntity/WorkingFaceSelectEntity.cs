using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class WorkingFaceSelectEntity
    {
        private string tableName;

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        private int mineID;

        public int MineID
        {
            get { return mineID; }
            set { mineID = value; }
        }

        private int horizontalID;

        public int HorizontalID
        {
            get { return horizontalID; }
            set { horizontalID = value; }
        }

        private int miningAreaID;

        public int MiningAreaID
        {
            get { return miningAreaID; }
            set { miningAreaID = value; }
        }
    }
}
