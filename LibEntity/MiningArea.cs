// ******************************************************************
// 概  述：水平实体
// 作  者：伍鑫
// 创建日期：2014/02/25
// 版本号：1.0
// ******************************************************************

using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_MININGAREA_INFO")]
    public class MiningArea
    {
        /** 采区编号 **/

        /// <summary>
        ///     采区编号
        /// </summary>
        [PrimaryKey]
        [Property("MININGAREA_ID")]
        public int MiningAreaId { get; set; }

        /** 采区名称 **/

        /// <summary>
        ///     采区名称
        /// </summary>
        [Property("MININGAREA_NAME")]
        public string MiningAreaName { get; set; }

        /** 水平 **/

        /// <summary>
        ///     水平
        /// </summary>
        [BelongsTo("HORIZONTAL_ID")]
        public Horizontal Horizontal { get; set; }
    }
}