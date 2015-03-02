// ******************************************************************
// 概  述：登录窗体数据库业务逻辑
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
using LibDatabase;
using System.Data;
using LibEntity;

namespace LibBusiness
{
    public class LoginFormBLL
    {


        /// <summary>
        /// 获取所有登录用户信息
        /// </summary>
        /// <returns>用户登录信息实体数组，无用户信息时返回NULL</returns>
        //public static UserLogin[] GetUserLoginInformations()
        //{
        //    UserLogin[] infos = null;
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT * FROM ");
        //    strSql.Append(LoginFormDbConstNames.TABLE_NAME);
        //    ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
        //    DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
        //    if (dt != null)
        //    {
        //        int n = dt.Rows.Count;
        //        if (n > 0)
        //        {
        //            infos = new UserLogin[n];
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //        for (int i = 0; i < n; i++)
        //        {
        //            UserLogin info = new UserLogin
        //            {
        //                LoginName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][1].ToString()),
        //                PassWord = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][2].ToString()),
        //                Permission = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][3].ToString()),
        //                GroupName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][4].ToString()),
        //                IsSavePassWord = Convert.ToInt32(dt.Rows[i][5]),
        //                IsLogined = Convert.ToInt32(dt.Rows[i][6]),
        //                Remarks = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][7].ToString())
        //            };
        //            infos[i] = info;
        //        }
        //    }
        //    return infos;
        //}

        /// <summary>
        /// 获取所有用户组名称
        /// </summary>
        /// <returns>含有用户组名称的数组</returns>
        public static string[] GetUserGroupName()
        {
            string[] names = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT ");
            strSql.Append(UserGroupInformationMangementDbConstNames.USER_GROUP_NAME);
            strSql.Append(" FROM ");
            strSql.Append(UserGroupInformationMangementDbConstNames.TABLE_NAME);
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                int n = dt.Rows.Count;
                names = new string[n];
                for (int i = 0; i < n; i++)
                {
                    names[i] = dt.Rows[i][0].ToString();
                }
            }
            return names;
        }

        /// <summary>
        /// 获取表中记录条数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>表中记录条数</returns>
        public static int GetRecordCountFromTable()
        {
            int n = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);

            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                n = dt.Rows.Count;
            }
            return n;
        }

        /// <summary>
        /// 根据用户名，返回用户实体
        /// </summary>
        /// <returns>用户登录信息实体</returns>
        public static UserLogin GetUserLoginInformationByLoginname(string loginname)
        {
            UserLogin name = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(LoginFormDbConstNames.USER_LOGIN_NAME);
            strSql.Append(" =  ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(loginname) + "'");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                int n = dt.Rows.Count;
                for (int i = 0; i < n; i++)
                {
                    name = new UserLogin();
                    name.Id = dt.Rows[i][0].ToString();
                    name.LoginName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][1].ToString());
                    name.PassWord = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][2].ToString());
                    name.Permission = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][3].ToString());
                    name.GroupName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][4].ToString());
                    name.IsSavePassWord = Convert.ToInt32(dt.Rows[i][5]);
                    name.IsLogined = Convert.ToInt32(dt.Rows[i][6]);
                    name.Remarks = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][7].ToString());
                }
            }
            return name;
        }

        /// <summary>
        /// LoginSuccess
        /// </summary>
        /// <param name="loginname"></param>
        /// <param name="passward"></param>
        /// <returns></returns>
        public static UserLogin LoginSuccess(string loginname, string passward)
        {
            UserLogin name = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(LoginFormDbConstNames.USER_LOGIN_NAME);
            strSql.Append(" = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(loginname) + "'");
            strSql.Append(" AND ");
            strSql.Append(LoginFormDbConstNames.USER_PASSWORD);
            strSql.Append(" = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(passward) + "'");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                name = new UserLogin();
                name.Id = dt.Rows[0][0].ToString();
                name.LoginName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[0][1].ToString());
                name.PassWord = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[0][2].ToString());
                name.Permission = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[0][3].ToString());
                name.GroupName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[0][4].ToString());
                name.IsSavePassWord = Convert.ToInt32(dt.Rows[0][5]);
                name.IsLogined = Convert.ToInt32(dt.Rows[0][6].ToString());
                name.Remarks = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[0][7].ToString());
            }

            return name;
        }

        /// <summary>
        /// 根据ID，返回用户实体
        /// </summary>
        /// <returns>用户登录信息实体</returns>
        public static UserLogin GetUserLoginInformationByIDAndLoginName(string id, string loginname)
        {
            UserLogin name = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(LoginFormDbConstNames.USER_LOGIN_NAME);
            strSql.Append(" =  ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(loginname) + "'");
            strSql.Append(" AND ");
            strSql.Append(LoginFormDbConstNames.USER_ID);
            strSql.Append(" !=  ");
            strSql.Append("'" + id + "'");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                int n = dt.Rows.Count;
                for (int i = 0; i < n; i++)
                {
                    name = new UserLogin();
                    name.Id = dt.Rows[i][0].ToString();
                    name.LoginName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][1].ToString());
                    name.PassWord = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][2].ToString());
                    name.Permission = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][3].ToString());
                    name.GroupName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][4].ToString());
                    name.IsSavePassWord = Convert.ToInt32(dt.Rows[i][5].ToString());
                    name.IsLogined = Convert.ToInt32(dt.Rows[i][6].ToString());
                    name.Remarks = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][7].ToString());
                }
            }
            return name;
        }

        /// <summary>
        /// 通过Loginname删除用户信息记录
        /// </summary>
        /// <param name="name">用户登录名</param>
        /// <returns>删除成功返回True</returns>
        public static bool DeleteUserLoginInformationByLoginName(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(LoginFormDbConstNames.USER_LOGIN_NAME);
            strSql.Append(" = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(name) + "'");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }

        /// <summary>
        /// 通过用户登录名更新记录
        /// </summary>
        /// <param name="ent">新值</param>
        /// <param name="oldName">旧值的登录名</param>
        /// <returns>更新成功，返回True</returns>
        public static bool UpdateUserLoginInfomation(UserLogin ent, string oldName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);
            strSql.Append(" SET ");
            strSql.Append(LoginFormDbConstNames.USER_LOGIN_NAME + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.LoginName) + "',");
            strSql.Append(LoginFormDbConstNames.USER_PASSWORD + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.PassWord) + "',");
            strSql.Append(LoginFormDbConstNames.USER_PERMISSION + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Permission) + "',");
            strSql.Append(LoginFormDbConstNames.USER_GROUP_NAME + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.GroupName) + "',");
            strSql.Append(LoginFormDbConstNames.USER_SAVE_PASSWORD + " = ");
            strSql.Append("'" + ent.IsSavePassWord + "',");
            strSql.Append(LoginFormDbConstNames.USER_NAVER_LOGIN + " = ");
            strSql.Append("'" + ent.IsLogined + "',");
            strSql.Append(LoginFormDbConstNames.USER_REMARKS + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Remarks) + "'");
            strSql.Append(" WHERE ");
            strSql.Append(LoginFormDbConstNames.USER_LOGIN_NAME + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(oldName) + "'");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }
    }
}
