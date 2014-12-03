// ******************************************************************
// 概  述：用户组中导入用户
// 作  者：秦凯
// 创建日期：2014/03/06
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibDatabase;

namespace LibCommonForm
{
    public partial class UserGroupManagementForAddUserIntoGroup : Form
    {
        ManageDataBase database = new ManageDataBase(LibDatabase.DATABASE_TYPE.WarningManagementDB);
        public UserGroupManagementForAddUserIntoGroup()
        {
            InitializeComponent();
        }

        private void UserGroupManagementForAddUserIntoGroup_Load(object sender, EventArgs e)
        {
            GetGroupInfo();
            GetUserInfo();
        }
        private void GetUserInfo()
        {
            string sql = "select loginname,username,usergroup,department,permission from T_USER_INFO_MANAGEMENT";
            if (database.ReturnDS(sql).Tables[0] != null)
            {
                _dgrvdUserInfo.DataSource = database.ReturnDS(sql).Tables[0];
                for (int i = 0; i < _dgrvdUserInfo.ColumnCount; i++)
                {
                    if (_dgrvdUserInfo.Columns[i].HeaderText == "loginname") _dgrvdUserInfo.Columns[i].HeaderText = "登陆名";
                    if (_dgrvdUserInfo.Columns[i].HeaderText == "usergroup") _dgrvdUserInfo.Columns[i].HeaderText = "所属用户组";
                    if (_dgrvdUserInfo.Columns[i].HeaderText == "username") _dgrvdUserInfo.Columns[i].HeaderText = "用户姓名";
                    if (_dgrvdUserInfo.Columns[i].HeaderText == "department") _dgrvdUserInfo.Columns[i].HeaderText = "所属部门";
                    if (_dgrvdUserInfo.Columns[i].HeaderText == "permission") _dgrvdUserInfo.Columns[i].HeaderText = "用户权限";
                    _dgrvdUserInfo.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }            
        }
        private void GetGroupInfo()
        {
            string sql = "select USER_GROUP_NAME,USER_GROUP_STAFF_COUNT,USER_GROUP_REMARKS from T_USER_GROUP_INFO_MANAGEMENT";
            if (database.ReturnDS(sql).Tables[0]!=null)
            {
                _dgrvdGroupInfo.DataSource = database.ReturnDS(sql).Tables[0];
                for (int i = 0; i < _dgrvdGroupInfo.ColumnCount;i++ )
                {
                    if (_dgrvdGroupInfo.Columns[i].HeaderText == "USER_GROUP_NAME") _dgrvdGroupInfo.Columns[i].HeaderText = "用户组名称";
                    if (_dgrvdGroupInfo.Columns[i].HeaderText == "USER_GROUP_STAFF_COUNT") _dgrvdGroupInfo.Columns[i].HeaderText = "用户组成员数";
                    if (_dgrvdGroupInfo.Columns[i].HeaderText == "remark") _dgrvdGroupInfo.Columns[i].HeaderText = "备注";
                    _dgrvdGroupInfo.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }            
        }

        private void _btnOK_Click(object sender, EventArgs e)
        {
            string strSelUser = "\"";
            for (int i = 0; i < _dgrvdUserInfo.SelectedRows.Count;i++ )
            {
                if (_dgrvdUserInfo.SelectedRows[i].Cells[0]==null)
                {
                    continue;
                }
                if (i==_dgrvdUserInfo.SelectedRows.Count-1)
                {
                    strSelUser += _dgrvdUserInfo.SelectedRows[i].Cells[0].Value.ToString() + "\"";
                }
                else
                {
                    strSelUser += _dgrvdUserInfo.SelectedRows[i].Cells[0].Value.ToString() + "\"、\"";
                }
            }
            string strSelGroup = "";
            if (_dgrvdGroupInfo.SelectedRows[0].Cells[0]!=null)
            {
                strSelGroup = _dgrvdGroupInfo.SelectedRows[0].Cells[0].Value.ToString();
            }
            string strQuestion = "确定将用户("+strSelUser+")\n添加到用户组(\""+strSelGroup+"\")中吗？";

            if (MessageBox.Show(strQuestion, "提示：", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)==DialogResult.OK) 
            {
                for (int i = 0; i < _dgrvdUserInfo.SelectedRows.Count; i++)
                {
                    if (_dgrvdUserInfo.SelectedRows[i].Cells[0] == null)
                    {
                        continue;
                    }
                    string sqlInsert = "UPDATE T_USER_INFO_MANAGEMENT SET usergroup = '" + strSelGroup + "' WHERE loginname = '" + _dgrvdUserInfo.SelectedRows[i].Cells[0].Value.ToString() + "'";
                    database.OperateDB(sqlInsert);
                }

                if (MessageBox.Show(@"操作成功，是否继续？","提示：",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)
                {
                    this.Close();
                }                
            }            
        }

        private void _btnCacle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _btnRefrsh_Click(object sender, EventArgs e)
        {
            GetGroupInfo();
            GetUserInfo();
        }
    }
}
