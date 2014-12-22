// ******************************************************************
// 概  述：部门信息管理表
// 作  者：秦凯
// 创建日期：2014/03/06
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibBusiness
{
    class DepartmentInformationManagemetDbConstNames
    {
        //表名
        public const string TABLE_NAME = "T_DEPT_INFO_MANAGEMENT";

        //部门信息Id
        public const string DEPT_Id = "DEPT_Id";

        //部门名称
        public const string DEPT_NAME = "DEPT_NAME";

        //部门电话
        public const string DEPT_TEL = "DEPT_TEL";

        //部门邮箱
        public const string DEPT_EMAIL = "DEPT_EMAIL";

        //部门人数
        public const string DEPT_STAFF_COUNT = "DEPT_STAFF_COUNT";

        //备注
        public const string DEPT_REMARKS = "DEPT_REMARKS";

    }
}
