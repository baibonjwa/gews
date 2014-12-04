// ******************************************************************
// 概  述：井筒实体
// 作  者：伍鑫
// 创建日期：2014/03/06
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGeometry;

namespace LibEntity
{
    public class PitshaftEntity
    {
        /** 井筒编号 **/
        private int pitshaftId;

        /// <summary>
        /// 井筒编号
        /// </summary>
        public int PitshaftId
        {
            get { return pitshaftId; }
            set { pitshaftId = value; }
        }

        /** 井筒名称 **/
        private string pitshaftName;

        /// <summary>
        /// 井筒名称
        /// </summary>
        public string PitshaftName
        {
            get { return pitshaftName; }
            set { pitshaftName = value; }
        }

        /** 井筒类型 **/
        private int pitshaftTypeId;

        /// <summary>
        /// 井筒类型
        /// </summary>
        public int PitshaftTypeId
        {
            get { return pitshaftTypeId; }
            set { pitshaftTypeId = value; }
        }

        /** 井口标高 **/
        private double wellheadElevation;

        /// <summary>
        /// 井口标高
        /// </summary>
        public double WellheadElevation
        {
            get { return wellheadElevation; }
            set { wellheadElevation = value; }
        }

        /** 井底标高 **/
        private double wellbottomElevation;

        /// <summary>
        /// 井底标高
        /// </summary>
        public double WellbottomElevation
        {
            get { return wellbottomElevation; }
            set { wellbottomElevation = value; }
        }

        /** 井筒坐标X **/
        private double pitshaftCoordinateX;

        /// <summary>
        /// 井筒坐标X
        /// </summary>
        public double PitshaftCoordinateX
        {
            get { return pitshaftCoordinateX; }
            set { pitshaftCoordinateX = value; }
        }

        /** 井筒坐标Y **/
        private double pitshaftCoordinateY;

        /// <summary>
        /// 井筒坐标Y
        /// </summary>
        public double PitshaftCoordinateY
        {
            get { return pitshaftCoordinateY; }
            set { pitshaftCoordinateY = value; }
        }

        /** 图形坐标X **/
        private double figureCoordinateX;

        /// <summary>
        /// 图形坐标X
        /// </summary>
        public double FigureCoordinateX
        {
            get { return figureCoordinateX; }
            set { figureCoordinateX = value; }
        }

        /** 图形坐标Y **/
        private double figureCoordinateY;

        /// <summary>
        /// 图形坐标Y
        /// </summary>
        public double FigureCoordinateY
        {
            get { return figureCoordinateY; }
            set { figureCoordinateY = value; }
        }

        /** 图形坐标Z **/
        private double figureCoordinateZ;

        /// <summary>
        /// 图形坐标ZZ
        /// </summary>
        public double FigureCoordinateZ
        {
            get { return figureCoordinateZ; }
            set { figureCoordinateZ = value; }
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
