using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    [ActiveRecord("T_USER_INFO_LOGIN_MANAGEMENT")]
    public class UserLogin : ActiveRecordBase<UserLogin>
    {
        /// <summary>
        ///     ID
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "USER_ID")]
        public string Id { get; set; }

        /// <summary>
        ///     登录名
        /// </summary>
        [Property("USER_LOGIN_NAME")]
        public string LoginName { get; set; }

        /// <summary>
        ///     密码
        /// </summary>
        [Property("USER_PASSWORD")]
        public string PassWord { get; set; }

        /// <summary>
        ///     权限
        /// </summary>
        [Property("USER_PERMISSION")]
        public string Permission { get; set; }

        /// <summary>
        ///     用户组名称
        /// </summary>
        [Property("USER_GROUP_NAME")]
        public string GroupName { get; set; }

        /// <summary>
        ///     记住密码
        /// </summary>
        [Property("USER_SAVE_PASSWORD")]
        public int IsSavePassWord { get; set; }

        /// <summary>
        ///     尚未登录
        /// </summary>
        [Property("USER_IS_LOGINED")]
        public int IsLogined { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [Property("USER_REMARKS")]
        public string Remarks { get; set; }

        public static UserLogin[] FindAllByIsLogined(int value)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("IsLogined", value)
            };
            return FindAll(criterion);
        }

        public static UserLogin FindOneByLoginName(string loginName)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("LoginName", loginName)
            };
            return FindOne(criterion);
        }

        public static UserLogin FindOneByLoginNameAndPassword(string loginName, string password)
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("LoginName", loginName),
                Restrictions.Eq("PassWord", password)
            };
            return FindOne(criterion);
        }

        public new static int Count()
        {
            return Count(typeof(UserLogin));
        }
    }
}