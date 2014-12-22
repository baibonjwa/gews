// ******************************************************************
// 概  述：井筒类型实体
// 作  者：伍鑫
// 创建日期：2014/03/06
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using Castle.ActiveRecord;

namespace LibEntity
{
    public class PitshaftType : ActiveRecordBase<PitshaftType>
    {
        /// <summary>
        ///     井筒类型编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "PITSHAFT_TYPE_ID")]
        public int PitshaftTypeId { get; set; }

        /// <summary>
        ///     井筒类型名称
        /// </summary>
        [Property("PITSHAFT_TYPE_NAME")]
        public string PitshaftTypeName { get; set; }
    }
}