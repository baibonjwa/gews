using System;

namespace LibEntity
{
    public class JiaZhengEntity
    {
        //记录ID
        private DateTime jzDateTime;
        public int ID { get; set; }

        //标识回采还是掘进
        public string LayerName { get; set; }

        //绑定ID
        public string BID { get; set; }

        //被校正的对象
        public string JzObjects { get; set; }

        //校正日期

        public DateTime JzDateTime
        {
            get { return jzDateTime; }
            set { jzDateTime = value; }
        }
    }
}