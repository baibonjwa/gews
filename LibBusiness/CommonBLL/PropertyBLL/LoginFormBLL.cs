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
        /// 返回曾经成功登录过系统的用户
        /// </summary>
        /// <returns>用户登录信息实体数组</returns>
        public static UserLoginInformationEnt[] GetUserLoginedInformation()
        {
            UserLoginInformationEnt[] names = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(LoginFormDbConstNames.USER_NAVER_LOGIN);
            strSql.Append(" =  ");
            strSql.Append("'" + ConvertTrueAndFalseToBool(LibCommon.Const.TrueOrFalse.False.ToString()) + "'");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt= database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt!=null)
            {
                int n = dt.Rows.Count;
                names = new UserLoginInformationEnt[n];
                for (int i = 0; i < n;i++ )
                {
                    UserLoginInformationEnt name = new UserLoginInformationEnt();
                    name.LoginName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][1].ToString());
                    name.PassWord = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][2].ToString());
                    name.Permission = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][3].ToString());
                    name.GroupName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][4].ToString());
                    name.SavePassWord = ConvertTrueAndFalseToBool(dt.Rows[i][5].ToString());
                    name.NaverLogin = ConvertTrueAndFalseToBool(dt.Rows[i][6].ToString());
                    name.Remarks = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][7].ToString());
                    names[i] = name;
                }
            }
            return names;
        }

        /// <summary>
        /// 字段类型为Bit的字段，插值时需要转换为"True","False"
        /// </summary>
        /// <param name="str">“True”or“False”</param>
        /// <returns>返回bool值</returns>
        private static bool ConvertTrueAndFalseToBool(string str)
        {
            bool b = false;
            bool.TryParse(str, out b);
            return b;
        }

        /// <summary>
        /// 记录登录用户中曾经登录过的用户信息
        /// </summary>
        /// <param name="ent">登录用户信息实体</param>
        /// <returns>是否记录成功</returns>
        public static bool RememberLoginUser(UserLoginInformationEnt ent)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);
            strSql.Append(" SET ");
            strSql.Append(LoginFormDbConstNames.USER_NAVER_LOGIN);
            strSql.Append(" = ");
            strSql.Append("'" + LibCommon.Const.TrueOrFalse.False.ToString() + "'");
            strSql.Append(" WHERE ");
            strSql.Append(LoginFormDbConstNames.USER_LOGIN_NAME);
            strSql.Append(" = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.LoginName) + "'");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }

        /// <summary>
        /// 记住密码
        /// </summary>
        /// <param name="strLoginName"></param>
        /// <returns>操作是否成功</returns>
        public static bool RememberPassword(string strLoginName,bool savePassword)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);
            strSql.Append(" SET ");
            strSql.Append(LoginFormDbConstNames.USER_NAVER_LOGIN);
            strSql.Append(" = ");
            strSql.Append("'" + LibCommon.Const.TrueOrFalse.False.ToString() + "',");
            strSql.Append(LoginFormDbConstNames.USER_SAVE_PASSWORD);
            strSql.Append(" = ");
            strSql.Append("'" + savePassword.ToString() + "'");
            strSql.Append(" WHERE ");
            strSql.Append(LoginFormDbConstNames.USER_LOGIN_NAME);
            strSql.Append(" = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(strLoginName) + "'");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }

        /// <summary>
        /// 获取所有登录用户信息
        /// </summary>
        /// <returns>用户登录信息实体数组，无用户信息时返回NULL</returns>
        public static UserLoginInformationEnt[] GetUserLoginInformations()
        {
            UserLoginInformationEnt[] infos = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);            
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                int n = dt.Rows.Count;
                if (n > 0)
                {
                    infos = new UserLoginInformationEnt[n];
                }
                else
                {
                    return null;
                }
                for (int i = 0; i < n; i++)
                {
                    UserLoginInformationEnt info = new UserLoginInformationEnt();
                    info.LoginName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][1].ToString());
                    info.PassWord = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][2].ToString());
                    info.Permission = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][3].ToString());
                    info.GroupName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][4].ToString());
                    info.SavePassWord = ConvertTrueAndFalseToBool(dt.Rows[i][5].ToString());
                    info.NaverLogin = ConvertTrueAndFalseToBool(dt.Rows[i][6].ToString());
                    info.Remarks = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][7].ToString());
                    infos[i] = info;
                }
            }
            return infos;
        }

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
        /// 插入新的用户登录信息
        /// </summary>
        /// <param name="ent">新值</param>
        /// <returns>是否录入成功</returns>
        public static bool InsertUserLoginInfoIntoTable(UserLoginInformationEnt ent)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);
            strSql.Append(" VALUES(");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.LoginName) + "',");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.PassWord) + "',");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Permission) + "',");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.GroupName) + "',");
            strSql.Append("'" + ent.SavePassWord + "',");
            strSql.Append("'" + ent.NaverLogin + "',");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Remarks) + "')");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
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
        public static UserLoginInformationEnt GetUserLoginInformationByLoginname(string loginname)
        {
            UserLoginInformationEnt name = null;
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
                    name = new UserLoginInformationEnt();
                    name.ID = dt.Rows[i][0].ToString() ;
                    name.LoginName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][1].ToString());
                    name.PassWord =  LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][2].ToString());
                    name.Permission =  LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][3].ToString());
                    name.GroupName =  LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][4].ToString());
                    name.SavePassWord = ConvertTrueAndFalseToBool(dt.Rows[i][5].ToString());
                    name.NaverLogin = ConvertTrueAndFalseToBool(dt.Rows[i][6].ToString());
                    name.Remarks =  LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][7].ToString());
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
        public static UserLoginInformationEnt LoginSuccess(string loginname, string passward)
        {
            UserLoginInformationEnt name = null;
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
                name = new UserLoginInformationEnt();
                name.ID = dt.Rows[0][0].ToString();
                name.LoginName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[0][1].ToString());
                name.PassWord = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[0][2].ToString());
                name.Permission = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[0][3].ToString());
                name.GroupName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[0][4].ToString());
                name.SavePassWord = ConvertTrueAndFalseToBool(dt.Rows[0][5].ToString());
                name.NaverLogin = ConvertTrueAndFalseToBool(dt.Rows[0][6].ToString());
                name.Remarks = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[0][7].ToString());
            }

            return name;
        }

        /// <summary>
        /// 根据ID，返回用户实体
        /// </summary>
        /// <returns>用户登录信息实体</returns>
        public static UserLoginInformationEnt GetUserLoginInformationByIDAndLoginName(string id,string loginname)
        {
            UserLoginInformationEnt name = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(LoginFormDbConstNames.USER_LOGIN_NAME);
            strSql.Append(" =  ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(loginname)+ "'");
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
                    name = new UserLoginInformationEnt();
                    name.ID = dt.Rows[i][0].ToString();
                    name.LoginName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][1].ToString());
                    name.PassWord = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][2].ToString());
                    name.Permission = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][3].ToString());
                    name.GroupName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][4].ToString());
                    name.SavePassWord = ConvertTrueAndFalseToBool(dt.Rows[i][5].ToString());
                    name.NaverLogin = ConvertTrueAndFalseToBool(dt.Rows[i][6].ToString());
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
        public static bool UpdateUserLoginInfomation(UserLoginInformationEnt ent, string oldName)
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
            strSql.Append("'" + ent.SavePassWord+ "',");
            strSql.Append(LoginFormDbConstNames.USER_NAVER_LOGIN + " = ");
            strSql.Append("'" + ent.NaverLogin + "',");
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
