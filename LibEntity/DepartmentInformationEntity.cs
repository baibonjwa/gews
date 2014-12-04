// ******************************************************************
// 概  述：部门信息实体
// 作  者：秦凯
// 创建日期：2014/03/07
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
    public class DepartmentInformationEntity
    {

        private string _name;
        /// <summary>
        /// 获取、设置部门名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _tel;
        /// <summary>
        /// 获取、设置电话
        /// </summary>
        public string Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }

        private string _email;
        /// <summary>
        /// 获取、设置邮箱
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _staff;
        /// <summary>
        /// 获取设置用户人数
        /// </summary>
        public string Staff
        {
            get { return _staff; }
            set { _staff = value; }
        }

        private string _remark;
        /// <summary>
        /// 获取、设置备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        private string _id;
        /// <summary>
        /// 获取、设置ID
        /// </summary>
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
