// ******************************************************************
// 概  述：探头类型实体
// 作  者：伍鑫
// 日  期：2014/03/01
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

namespace LibEntity
{
    internal class ProbeType
    {
        /** 探头类型编号 **/

        /// <summary>
        ///     探头类型编号
        /// </summary>
        public int ProbeTypeId { get; set; }

        /** 探头类型名称 **/

        /// <summary>
        ///     探头类型名称
        /// </summary>
        public string ProbeTypeName { get; set; }
    }
}