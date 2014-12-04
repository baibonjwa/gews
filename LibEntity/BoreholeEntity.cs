// ******************************************************************
// 概  述：钻孔实体
// 作  者：伍鑫
// 创建日期：2013/11/26
// 版本号：1.0
// ******************************************************************

namespace LibEntity
{
    public class BoreholeEntity
    {
        /** 钻孔编号（主键） **/

        /// <summary>
        ///     钻孔编号
        /// </summary>
        public int BoreholeId { get; set; }

        /** 孔号 **/

        /// <summary>
        ///     孔号
        /// </summary>
        public string BoreholeNumber { get; set; }

        /** 地面标高 **/

        /// <summary>
        ///     地面标高
        /// </summary>
        public double GroundElevation { get; set; }

        /** 坐标X **/

        /// <summary>
        ///     坐标X
        /// </summary>
        public double CoordinateX { get; set; }

        /** 坐标Y **/

        /// <summary>
        ///     坐标Y
        /// </summary>
        public double CoordinateY { get; set; }

        /** 坐标Z **/

        /// <summary>
        ///     坐标Z
        /// </summary>
        public double CoordinateZ { get; set; }

        /** 煤层结构 **/

        /// <summary>
        ///     煤层结构
        /// </summary>
        public string CoalSeamsTexture { get; set; }

        /** BID **/

        /// <summary>
        ///     BID
        /// </summary>
        public string BindingId { get; set; }

        // 以下代码暂时保留
        //static BoreholeEntity _emptyEntity;

        //static public BoreholeEntity EMPTY
        //{
        //    get
        //    {
        //        if (_emptyEntity == null)
        //        {
        //            _emptyEntity = new BoreholeEntity();
        //            _emptyEntity.BoreholeId = -1;
        //            _emptyEntity.BoreholeNumber = "-1";
        //            _emptyEntity.GroundElevation = "-1";
        //            _emptyEntity.CoalSeamsTexture = "-1";
        //        }
        //        return _emptyEntity;
        //    }
        //}
    }
}