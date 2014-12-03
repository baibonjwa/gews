// ******************************************************************
// 概  述：用户详细信息实体
// 作  者：秦凯
// 创建日期：2014/03/10
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class UserInformationDetailsEnt
    {
        //ID
        private int _id = 0;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        //姓名
        private string _userName = "";
        public string Name
        {
            get { return _userName; }
            set { _userName = value; }
        }
        //手机号码
        private string _userPhoneNumber = "";
        public string PhoneNumber
        {
            get { return _userPhoneNumber; }
            set { _userPhoneNumber = value; }
        }
        //电话号码
        private string _userTelePhoneNumber = "";
        public string TelePhoneNumber
        {
            get { return _userTelePhoneNumber; }
            set { _userTelePhoneNumber = value; }
        }
        //用户邮箱
        private string _userEmail = "";
        public string Email
        {
            get { return _userEmail; }
            set { _userEmail = value; }
        }
        //所属部门
        private string _userDepratment = "";
        public string Depratment
        {
            get { return _userDepratment; }
            set { _userDepratment = value; }
        }
        //职位
        private string _userPosition = "";
        public string Position
        {
            get { return _userPosition; }
            set { _userPosition = value; }
        }
        //备注
        private string _userRemarks = "";
        public string Remarks
        {
            get { return _userRemarks; }
            set { _userRemarks = value; }
        }

        //是否通知预警信息
        public int IsInform { get; set; }
    }
}
