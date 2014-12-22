
using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_GEOLOGY_SPACE")]
    public class GeologySpace : ActiveRecordBase<GeologySpace>
    {
        // 主键

        /// <summary>
        ///     设置或获取主键
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "ID")]
        public int Id { get; set; }

        // 工作面ID

        /// <summary>
        /// </summary>
        [BelongsTo("WORKFACE_ID")]
        public WorkingFace WorkingFace { get; set; }

        // 距离

        /// <summary>
        /// </summary>
        [Property("TECTONIC_ID")]
        public string TectonicId { get; set; }

        // 起点坐标Y

        /// <summary>
        ///     设置或获取起点坐标Y
        /// </summary>
        [Property("TECTONIC_DISTANCE")]
        public double Distance { get; set; }

        // 类型

        /// <summary>
        ///     类型
        /// </summary>
        [Property("TECTONIC_DISTANCE")]
        public int TectonicType { get; set; }

        //时间 

        /// <summary>
        /// </summary>
        [Property("DATE_TIME")]
        public string OnDateTime { get; set; }
    }
}