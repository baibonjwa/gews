// ******************************************************************
// 概  述：用户登录信息实体
// 作  者：秦凯
// 创建日期：2014/03/10
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************

namespace LibEntity
{
    public class UserLogin
    {
        //ID
        private string _id = "";
        private string _userGroupName = "";

        //登录名
        private string _userLoginName = "";

        //密码
        private string _userPassword = "";

        //权限
        private string _userPermission = "";
        private string _userRemarks = "";

        /// <summary>
        ///     默认构造函数
        /// </summary>
        public UserLogin()
        {
            NaverLogin = false;
            SavePassWord = false;
        }

        /// <summary>
        ///     分配默认用户权限的构造函数
        /// </summary>
        /// <param name="permission"></param>
        public UserLogin(string permission)
        {
            NaverLogin = false;
            SavePassWord = false;
            _userPermission = permission;
        }

        /// <summary>
        ///     ID
        /// </summary>
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        ///     登录名
        /// </summary>
        public string LoginName
        {
            get { return _userLoginName; }
            set { _userLoginName = value; }
        }

        /// <summary>
        ///     密码
        /// </summary>
        public string PassWord
        {
            get { return _userPassword; }
            set { _userPassword = value; }
        }

        /// <summary>
        ///     权限
        /// </summary>
        public string Permission
        {
            get { return _userPermission; }
            set { _userPermission = value; }
        }

        //用户组名称

        /// <summary>
        ///     用户组名称
        /// </summary>
        public string GroupName
        {
            get { return _userGroupName; }
            set { _userGroupName = value; }
        }

        //记住密码

        /// <summary>
        ///     记住密码
        /// </summary>
        public bool SavePassWord { get; set; }

        //尚未登录

        /// <summary>
        ///     尚未登录
        /// </summary>
        public bool NaverLogin { get; set; }

        //备注

        /// <summary>
        ///     备注
        /// </summary>
        public string Remarks
        {
            get { return _userRemarks; }
            set { _userRemarks = value; }
        }
    }
}