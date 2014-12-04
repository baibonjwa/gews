// ******************************************************************
// 概  述：井筒实体
// 作  者：伍鑫
// 创建日期：2014/03/06
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

namespace LibEntity
{
    public class PitshaftEntity
    {
        /** 井筒编号 **/

        /// <summary>
        ///     井筒编号
        /// </summary>
        public int PitshaftId { get; set; }

        /** 井筒名称 **/

        /// <summary>
        ///     井筒名称
        /// </summary>
        public string PitshaftName { get; set; }

        /** 井筒类型 **/

        /// <summary>
        ///     井筒类型
        /// </summary>
        public int PitshaftTypeId { get; set; }

        /** 井口标高 **/

        /// <summary>
        ///     井口标高
        /// </summary>
        public double WellheadElevation { get; set; }

        /** 井底标高 **/

        /// <summary>
        ///     井底标高
        /// </summary>
        public double WellbottomElevation { get; set; }

        /** 井筒坐标X **/

        /// <summary>
        ///     井筒坐标X
        /// </summary>
        public double PitshaftCoordinateX { get; set; }

        /** 井筒坐标Y **/

        /// <summary>
        ///     井筒坐标Y
        /// </summary>
        public double PitshaftCoordinateY { get; set; }

        /** 图形坐标X **/

        /// <summary>
        ///     图形坐标X
        /// </summary>
        public double FigureCoordinateX { get; set; }

        /** 图形坐标Y **/

        /// <summary>
        ///     图形坐标Y
        /// </summary>
        public double FigureCoordinateY { get; set; }

        /** 图形坐标Z **/

        /// <summary>
        ///     图形坐标ZZ
        /// </summary>
        public double FigureCoordinateZ { get; set; }

        /** BID **/

        /// <summary>
        ///     BID
        /// </summary>
        public string BindingId { get; set; }
    }
}