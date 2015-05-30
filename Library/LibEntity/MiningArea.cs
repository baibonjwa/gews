// ******************************************************************
// 概  述：水平实体
// 作  者：伍鑫
// 创建日期：2014/02/25
// 版本号：1.0
// ******************************************************************

using System.Collections.Generic;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_MININGAREA_INFO")]
    public class MiningArea : ActiveRecordBase<MiningArea>
    {
        /** 采区编号 **/

        /// <summary>
        ///     采区编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "MININGAREA_ID")]
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

        public static MiningArea[] FindAllByHorizontalId(int horizontalId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("Horizontal.HorizontalId", horizontalId)
            };
            return FindAll(criterion);
        }

        public static MiningArea FindOneByMiningAreaName(string miningAreaName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("MiningAreaName", miningAreaName)
            };
            return FindOne(criterion);
        }
    }
}