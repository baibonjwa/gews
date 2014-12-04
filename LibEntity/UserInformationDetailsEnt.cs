// ******************************************************************
// 概  述：用户详细信息实体
// 作  者：秦凯
// 创建日期：2014/03/10
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************

namespace LibEntity
{
    public class UserInformationDetailsEnt
    {
        //ID

        //姓名
        private string _userDepratment = "";
        private string _userEmail = "";
        private string _userName = "";

        //手机号码
        private string _userPhoneNumber = "";
        private string _userPosition = "";
        private string _userRemarks = "";

        //电话号码
        private string _userTelePhoneNumber = "";

        public UserInformationDetailsEnt()
        {
            ID = 0;
        }

        public int ID { get; set; }

        public string Name
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string PhoneNumber
        {
            get { return _userPhoneNumber; }
            set { _userPhoneNumber = value; }
        }

        public string TelePhoneNumber
        {
            get { return _userTelePhoneNumber; }
            set { _userTelePhoneNumber = value; }
        }

        //用户邮箱

        public string Email
        {
            get { return _userEmail; }
            set { _userEmail = value; }
        }

        //所属部门

        public string Depratment
        {
            get { return _userDepratment; }
            set { _userDepratment = value; }
        }

        //职位

        public string Position
        {
            get { return _userPosition; }
            set { _userPosition = value; }
        }

        //备注

        public string Remarks
        {
            get { return _userRemarks; }
            set { _userRemarks = value; }
        }

        //是否通知预警信息
        public int IsInform { get; set; }
    }
}