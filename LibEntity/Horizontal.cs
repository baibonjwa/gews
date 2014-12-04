// ******************************************************************
// 概  述：水平实体
// 作  者：伍鑫
// 创建日期：2014/02/25
// 版本号：1.0
// ******************************************************************

using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_HORIZONTAL_INFO")]
    public class Horizontal : ActiveRecordBase
    {

        /// <summary>
        ///     水平编号
        /// </summary>
        [PrimaryKey]
        [Property("HORIZONTAL_ID")]
        public int HorizontalId { get; set; }

        /// <summary>
        ///     水平名称
        /// </summary>
        [Property("HORIZONTAL_NAME")]
        public string HorizontalName { get; set; }

        /// <summary>
        ///     矿井
        /// </summary>
        [BelongsTo("MINE_ID")]
        public Mine Mine { get; set; }
    }
}