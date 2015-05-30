using Castle.ActiveRecord;

namespace LibEntity
{
    [ActiveRecord("T_USER_INFO_DETAILS_MANAGEMENT")]
    public class UserInformationDetails : ActiveRecordBase<UserInformationDetails>
    {
        public UserInformationDetails()
        {
            Remarks = "";
            Position = "";
            Depratment = "";
            Email = "";
            TelePhoneNumber = "";
            PhoneNumber = "";
            Name = "";
            Id = 0;
        }

        //编号
        [PrimaryKey(PrimaryKeyType.Identity, "USER_ID")]
        public int Id { get; set; }
        //姓名
        [Property("USER_NAME")]
        public string Name { get; set; }
        //手机号码
        [Property("USER_PHONENUMBER")]
        public string PhoneNumber { get; set; }
        //电话号码
        [Property("USER_TELEPHONE")]
        public string TelePhoneNumber { get; set; }

        //用户邮箱
        [Property("USER_EMAIL")]
        public string Email { get; set; }

        //所属部门
        [Property("USER_DEPARTMENT")]
        public string Depratment { get; set; }

        //职位
        [Property("USER_POSITION")]
        public string Position { get; set; }

        //备注
        [Property("USER_REMARKS")]
        public string Remarks { get; set; }

        //是否通知预警信息
        [Property("USER_ISINFORM")]
        public int IsInform { get; set; }
    }
}