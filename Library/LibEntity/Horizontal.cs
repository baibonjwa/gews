using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_HORIZONTAL_INFO")]
    public class Horizontal : ActiveRecordBase<Horizontal>
    {

        /// <summary>
        ///     水平编号
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "HORIZONTAL_ID")]
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

        public static Horizontal[] FindAllByMineId(int mineId)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("Mine.MineId", mineId)
            };
            return FindAll(criterion);
        }
    }
}