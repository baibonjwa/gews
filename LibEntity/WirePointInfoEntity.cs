// ******************************************************************
// 概  述：导线点实体
// 作  者：宋英杰  
// 创建日期：2013/11/29
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class WirePointInfoEntity
    {
        private int iD;
        //导线点编号
        private string wirePointID;
        //坐标X
        private double coordinateX;
        //坐标Y
        private double coordinateY;
        //坐标Z
        private double coordinateZ;
        //距左帮距离
        private double leftDis;
        //距右帮距离
        private double rightDis;
        //绑定导线编号
        private int wireInfoID;
        //巷道ID
        private int tunnelID;
        //距顶板距离
        private double topDis;
        //距底板距离
        private double bottomDis;

        /// <summary>
        /// 距底板距离
        /// </summary>
        public double BottomDis
        {
            get { return bottomDis; }
            set { bottomDis = value; }
        }

        /// <summary>
        /// 距顶板距离
        /// </summary>
        public double TopDis
        {
            get { return topDis; }
            set { topDis = value; }
        }
        
        /// <summary>
        /// 巷道ID
        /// </summary>
        public int TunnelID
        {
            get { return tunnelID; }
            set { tunnelID = value; }
        }

        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        /// <summary>
        /// 绑定导线编号
        /// </summary>
        public int WireInfoID
        {
            get { return wireInfoID; }
            set { wireInfoID = value; }
        }
        //BID
        private string bindingID;

        /// <summary>
        /// 构造方法
        /// </summary>
        public WirePointInfoEntity()
        { 
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="src">导线点实体</param>
        public WirePointInfoEntity(WirePointInfoEntity src)
        {
            this.WirePointID = src.WirePointID;
            this.CoordinateX = src.CoordinateX;
            this.CoordinateY = src.CoordinateY;
            this.CoordinateZ = src.CoordinateZ;
            this.LeftDis = src.LeftDis;
            this.RightDis = src.RightDis;
            this.wireInfoID = src.wireInfoID;
        }

        /// <summary>
        /// 导线点编号
        /// </summary>
        public string WirePointID
        {
            get { return wirePointID; }
            set { wirePointID = value; }
        }

        /// <summary>
        /// 坐标X
        /// </summary>
        public double CoordinateX
        {
            get { return coordinateX; }
            set { coordinateX = value; }
        }

        /// <summary>
        /// 坐标Y
        /// </summary>
        public double CoordinateY
        {
            get { return coordinateY; }
            set { coordinateY = value; }
        }
        
        /// <summary>
        /// 坐标Z
        /// </summary>
        public double CoordinateZ
        {
            get { return coordinateZ; }
            set { coordinateZ = value; }
        }
        
        /// <summary>
        /// 距左帮距离
        /// </summary>
        public double LeftDis
        {
            get { return leftDis; }
            set { leftDis = value; }
        }
        
        /// <summary>
        /// 距右帮距离
        /// </summary>
        public double RightDis
        {
            get { return rightDis; }
            set { rightDis = value; }
        }

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
