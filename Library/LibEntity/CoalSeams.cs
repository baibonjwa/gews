// ******************************************************************
// 概  述：煤层实体
// 作  者：伍鑫
// 创建日期：2014/02/25
// 版本号：1.0
// ******************************************************************

using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_COAL_SEAMS_INFO")]
    public class CoalSeams : ActiveRecordBase<CoalSeams>
    {
        /// <summary>
        ///     煤层编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "COAL_SEAMS_ID")]
        public int CoalSeamsId { get; set; }

        /// <summary>
        ///     煤层名称
        /// </summary>
        [Property("COAL_SEAMS_NAME")]
        public string CoalSeamsName { get; set; }
    }
}