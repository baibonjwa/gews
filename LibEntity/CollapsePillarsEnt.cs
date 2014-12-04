namespace LibEntity
{
    public class CollapsePillarsEnt
    {
        //主键

        /// <summary>
        ///     设置或获取主键
        /// </summary>
        public int ID { get; set; }

        //关键点ID

        /// <summary>
        ///     关键点ID
        /// </summary>
        public int PointID { get; set; }

        //陷落柱名称

        /// <summary>
        ///     设置或获取陷落柱名称
        /// </summary>
        public string CollapsePillarsName { get; set; }

        //关键点坐标X

        /// <summary>
        ///     设置或获取关键点坐标X
        /// </summary>
        public double CoordinateX { get; set; }

        //关键点坐标Y

        /// <summary>
        ///     设置或获取关键点坐标Y
        /// </summary>
        public double CoordinateY { get; set; }

        //关键点坐标Z

        /// <summary>
        ///     设置或获取关键点坐标Z
        /// </summary>
        public double CoordinateZ { get; set; }

        //描述

        /// <summary>
        ///     设置或获取描述
        /// </summary>
        public string Discribe { get; set; }

        //BID

        /// <summary>
        ///     bindingID
        /// </summary>
        public string BindingID { get; set; }

        //类别

        /// <summary>
        ///     类别
        /// </summary>
        public string Xtype { get; set; }
    }

    /// <summary>
    ///     20140531 lyf
    ///     陷落柱关键点实体
    /// </summary>
    public class CollapsePillarsKeyPointEnt
    {
        //关键点ID

        /// <summary>
        ///     关键点ID
        /// </summary>
        public int PointID { get; set; }

        //陷落柱ID

        /// <summary>
        ///     设置或获取陷落柱ID
        /// </summary>
        public int CollapsePillarsID { get; set; }

        //关键点坐标X

        /// <summary>
        ///     设置或获取关键点坐标X
        /// </summary>
        public double CoordinateX { get; set; }

        //关键点坐标Y

        /// <summary>
        ///     设置或获取关键点坐标Y
        /// </summary>
        public double CoordinateY { get; set; }

        //关键点坐标Z

        /// <summary>
        ///     设置或获取关键点坐标Z
        /// </summary>
        public double CoordinateZ { get; set; }

        //BID

        /// <summary>
        ///     绑定ID
        /// </summary>
        public string BindingID { get; set; }
    }
}