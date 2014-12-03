// ******************************************************************
// 概  述：人员信息数据库业务逻辑
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
using LibEntity;
using LibDatabase;
using System.Data;

namespace LibBusiness
{
    public class UserInformationDetailsManagementBLL
    {
        /// <summary>
        /// 获取所有登录用户信息
        /// </summary>
        /// <returns>DataSet</returns>
        public static DataSet GetUserInformationDetailsDS()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.TABLE_NAME);
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataSet ds = database.ReturnDS(strSql.ToString());
            return ds;
        }

        /// <summary>
        /// 获取所有登录用户信息
        /// </summary>
        /// <returns>用户登录信息实体数组，无用户信息时返回NULL</returns>
        public static UserInformationDetailsEnt[] GetUserInformationDetails()
        {
            UserInformationDetailsEnt[] infos = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.TABLE_NAME);
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                int n = dt.Rows.Count;
                if (n > 0)
                {
                    infos = new UserInformationDetailsEnt[n];
                }
                else
                {
                    return null;
                }
                for (int i = 0; i < n; i++)
                {
                    UserInformationDetailsEnt info = new UserInformationDetailsEnt();
                    info.ID = (int)dt.Rows[i][0];
                    info.Name = dt.Rows[i][1].ToString();
                    info.PhoneNumber = dt.Rows[i][2].ToString();
                    info.TelePhoneNumber = dt.Rows[i][3].ToString();
                    info.Email = dt.Rows[i][4].ToString();
                    info.Depratment = dt.Rows[i][5].ToString();
                    info.Position = dt.Rows[i][6].ToString();
                    info.Remarks = dt.Rows[i][7].ToString();
                    info.IsInform = Convert.ToInt16(dt.Rows[i][8]);
                    infos[i] = info;
                }
            }
            return infos;
        }

        /// <summary>
        /// 获取表中记录条数
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns>表中记录条数</returns>
        public static int GetRecordCountFromTable()
        {
            int n = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.TABLE_NAME);

            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                n = dt.Rows.Count;
            }
            return n;
        }

        /// <summary>
        /// 通过ID删除用户信息记录
        /// </summary>
        /// <param name="name">ID</param>
        /// <returns>删除成功，返回True</returns>
        public static bool DeleteUserLoginInformationByID(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.ID);
            strSql.Append(" = ");
            strSql.Append(id);
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }

        /// <summary>
        /// 通过ID获取用户信息记录
        /// </summary>
        /// <param name="name">ID</param>
        /// <returns>若无值，则返回空</returns>
        public static UserInformationDetailsEnt GetUserLoginInformationByID(int id)
        {
            UserInformationDetailsEnt ent = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.ID);
            strSql.Append(" = ");
            strSql.Append(id);
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                int n = dt.Rows.Count;
                for (int i = 0; i < n; i++)
                {
                    ent = new UserInformationDetailsEnt();
                    ent.ID = (int)dt.Rows[i][0];
                    ent.Name = dt.Rows[i][1].ToString();
                    ent.PhoneNumber = dt.Rows[i][2].ToString();
                    ent.TelePhoneNumber = dt.Rows[i][3].ToString();
                    ent.Email = dt.Rows[i][4].ToString();
                    ent.Depratment = dt.Rows[i][5].ToString();
                    ent.Position = dt.Rows[i][6].ToString();
                    ent.Remarks = dt.Rows[i][7].ToString();
                    ent.IsInform = Convert.ToInt16(dt.Rows[i][8]);
                }
            }
            return ent;
        }

        /// <summary>
        /// 返回存有所有部门名称的数组，返回值可能null,注意检查
        /// </summary>
        /// <returns>所有部门名称</returns>
        public static string[] GetDepartmentNames()
        {
            string[] names = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT ");
            strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_NAME);
            strSql.Append(" FROM ");
            strSql.Append(DepartmentInformationManagemetDbConstNames.TABLE_NAME);
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
        /// 插入新的用户详细信息
        /// </summary>
        /// <param name="ent">新值</param>
        /// <returns>是否录入成功</returns>
        public static bool InsertUserInformationDetailsIntoTable(UserInformationDetailsEnt ent)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.TABLE_NAME);
            strSql.Append(" VALUES(");
            strSql.Append("'" + ent.Name + "',");
            strSql.Append("'" + ent.PhoneNumber + "',");
            strSql.Append("'" + ent.TelePhoneNumber + "',");
            strSql.Append("'" + ent.Email + "',");
            strSql.Append("'" + ent.Depratment + "',");
            strSql.Append("'" + ent.Position + "',");
            strSql.Append("'" + ent.Remarks + "',");
            strSql.Append("'" + ent.IsInform + "')");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }

        /// <summary>
        /// 修改用户详细信息
        /// </summary>
        /// <param name="ent">新值</param>
        /// <returns>是否修改成功</returns>
        public static bool UpdataUserInformationDetails(UserInformationDetailsEnt ent, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.TABLE_NAME);
            strSql.Append(" SET ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.USER_NAME + " = ");
            strSql.Append("'" + ent.Name + "',");
            strSql.Append(UserInformationDetailsManagementDbConstNames.USER_PHONENUMBER + " = ");
            strSql.Append("'" + ent.PhoneNumber + "',");
            strSql.Append(UserInformationDetailsManagementDbConstNames.USER_TELEPHONE + " = ");
            strSql.Append("'" + ent.TelePhoneNumber + "',");
            strSql.Append(UserInformationDetailsManagementDbConstNames.USER_EMAIL + " = ");
            strSql.Append("'" + ent.Email + "',");
            strSql.Append(UserInformationDetailsManagementDbConstNames.USER_DEPARTMENT + " = ");
            strSql.Append("'" + ent.Depratment + "',");
            strSql.Append(UserInformationDetailsManagementDbConstNames.USER_POSITION + " = ");
            strSql.Append("'" + ent.Position + "',");
            strSql.Append(UserInformationDetailsManagementDbConstNames.USER_REMARKS + " = ");
            strSql.Append("'" + ent.Remarks + "',");
            strSql.Append(UserInformationDetailsManagementDbConstNames.USER_ISINFORM + " = ");
            strSql.Append("'" + ent.IsInform + "'");
            strSql.Append(" WHERE ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.ID + "=");
            strSql.Append(id);
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }

        public static DataSet GetNeedSendMessageUsers()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.USER_ISINFORM + " = ");
            strSql.Append("1");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.ReturnDS(strSql.ToString());
        }
    }
}
