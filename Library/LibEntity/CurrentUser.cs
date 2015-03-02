// ******************************************************************
// 概  述：系统当前登录用户
// 作  者：秦凯
// 创建日期：2014/03/13
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCommon;

namespace LibEntity
{
    public static class CurrentUser
    {
        //当前登录的用户信息
        static public UserLogin CurLoginUserInfo = new UserLogin();
    }
}
