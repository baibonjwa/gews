// ******************************************************************
// 概  述：大断层实体
// 作  者：伍鑫
// 创建日期：2013/11/30
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class BigFaultageEntity
    {
        /** 断层编号 **/
        private int faultageId;

        /// <summary>
        /// 断层编号
        /// </summary>
        public int FaultageId
        {
            get { return faultageId; }
            set { faultageId = value; }
        }

        /** 断层名称 **/
        private string faultageName;

        /// <summary>
        /// 断层名称
        /// </summary>
        public string FaultageName
        {
            get { return faultageName; }
            set { faultageName = value; }
        }

        /** 落差 **/
        private String gap;

        /// <summary>
        /// 落差
        /// </summary>
        public String Gap
        {
            get { return gap; }
            set { gap = value; }
        }

        /** 倾角 **/
        private string angle;

        /// <summary>
        /// 倾角
        /// </summary>
        public string Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        /** 类型 **/
        private string type;

        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /** 走向 **/
        private String trend;

        /// <summary>
        /// 走向
        /// </summary>
        public String Trend
        {
            get { return trend; }
            set { trend = value; }
        }

        /** 类型 **/
        //private string type;

        ///// <summary>
        ///// 类型
        ///// </summary>
        //public string Type
        //{
        //    get { return type; }
        //    set { type = value; }
        //}

        /** 揭露点 **/
        //private string exposePoints;

        ///// <summary>
        ///// 揭露点
        ///// </summary>
        //public string ExposePoints
        //{
        //    get { return exposePoints; }
        //    set { exposePoints = value; }
        //}

        /** BID **/
        //private string bindingId;

        /// <summary>
        /// BID
        /// </summary>
        public string BindingId { get; set; }
    }
}
