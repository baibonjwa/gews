using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class CollapsePillarsEnt
    {
        //主键
        private int iD;
        /// <summary>
        /// 设置或获取主键
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        //关键点ID
        private int pointID;
        /// <summary>
        /// 关键点ID
        /// </summary>
        public int PointID
        {
            get { return pointID; }
            set { pointID = value; }
        }
        //陷落柱名称
        private string collapsePillarsName;
        /// <summary>
        /// 设置或获取陷落柱名称
        /// </summary>
        public string CollapsePillarsName
        {
            get { return collapsePillarsName; }
            set { collapsePillarsName = value; }
        }
        //关键点坐标X
        private double coordinateX;
        /// <summary>
        /// 设置或获取关键点坐标X
        /// </summary>
        public double CoordinateX
        {
            get { return coordinateX; }
            set { coordinateX = value; }
        }
        //关键点坐标Y
        private double coordinateY;
        /// <summary>
        /// 设置或获取关键点坐标Y
        /// </summary>
        public double CoordinateY
        {
            get { return coordinateY; }
            set { coordinateY = value; }
        }
        //关键点坐标Z
        private double coordinateZ;
        /// <summary>
        /// 设置或获取关键点坐标Z
        /// </summary>
        public double CoordinateZ
        {
            get { return coordinateZ; }
            set { coordinateZ = value; }
        }
        //描述
        private string discribe;
        /// <summary>
        /// 设置或获取描述
        /// </summary>
        public string Discribe
        {
            get { return discribe; }
            set { discribe = value; }
        }
        //BID
        private string bindingID;
        /// <summary>
        /// bindingID
        /// </summary>
        public string BindingID
        {
            get { return bindingID; }
            set { bindingID = value; }
        }
        //类别
        private string xType;
        /// <summary>
        /// 类别
        /// </summary>
        public string Xtype
        {
            get { return xType; }
            set { xType = value; }
        }
    }

    /// <summary>
    /// 20140531 lyf 
    /// 陷落柱关键点实体
    /// </summary>
    public class CollapsePillarsKeyPointEnt
    {       
        //关键点ID
        private int pointID;
        /// <summary>
        /// 关键点ID
        /// </summary>
        public int PointID
        {
            get { return pointID; }
            set { pointID = value; }
        }

        //陷落柱ID
        private int collPillarsID;
        /// <summary>
        /// 设置或获取陷落柱ID
        /// </summary>
        public int CollapsePillarsID
        {
            get { return collPillarsID; }
            set { collPillarsID = value; }
        }

        //关键点坐标X
        private double coordinateX;
        /// <summary>
        /// 设置或获取关键点坐标X
        /// </summary>
        public double CoordinateX
        {
            get { return coordinateX; }
            set { coordinateX = value; }
        }

        //关键点坐标Y
        private double coordinateY;
        /// <summary>
        /// 设置或获取关键点坐标Y
        /// </summary>
        public double CoordinateY
        {
            get { return coordinateY; }
            set { coordinateY = value; }
        }

        //关键点坐标Z
        private double coordinateZ;
        /// <summary>
        /// 设置或获取关键点坐标Z
        /// </summary>
        public double CoordinateZ
        {
            get { return coordinateZ; }
            set { coordinateZ = value; }
        }

        //BID
        private string bindingID;
        /// <summary>
        /// 绑定ID
        /// </summary>
        public string BindingID
        {
            get { return bindingID; }
            set { bindingID = value; }
        }
    }
}
