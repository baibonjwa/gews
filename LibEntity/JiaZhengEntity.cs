using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class JiaZhengEntity
    {
        //记录ID
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        //标识回采还是掘进
        private string layerName;
        public string LayerName
        {
            get { return layerName;}
            set { layerName = value; }
        }
       
        //绑定ID
        private string bid;
        public string BID
        {
            get { return bid; }
            set { bid = value; }
        }

        //被校正的对象
        private string jzObjects;
        public string JzObjects
        {
            get { return jzObjects; }
            set { jzObjects = value;}
        }

        //校正日期
        private DateTime jzDateTime;
        public DateTime JzDateTime
        {
            get { return jzDateTime; }
            set { jzDateTime = value; }
        }
    }
}
