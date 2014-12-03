// ******************************************************************
// 概  述：勘探线实体
// 作  者：伍鑫
// 创建日期：2014/03/05
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class ProspectingLineEntity
    {
        /**  勘探线编号 **/
        private int prospectingLineId;

        /// <summary>
        ///  勘探线编号
        /// </summary>
        public int ProspectingLineId
        {
            get { return prospectingLineId; }
            set { prospectingLineId = value; }
        }

        /**  勘探线名称 **/
        private string prospectingLineName;

        /// <summary>
        ///  勘探线名称
        /// </summary>
        public string ProspectingLineName
        {
            get { return prospectingLineName; }
            set { prospectingLineName = value; }
        }

        /** 勘探钻孔 **/
        private string prospectingBorehole;

        /// <summary>
        /// 勘探钻孔
        /// </summary>
        public string ProspectingBorehole
        {
            get { return prospectingBorehole; }
            set { prospectingBorehole = value; }
        }

        /** BID **/
        private string bindingId;

        /// <summary>
        /// BID
        /// </summary>
        public string BindingId
        {
            get { return bindingId; }
            set { bindingId = value; }
        }
    }
}
