// ******************************************************************
// 概  述：矿井实体
// 作  者：伍鑫
// 创建日期：2014/02/25
// 版本号：1.0
// ******************************************************************

using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_MINE_INFO")]
    public class Mine : ActiveRecordBase<Mine>
    {
        /// <summary>
        ///     矿井编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "MINE_ID")]
        public int MineId { get; set; }

        /// <summary>
        ///     矿井名称
        /// </summary>
        [Property("MINE_NAME")]
        public string MineName { get; set; }

        public static bool ExistsByMineName(string mineName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("MineName", mineName)
            };
            return Exists(criterion);
        }
    }
}