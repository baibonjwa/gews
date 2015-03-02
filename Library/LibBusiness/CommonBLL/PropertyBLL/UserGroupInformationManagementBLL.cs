// ******************************************************************
// 概  述：用户组信息管理业务逻辑
// 作  者：秦凯
// 创建日期：2013/03/06
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibBusiness;
using LibEntity;
using LibDatabase;
using System.Data;

namespace LibBusiness
{
    public class UserGroupInformationManagementBLL
    {
        /// <summary>
        /// 获取所有用户组信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUserGroupInformation()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(UserGroupInformationMangementDbConstNames.USER_GROUP_NAME 
                + ",");
            strSql.Append(UserGroupInformationMangementDbConstNames.USER_GROUP_STAFF_COUNT 
                + ",");
            strSql.Append(UserGroupInformationMangementDbConstNames.USER_GROUP_REMARKS);
            strSql.Append(" FROM ");
            strSql.Append(UserGroupInformationMangementDbConstNames.TABLE_NAME);
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            return dt;
        }

        /// <summary>
        /// 通过组名删除用户组记录
        /// </summary>
        /// <param name="groupName">用户组名称</param>
        /// <returns>是否删除成功</returns>
        public static bool DeleteUserGroupInformationByGroupName(string 
            groupName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM ");
            strSql.Append(UserGroupInformationMangementDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(UserGroupInformationMangementDbConstNames.USER_GROUP_NAME);
            strSql.Append(" = ");
            strSql.Append("'" + groupName + "'");
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }

        /// <summary>
        /// 通过用户组名更新记录
        /// </summary>
        /// <param name="ent">用户组实体</param>
        /// <param name="oldName">用户组名称</param>
        /// <returns>是否更新成功</returns>
        public static bool UpdateUserGroupInfomationDatabase(UserGroup ent, 
            string oldName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE ");
            strSql.Append(UserGroupInformationMangementDbConstNames.TABLE_NAME);
            strSql.Append(" SET ");
            strSql.Append(UserGroupInformationMangementDbConstNames.USER_GROUP_NAME 
                + " = ");
            strSql.Append("'" + ent.GroupName + "',");
            strSql.Append(UserGroupInformationMangementDbConstNames.USER_GROUP_STAFF_COUNT 
                + " = ");
            strSql.Append("'" + ent.UserCount + "',");
            strSql.Append(UserGroupInformationMangementDbConstNames.USER_GROUP_REMARKS 
                + " = ");
            strSql.Append("'" + ent.Remark + "'");
            strSql.Append(" WHERE ");
            strSql.Append(UserGroupInformationMangementDbConstNames.USER_GROUP_NAME 
                + " = ");
            strSql.Append("'" + oldName + "'");
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }

        /// <summary>
        /// 添加用户界面获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUserInformationForInputWindow()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(LoginFormDbConstNames.USER_LOGIN_NAME + ",");
            strSql.Append(LoginFormDbConstNames.USER_GROUP_NAME + ",");
            strSql.Append(LoginFormDbConstNames.USER_PERMISSION);
            strSql.Append(" FROM ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            return dt;
        }

        /// <summary>
        /// 查找重复用户名,查找到时，返回true
        /// </summary>
        /// <param name="newGroupName"></param>
        /// <returns></returns>
        public static bool FindTheSameGroupName(string newGroupName)
        {
            bool returnValue = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(UserGroupInformationMangementDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(UserGroupInformationMangementDbConstNames.USER_GROUP_NAME);
            strSql.Append(" = ");
            strSql.Append("'" + newGroupName + "'");

            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }

        /// <summary>
        ///  添加新的用户组记录
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public static bool 
            InsertRecordIntoTableUserGroupInformation(UserGroup ent)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO ");
            strSql.Append(UserGroupInformationMangementDbConstNames.TABLE_NAME);
            strSql.Append(" VALUES (");
            strSql.Append("'" + ent.GroupName + "',");
            strSql.Append(ent.UserCount + ",");
            strSql.Append("'" + ent.Remark + "',");
            strSql.Append("'" + ent.Permission + "')");
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }

        /// <summary>
        /// 更改用户的用户组
        /// </summary>
        /// <param name="newGroupName">新用户组名称</param>
        /// <param name="oldGroupName">旧用户组名称</param>
        /// <returns></returns>
        public static bool ChangeUserGroup(string newGroupName, string 
            userLoginName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);
            strSql.Append(" SET ");
            strSql.Append(LoginFormDbConstNames.USER_GROUP_NAME + " = ");
            strSql.Append("'" + 
                LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(newGroupName) 
                + "'");//清空组名
            strSql.Append(" WHERE ");
            strSql.Append(LoginFormDbConstNames.USER_LOGIN_NAME + " = ");
            strSql.Append("'" + 
                LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(userLoginName) 
                + "'");
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }

        /// <summary>
        /// 删除登录用户信息中用户组的值
        /// </summary>
        /// <param name="groupName">用户组名称</param>
        /// <returns>是否更新成功</returns>
        public static bool UpdateUserInformationWhenDeleteGroup(string 
            groupName)
        {
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);
            strSql.Append(" SET ");
            strSql.Append(LoginFormDbConstNames.USER_GROUP_NAME);
            strSql.Append(" = '' WHERE ");
            strSql.Append(LoginFormDbConstNames.USER_GROUP_NAME);
            strSql.Append("=");
            strSql.Append("'" + groupName + "'");
            return database.OperateDB(strSql.ToString());
        }

        /// <summary>
        /// 获取表中用户组记录条数
        /// </summary>
        /// <returns></returns>
        public static int GetRecordCountFromTable()
        {
            int n = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(UserGroupInformationMangementDbConstNames.TABLE_NAME);

            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                n = dt.Rows.Count;
            }
            return n;
        }

        /// <summary>
        /// 获取某个用户组的成员数量
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static int GetUserCountFromUserGroup(string groupName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append((LoginFormDbConstNames.TABLE_NAME));
            strSql.Append(" WHERE ");
            strSql.Append(LoginFormDbConstNames.USER_GROUP_NAME + " = ");
            strSql.Append("'" + 
                LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(groupName) 
                + "'");

            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];

            return dt.Rows.Count;
        }

        /// <summary>
        /// 更新某个用户组的成员数量
        /// </summary>
        /// <param name="groupName"></param>
        public static void UpdateUserCountFromUserGroup(string groupName)
        {
            int iCount = GetUserCountFromUserGroup(groupName);
            ManageDataBase database = new 
                ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE ");
            strSql.Append(UserGroupInformationMangementDbConstNames.TABLE_NAME);
            strSql.Append(" SET ");
            strSql.Append(UserGroupInformationMangementDbConstNames.USER_GROUP_STAFF_COUNT);
            strSql.Append(" = "+ iCount +" WHERE ");
            strSql.Append(UserGroupInformationMangementDbConstNames.USER_GROUP_NAME);
            strSql.Append("=");
            strSql.Append("'" + groupName + "'");
            database.OperateDB(strSql.ToString());
        }


    }
}
