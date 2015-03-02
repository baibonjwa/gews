// ******************************************************************
// 概  述：用户登录信息数据库业务逻辑
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
    public class UserLoginInformationManagementBLL
    {
        /// <summary>
        /// 获取所有登录用户信息
        /// </summary>
        /// <returns>用户登录信息实体数组</returns>
        public static UserLogin[] GetUserLoginInformations()
        {
            //select * from T_USER_INFO_LOGIN_MANAGEMENT
            UserLogin[] infos = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ");
            strSql.Append(LoginFormDbConstNames.TABLE_NAME);
            ManageDataBase database = new ManageDataBase(DATABASE_TYPE.WarningManagementDB);
            DataTable dt = database.ReturnDS(strSql.ToString()).Tables[0];
            if (dt != null)
            {
                int n = dt.Rows.Count;
                infos = new UserLogin[n];
                for (int i = 0; i < n; i++)
                {
                    UserLogin info = new UserLogin
                    {
                        LoginName = dt.Rows[i][1].ToString(),
                        PassWord = dt.Rows[i][2].ToString(),
                        Permission = dt.Rows[i][3].ToString(),
                        GroupName = dt.Rows[i][4].ToString(),
                        IsSavePassWord = Convert.ToInt32(dt.Rows[i][5]),
                        IsLogined = Convert.ToInt32(dt.Rows[i][6].ToString()),
                        Remarks = dt.Rows[i][7].ToString()
                    };
                    infos[i] = info;
                }
            }
            return infos;
        }

        /// <summary>
        /// 字段类型为Bit的字段，插值时需要转换为"True","False"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static bool ConvertTrueAndFalseToBool(string str)
        {
            bool b = false;
            bool.TryParse(str, out b);
            return b;
        }
    }
}
