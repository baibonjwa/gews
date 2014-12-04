// ******************************************************************
// 概  述：用户组信息实体
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
    public class UserGroupInformationManagementEntity
    {
        //用户组名称
        private string _groupname;
        /// <summary>
        /// 获取、设置用户组名称
        /// </summary>
        public string GroupName
        {
            get { return _groupname; }
            set { _groupname = value; }
        }

        //用户人数
        private string _usercount;
        /// <summary>
        /// 获取、设置用户人数
        /// </summary>
        public string UserCount
        {
            get { return _usercount; }
            set { _usercount = value; }
        }

        //备注
        private string _remark;
        /// <summary>
        /// 获取、设置备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        //用户组ID
        private string _Id;
        /// <summary>
        /// 获取、设置用户组ID
        /// </summary>
        public string ID
        {
            get { return _Id; }
            set { _Id = value; }
        }

        //用户组权限
        /// <summary>
        /// 获取、设置用户组ID
        /// </summary>
        public string Permission { get; set; }
     

    }
}
