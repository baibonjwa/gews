// ******************************************************************
// 概  述：大断层实体
// 作  者：伍鑫
// 创建日期：2013/11/30
// 版本号：1.0
// ******************************************************************

namespace LibEntity
{
    public class BigFaultageEntity
    {
        /** 断层编号 **/

        /// <summary>
        ///     断层编号
        /// </summary>
        public int FaultageId { get; set; }

        /** 断层名称 **/

        /// <summary>
        ///     断层名称
        /// </summary>
        public string FaultageName { get; set; }

        /** 落差 **/

        /// <summary>
        ///     落差
        /// </summary>
        public string Gap { get; set; }

        /** 倾角 **/

        /// <summary>
        ///     倾角
        /// </summary>
        public string Angle { get; set; }

        /** 类型 **/

        /// <summary>
        ///     类型
        /// </summary>
        public string Type { get; set; }

        /** 走向 **/

        /// <summary>
        ///     走向
        /// </summary>
        public string Trend { get; set; }

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
        ///     BID
        /// </summary>
        public string BindingId { get; set; }
    }
}