// ******************************************************************
// 概  述：钻孔实体
// 作  者：伍鑫
// 创建日期：2013/11/26
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class BoreholeEntity
    {
        /** 钻孔编号（主键） **/
        private int boreholeId;

        /// <summary>
        /// 钻孔编号
        /// </summary>
        public int BoreholeId
        {
            get { return boreholeId; }
            set { boreholeId = value; }
        }

        /** 孔号 **/
        private string boreholeNumber;

        /// <summary>
        /// 孔号
        /// </summary>
        public string BoreholeNumber
        {
            get { return boreholeNumber; }
            set { boreholeNumber = value; }
        }

        /** 地面标高 **/
        private double groundElevation;

        /// <summary>
        /// 地面标高
        /// </summary>
        public double GroundElevation
        {
            get { return groundElevation; }
            set { groundElevation = value; }
        }

        /** 坐标X **/
        private double coordinateX;

        /// <summary>
        /// 坐标X
        /// </summary>
        public double CoordinateX
        {
            get { return coordinateX; }
            set { coordinateX = value; }
        }

        /** 坐标Y **/
        private double coordinateY;

        /// <summary>
        ///  坐标Y
        /// </summary>
        public double CoordinateY
        {
            get { return coordinateY; }
            set { coordinateY = value; }
        }

        /** 坐标Z **/
        private double coordinateZ;

        /// <summary>
        ///  坐标Z
        /// </summary>
        public double CoordinateZ
        {
            get { return coordinateZ; }
            set { coordinateZ = value; }
        }

        /** 煤层结构 **/
        private string coalSeamsTexture;

        /// <summary>
        /// 煤层结构
        /// </summary>
        public string CoalSeamsTexture
        {
            get { return coalSeamsTexture; }
            set { coalSeamsTexture = value; }
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
