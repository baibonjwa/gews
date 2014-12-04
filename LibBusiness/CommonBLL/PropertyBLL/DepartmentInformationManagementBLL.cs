// ******************************************************************
// 概  述：部门信息数据可业务逻辑
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
using System.Data;
using LibBusiness;
using LibDatabase;
using LibEntity;

namespace LibBusiness
{
    public class DepartmentInformationManagementBLL
    {
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDeptInformation()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_NAME + ",");
            strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_TEL + ",");
            strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_EMAIL + ",");
            strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_STAFF_COUNT + ",");
            strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_REMARKS);
            strSql.Append(" FROM ");
            strSql.Append(DepartmentInformationManagemetDbConstNames.TABLE_NAME);
            ManageDataBase database = new ManageDataBase(LibDatabase.DATABASE_TYPE.WarningManagementDB);
            return database.ReturnDS(strSql.ToString()).Tables[0];
        }

        /// <summary>
        /// 通过部门名称查找部门信息
        /// </summary>
        /// <param name="deptName">部门名称</param>
        /// <returns>返回的DataTable中包含所有满足条件的记录</returns>
        public static DataTable GetDeptInformationByDeptName(string deptName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");          
            strSql.Append(DepartmentInformationManagemetDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_NAME);
            strSql.Append(" = ");
            strSql.Append("'" + deptName + "'");
            ManageDataBase database = new ManageDataBase(LibDatabase.DATABASE_TYPE.WarningManagementDB);
            return database.ReturnDS(strSql.ToString()).Tables[0];
        }

        /// <summary>
        /// 插入新的部门信息记录
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public static bool InsertDeptInfoIntoTable(DepartmentInformation ent)
        {
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO ");
            strSql.Append(DepartmentInformationManagemetDbConstNames.TABLE_NAME);
            strSql.Append(" VALUES (");
            strSql.Append("'"+ent.Name+"',");
            strSql.Append("'" + ent.Tel + "',");
            strSql.Append("'" + ent.Email + "',"); 
            strSql.Append("'" + ent.Staff + "',"); 
            strSql.Append("'" + ent.Remark + "')");
            return database.OperateDB(strSql.ToString());
        }

        /// <summary>
        /// 通过部门名称修改部门信息记录
        /// </summary>
        /// <param name="ent">部门信息实体</param>
        /// <param name="oldName">部门旧名称</param>
        /// <returns></returns>
        public static bool UpdateDepartmentInfomationDatabase(DepartmentInformation ent, string oldName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE ");
            strSql.Append(DepartmentInformationManagemetDbConstNames.TABLE_NAME);
            strSql.Append(" SET ");
            strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_NAME + " = ");
            strSql.Append("'" + ent.Name + "',");
            strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_TEL + " = ");
            strSql.Append("'" + ent.Tel + "',");
            strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_EMAIL + " = ");
            strSql.Append("'" + ent.Email + "',");
            strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_STAFF_COUNT + " = ");
            strSql.Append("'"+ent.Staff+"',");
            strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_REMARKS + " = ");
            strSql.Append("'" + ent.Remark + "'");
            strSql.Append(" WHERE ");
            strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_NAME + " = ");
            strSql.Append("'" + oldName + "'");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }

        /// <summary>
        /// 通过部门名删除记录
        /// </summary>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public static bool DeleteDeptInformationByDeptName(string deptName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM ");
            strSql.Append(DepartmentInformationManagemetDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_NAME);
            strSql.Append(" = ");
            strSql.Append("'" + deptName + "'");
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            return database.OperateDB(strSql.ToString());
        }

        /// <summary>
        /// 修改用户详细信息中的部门
        /// </summary>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public static bool UpdateUserInformationWhenDeleteDept(string deptName)
        {
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.TABLE_NAME);
            strSql.Append(" SET ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.USER_DEPARTMENT);
            strSql.Append(" = '' WHERE ");
            strSql.Append(UserInformationDetailsManagementDbConstNames.USER_DEPARTMENT);
            strSql.Append("=");
            strSql.Append("'" + deptName + "'");
            return database.OperateDB(strSql.ToString());
        }

        /// <summary>
        /// 获取部门信息记录条数
        /// </summary>
        /// <returns></returns>
        public static int GetRecordCountFromTable()
        {
            int n = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(DepartmentInformationManagemetDbConstNames.TABLE_NAME);

            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                n = dt.Rows.Count;
            }
            return n;
        }

        /// <summary>
        /// 通过部门名称查找部门信息
        /// </summary>
        /// <param name="deptName">部门名称</param>
        /// <returns></returns>
        public static bool FindDeptInformationByDeptName(string deptName)
        {
            bool returnValue = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(DepartmentInformationManagemetDbConstNames.TABLE_NAME);
            strSql.Append(" WHERE ");
            strSql.Append(DepartmentInformationManagemetDbConstNames.DEPT_NAME);
            strSql.Append(" = ");
            strSql.Append("'" + deptName + "'");
            ManageDataBase database = new ManageDataBase(LibDatabase.DATABASE_TYPE.WarningManagementDB);
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
    }
}
