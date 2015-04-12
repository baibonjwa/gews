using System;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibCommonForm;
using LibDatabase;

namespace _5.WarningManagement
{
    public partial class ShortMessage : Form
    {
        /// <summary>
        ///     实例化子窗体
        /// </summary>
        private readonly UserInformationDetailsManagement uidm = new UserInformationDetailsManagement();

        public ShortMessage()
        {
            InitializeComponent();
            //设置子窗体格式
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this,
                LibFormTitles.USER_INFO_DETAILS_MANMAGEMENT);
        }

        private void ShortMessage_Load(object sender, EventArgs e)
        {
            //注册事件
            uidm.CloseExternal(true);
            uidm.OnButtonClickHandle += uidm_OnButtonClickHandle;
            uidm.OnCancleButtonClickHandle += uidm_OnButtonClickHandle;
            uidm.OnExiteClickHandle += uidm_OnButtonClickHandle;

            //设置窗体能否接收子窗体
            IsMdiContainer = true;
            //设置窗体的子窗体
            uidm.MdiParent = this;
            //添加窗体
            panelFather.Controls.Add(uidm);
            //设置显示格式
            uidm.WindowState = FormWindowState.Maximized;
            uidm.Dock = DockStyle.Fill;
            uidm.Show();
            uidm.Activate();
            //加载窗体默认属性
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_WM.SHORT_MESSAGE_MANUALSEND);
            //获取机器端口显示在Combox中
            var ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            combPort.Items.AddRange(ports);
            combPort.SelectedIndex = 0;
            //波特率初值
            combBoteRate.SelectedIndex = 1;
        }

        private void uidm_OnButtonClickHandle(object sender, EventArgs e)
        {
            SetShortMessage.Sms_Disconnection();
            //关闭窗体
            Close();
        }

        /// <summary>
        ///     链接端口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btConnect_Click(object sender, EventArgs e)
        {
            try
            {
                var intBoteRate = 0;
                //判断波特率是否正确
                if (int.TryParse(combBoteRate.Text, out intBoteRate))
                {
                    //定义链接参数port
                    var port = Convert.ToInt16(combPort.Text.Replace("COM", ""));
                    var isture = SetShortMessage.Sms_Connection(CopyRightStr, port, Convert.ToInt16(intBoteRate),
                        TypeStr, CopyRightToCOMStr);
                    if (isture == 1)
                    {
                        rtbConnectInfo.BackColor = Color.Green;
                        rtbConnectInfo.Text = "连接短信猫成功,(短信猫型号为：" + TypeStr + ")！";
                    }
                    else
                    {
                        rtbConnectInfo.BackColor = Color.Red;
                        rtbConnectInfo.Text = "连接短信猫失败" + "(请重新连接短信猫)！";
                    }
                }
                else
                {
                    //波特率错误提示
                    MessageBox.Show("请输入正确的波特率！", "提示！");
                    combBoteRate.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        ///     发送短信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSendMessage_Click(object sender, EventArgs e)
        {
            //检查tbMessageContent(短信内容)中是否为空
            if (tbMessageContent.Text != "")
            {
                //检查是否选中用户
                if (UserInformationDetailsManagement._userSel.Count > 0)
                {
                    //通过ID查询所选择用户的电话号码并发送短信
                    foreach (var id in UserInformationDetailsManagement._userSel)
                    {
                        var phoneNumber = ReturnPhoneNumber(id);
                        if (!String.IsNullOrEmpty(phoneNumber))
                        {
                            SetShortMessage.Sms_Send(phoneNumber, tbMessageContent.Text);
                        }
                        else
                        {
                            MessageBox.Show("选择的用户电话号码有误！", "提示！");
                            return;
                        }
                    }
                }
                else
                {
                    //提示选择用户
                    MessageBox.Show("请选择用户！", "提示");
                }
            }
            else
            {
                //提示输入短信内容
                MessageBox.Show("请输入短信内容！");
                tbMessageContent.BackColor = Color.Red;
            }
        }

        /// <summary>
        ///     通过用户ID查询用户电话号码
        /// </summary>
        /// <param name="id">选择的用户ID</param>
        /// <returns></returns>
        public static string ReturnPhoneNumber(int id)
        {
            var manaDB = new ManageDataBase(DATABASE_TYPE.GasEmissionDB);
            var strBuilder = new StringBuilder();
            strBuilder.Append("SELECT " + UserInformationDetailsManagementDbConstNames.USER_PHONENUMBER);
            strBuilder.Append(" FROM " + UserInformationDetailsManagementDbConstNames.TABLE_NAME);
            strBuilder.Append(" WHERE " + UserInformationDetailsManagementDbConstNames.ID + "=" + id);
            var ds = manaDB.ReturnDS(strBuilder.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            return "";

            //if (double.TryParse(ds.Tables[0].Rows[0][0].ToString(), out douPhoneNumber) == false)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
            //if(ds.count)
        }

        ///// <summary>
        ///// 断开短信链接
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btBreak_Click(object sender, EventArgs e)
        //{
        //    SetShortMessage.Sms_Disconnection();
        //}

        private void rtbConnectInfo_TextChanged(object sender, EventArgs e)
        {
        }

        #region 参数设置

        //移动电话型号
        private readonly string TypeStr = "";
        private readonly string CopyRightToCOMStr = "";
        private readonly string CopyRightStr = "//上海迅赛信息技术有限公司,网址www.xunsai.com//";
        //定义RichTextbox改变text时颜色变量
        private int _intColor = 0;
        //定义用户电话号码
        private static string douPhoneNumber;

        #endregion
    }
}