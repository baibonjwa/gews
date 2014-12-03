// ******************************************************************
// 概  述：用户信息管理业务逻辑
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
using System.Data;
using LibDatabase;
using LibEntity;
using LibEncryptDecrypt;

namespace LibBusiness
{
    public class UserInformationManagementBLL
    {
        //string sql = "select USER_LOGIN_NAME,USER_PASSWORD,USER_UNDER_GROUP,USER_UNDER_DEPT,USER_NAME,USER_EMAIL,USER_TEL,USER_PHONE,USER_REMARKS,USER_PERMISSION from T_USER_INFO_MANAGEMENT";
        //读取数据库中用户信息的sql语句
        public static DataTable  sqlGetUserInformation()
        {
            DataTable dt = new DataTable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(UserInformationManagementDbConstNames.USER_LOGIN_NAME+",");
            strSql.Append(UserInformationManagementDbConstNames.USER_PASSWORD+",");
            strSql.Append(UserInformationManagementDbConstNames.USER_UNDER_GROUP+",");
            strSql.Append(UserInformationManagementDbConstNames.USER_UNDER_DEPT+",");
            strSql.Append(UserInformationManagementDbConstNames.USER_NAME+",");
            strSql.Append(UserInformationManagementDbConstNames.USER_EMAIL+",");
            strSql.Append(UserInformationManagementDbConstNames.USER_TEL+",");
            strSql.Append(UserInformationManagementDbConstNames.USER_PHONE+",");
            strSql.Append(UserInformationManagementDbConstNames.USER_REMARKS+",");
            strSql.Append(UserInformationManagementDbConstNames.USER_PERMISSION+" ");
            strSql.Append("from ");
            strSql.Append(" "+UserInformationManagementDbConstNames.TABLE_NAME);

            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return dt=database.ReturnDS(strSql.ToString()).Tables[0];            
        }

        /// <summary>
        /// 获取用户信息实体
        /// </summary>
        /// <returns>可能返回null</returns>
        public static UserInformationManagementEntity[] sqlGetUserInformationEntity()
        {           
            UserInformationManagementEntity[] ents=null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(UserInformationManagementDbConstNames.USER_LOGIN_NAME + ",");
            strSql.Append(UserInformationManagementDbConstNames.USER_PASSWORD + ",");
            strSql.Append(UserInformationManagementDbConstNames.USER_UNDER_GROUP + ",");
            strSql.Append(UserInformationManagementDbConstNames.USER_UNDER_DEPT + ",");
            strSql.Append(UserInformationManagementDbConstNames.USER_NAME + ",");
            strSql.Append(UserInformationManagementDbConstNames.USER_EMAIL + ",");
            strSql.Append(UserInformationManagementDbConstNames.USER_TEL + ",");
            strSql.Append(UserInformationManagementDbConstNames.USER_PHONE + ",");
            strSql.Append(UserInformationManagementDbConstNames.USER_REMARKS + ",");
            strSql.Append(UserInformationManagementDbConstNames.USER_PERMISSION + " ");
            strSql.Append("from ");
            strSql.Append(" " + UserInformationManagementDbConstNames.TABLE_NAME);

            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ents = new UserInformationManagementEntity[dt.Rows.Count];
            }
            else
            {
                return null;
            }
            int n = dt.Rows.Count;
            for (int i = 0; i < n;i++ )
            {
                UserInformationManagementEntity ent = new UserInformationManagementEntity();
                ent.LoginName = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][0].ToString());
                ent.PassWord = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][1].ToString());
                ent.Group = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][2].ToString());
                ent.Department = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][3].ToString());
                ent.Name = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][4].ToString());
                ent.Email = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][5].ToString());
                ent.Tel = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][6].ToString());
                ent.Phone = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][7].ToString());
                ent.Remark = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][8].ToString());
                ent.Permission = LibEncryptDecrypt.DWEncryptDecryptClass.DecryptString(dt.Rows[i][9].ToString());
                ents[i] = ent;
            }
           
            return ents; 
        }

        //string sqlCount = "select count(*) from "+tableName;
        //获取表中记录条数，可将其放到通用下
       public static int sqlGetRecordCountFromTable(string tableName)
        {
            int n = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ");
            strSql.Append(tableName);

            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt=database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt!=null)
            {
                n = dt.Rows.Count;
            }
            return n;
        }

       //string sql = "DEPT_NAME from T_DEPT_INFO_MANAGEMENT";
        //获取所有部门名称
       public static string[] sqlGetDepartmentName()
       {
           string[] names= null;
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select distinct ");
           strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_NAME);
           strSql.Append(" from ");
           strSql.Append(DepartmentInformationManagemetDbConstNames.TABLE_NAME);
           ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
           DataTable dt=database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt!=null)
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

        //string sql = "select distinct USER_GROUP_NAME from T_USER_GROUP_INFO_MANAGEMENT";
        //获取所有用户组名称
       public static string[] sqlGetUserGroupName()
        {
            string[] names = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct ");
            strSql.Append(UserGroupInformationMangementDbConstNames.USER_GROUP_NAME);
            strSql.Append(" from ");
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

        //通过用户登陆名更新记录
        public static bool UpdateUserInfomationDatabase(UserInformationManagementEntity ent,string oldName)
        {
            //string sqlUpdate = "update T_USER_INFO_MANAGEMENT set USER_LOGIN_NAME = '" + ent.LoginName + "',"
            //        + "USER_PASSWORD = '" + ent.PassWord + "',"
            //        + "USER_UNDER_GROUP = '" + ent.Group + "',"
            //        + "USER_UNDER_DEPT = '" + ent.Department + "',"
            //        + "USER_NAME = '" + ent.Name + "',"
            //        + "USER_EMAIL = '" + ent.Email + "',"
            //        + "USER_TEL = '" + ent.Tel + "',"
            //        + "USER_PHONE = '" + ent.Phone + "',"
            //        + "USER_REMARKS = '" + ent.Remark + "',"
            //        + "USER_PERMISSION = '" + ent.Permission + "'"
            //        + " where USER_LOGIN_NAME = '" + oldName + "'";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ");
            strSql.Append(UserInformationManagementDbConstNames.TABLE_NAME);
            strSql.Append(" set ");
            strSql.Append(UserInformationManagementDbConstNames.USER_LOGIN_NAME + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.LoginName) + "',");
            strSql.Append(UserInformationManagementDbConstNames.USER_PASSWORD + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.PassWord) + "',");
            strSql.Append(UserInformationManagementDbConstNames.USER_UNDER_GROUP + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Group) + "',");
            strSql.Append(UserInformationManagementDbConstNames.USER_UNDER_DEPT + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Department) + "',"); 
            strSql.Append(UserInformationManagementDbConstNames.USER_NAME + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Name) + "',"); 
            strSql.Append(UserInformationManagementDbConstNames.USER_EMAIL + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Email) + "',"); 
            strSql.Append(UserInformationManagementDbConstNames.USER_TEL + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Tel) + "',");
            strSql.Append(UserInformationManagementDbConstNames.USER_PHONE + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Phone) + "',");
            strSql.Append(UserInformationManagementDbConstNames.USER_REMARKS + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Remark) + "',");
            strSql.Append(UserInformationManagementDbConstNames.USER_PERMISSION + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Permission) + "'");
            strSql.Append( " where ");
            strSql.Append(UserInformationManagementDbConstNames.USER_LOGIN_NAME + " = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(oldName) + "'");
            ManageDataBase database=new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return  database.OperateDB(strSql.ToString());
        }

        //通过Loginname删除用户信息记录
        public static bool DeleteUserInformationByLoginName(string name)
        {
            //string sqlDelete = "T_USER_INFO_MANAGEMENT  USER_LOGIN_NAME ='" + name + "'";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ");
            strSql.Append(UserInformationManagementDbConstNames.TABLE_NAME);
            strSql.Append(" where ");
            strSql.Append(UserInformationManagementDbConstNames.USER_LOGIN_NAME);
            strSql.Append(" = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(name) + "'");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }

        //查找重复用户名,查找到时，返回true
        public static bool FindTheSameLoginName(string newLoginName)
        {
            //DataTable dt = database.ReturnDS("T_USER_INFO_MANAGEMENT where USER_LOGIN_NAME = '" + name + "'").Tables[0];
            bool returnValue = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ");
            strSql.Append(UserInformationManagementDbConstNames.TABLE_NAME);
            strSql.Append(" where ");
            strSql.Append(UserInformationManagementDbConstNames.USER_LOGIN_NAME);
            strSql.Append(" = ");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(newLoginName) + "'");

            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt!=null)
            {
                if (dt.Rows.Count>0)
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }

        //向用户信息记录表中插入新的用户信息
        public static bool InsertRecordIntoTableUserInformation(UserInformationManagementEntity ent)
        {
            //string sql = "insert into T_USER_INFO_MANAGEMENT values ('" + _txtLoginName.Text.ToString().Trim() + "','"
            //    + _txtPassWord.Text.ToString().Trim() + "','"
            //    + _cboGroup.Text.ToString().Trim() + "','"
            //    + _cboDepartment.Text.ToString().Trim() + "','"
            //    + _txtName.Text.ToString().Trim() + "','"
            //    + _txtEmail.Text.ToString().Trim() + "','"
            //    + _txtTel.Text.ToString().Trim() + "','"
            //    + _txtPhoneNumber.Text.ToString().Trim() + "','"
            //    + _rtxtRemark.Text.ToString().Trim() + "','"
            //    + _cboPromission.Text.ToString().Trim() + "')";
            //database.OperateDB(sql);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ");
            strSql.Append(UserInformationManagementDbConstNames.TABLE_NAME);
            strSql.Append(" values (");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.LoginName) + "',");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.PassWord) + "',");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Group) + "',");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Department) + "',");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Name) + "',");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Email)  + "',");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Tel)  + "',");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Phone) + "',");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Remark) + "',");
            strSql.Append("'" + LibEncryptDecrypt.DWEncryptDecryptClass.EncryptString(ent.Permission) + "')");

            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }
    }  
}
