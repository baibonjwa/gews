using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_COLLAPSE_PILLARS_INFO")]
    public class CollapsePillarsEnt : ActiveRecordBase<CollapsePillarsEnt>
    {
        /// <summary>
        ///     设置或获取主键
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "ID")]
        public int Id { get; set; }

        //关键点ID

        /// <summary>
        ///     关键点ID
        /// </summary>
        public int PointId { get; set; }

        //陷落柱名称

        /// <summary>
        ///     设置或获取陷落柱名称
        /// </summary>
        [Property("COLLAPSE_PILLARS")]
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
        [Property("DISCRIBE")]
        public string Discribe { get; set; }

        //BID

        /// <summary>
        ///     bindingID
        /// </summary>
        public string BindingId { get; set; }

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
    public class CollapsePillarsKeyPointEnt : ActiveRecordBase
    {
        /// <summary>
        ///     关键点ID
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "ID")]
        public int PointId { get; set; }

        /// <summary>
        ///     设置或获取陷落柱ID
        /// </summary>
        [BelongsTo("ID")]
        public CollapsePillarsEnt CollapsePillars { get; set; }

        //关键点坐标X

        /// <summary>
        ///     设置或获取关键点坐标X
        /// </summary>
        [Property("COORDINATE_X")]
        public double CoordinateX { get; set; }

        //关键点坐标Y

        /// <summary>
        ///     设置或获取关键点坐标Y
        /// </summary>
        [Property("COORDINATE_Y")]
        public double CoordinateY { get; set; }

        //关键点坐标Z

        /// <summary>
        ///     设置或获取关键点坐标Z
        /// </summary>
        [Property("COORDINATE_Z")]
        public double CoordinateZ { get; set; }

        //BID

        /// <summary>
        ///     绑定ID
        /// </summary>
        [Property("BINDINGID")]
        public string BindingId { get; set; }
    }
}