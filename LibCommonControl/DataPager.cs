// ******************************************************************
// 概  述：分页控件
// 作  者：伍鑫
// 创建日期：2013/12/16
// 版本号：V1.1
// 版本信息：
// V1.0 新建
// V1.1 修复BUG
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibCommon;
using System.Text.RegularExpressions;

namespace LibCommonControl
{
    public partial class DataPager : UserControl
    {
        #region 成员变量
        // 数据总件数
        private int _RecordCount = 0;
        // 每页显示数据件数
        private int _PageSize = 0;
        // 总共页数
        private int _TotalPages = 0;
        // 当前显示页数
        private int _CurrentPage = 1;
        // 每页默认显示件数
        private int _DefaultDisplayCount = 3;

        // 添加一个委托
        public delegate void FrmChild_DelegateHandler(object sender);

        //添加一个PassDataBetweenFormHandler类型的事件
        public event FrmChild_DelegateHandler FrmChild_EventHandler;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataPager()
        {
            InitializeComponent();

            // 设置当前显示页数
            this.txtCurrentPage.Text = this._CurrentPage.ToString();
            // 设置默认显示条数
            this._DefaultDisplayCount = 30;
        }
        #endregion

        #region 分页控件初始化方法
        /// <summary>
        /// 分页控件初始化方法
        /// </summary>
        /// <param name="iRecordCount">数据总件数</param>
        public void PageControlInit(int iRecordCount)
        {
            // 有数据
            if (iRecordCount > 0)
            {
                // 获取每页显示件数
                if (this.cboPageSize.Text == "全部")
                {
                    // 设置当前显示页
                    this.txtCurrentPage.Text = "1";
                    this._CurrentPage = 1;

                    // 设置总页数
                    this.lblTotalPages.Text = "1";
                    this._TotalPages = 1;

                }
                else
                {
                    // 初期时
                    if (this.cboPageSize.Text == "")
                    {
                        this._PageSize = _DefaultDisplayCount;
                        this.cboPageSize.Text = _DefaultDisplayCount.ToString();
                    }
                    else
                    {
                        // 获取每页显示件数
                        this._PageSize = Convert.ToInt32(this.cboPageSize.Text);
                    }

                    // 计算出总页数
                    this._TotalPages = (int)Math.Ceiling((decimal)(iRecordCount / (decimal)this._PageSize));
                    // 设置总页数
                    this.lblTotalPages.Text = _TotalPages.ToString();
                }

                // 解决删除最后一页所有数据，当前显示页数不变的BUG
                if (Convert.ToInt32(this.txtCurrentPage.Text) > Convert.ToInt32(this.lblTotalPages.Text))
                {
                    this.txtCurrentPage.Text = this.lblTotalPages.Text;
                    this._CurrentPage = Convert.ToInt32(this.txtCurrentPage.Text);
                }

                // 如果总页数 = 1
                if (this._TotalPages == 1)
                {
                    this.btnFirstPage.Enabled = false;
                    this.btnPrePage.Enabled = false;
                    this.btnNextPage.Enabled = false;
                    this.btnLastPage.Enabled = false;
                }
                else
                {
                    // 如果当前页数 = 1
                    if (Convert.ToInt32(this.txtCurrentPage.Text) == 1)
                    {
                        this.btnFirstPage.Enabled = false;
                        this.btnPrePage.Enabled = false;
                        this.btnNextPage.Enabled = true;
                        this.btnLastPage.Enabled = true;
                    }
                    else
                    {
                        // 当前页数如果 = 总页数
                        if (Convert.ToInt32(this.txtCurrentPage.Text) == this._TotalPages)
                        {
                            this.btnFirstPage.Enabled = true;
                            this.btnPrePage.Enabled = true;
                            this.btnNextPage.Enabled = false;
                            this.btnLastPage.Enabled = false;
                        }

                        // 如果当前总页数 < 总页数
                        if (Convert.ToInt32(this.txtCurrentPage.Text) < this._TotalPages)
                        {
                            this.btnFirstPage.Enabled = true;
                            this.btnPrePage.Enabled = true;
                            this.btnNextPage.Enabled = true;
                            this.btnLastPage.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                this.btnFirstPage.Enabled = false;
                this.btnPrePage.Enabled = false;
                this.btnNextPage.Enabled = false;
                this.btnLastPage.Enabled = false;

            }

            _RecordCount = iRecordCount;

            // 总共件数
            this.txtTotalCount.Text = iRecordCount.ToString();

        }
        #endregion

        #region 检索开始位置（从第几条数据开始）
        /// <summary>
        /// 检索开始位置（从第几条数据开始）
        /// </summary>
        public int getStartIndex()
        {
            if (this.cboPageSize.Text == "全部")
            {
                return 0;
            }
            else
            {
                int pageSize;
                // 初期时
                if (this.cboPageSize.Text == "")
                {
                    pageSize = _DefaultDisplayCount;
                }
                else
                {
                    // 获取每页显示件数
                    pageSize = Convert.ToInt32(this.cboPageSize.Text);
                }

                // 获取当前显示页数
                int currentPage = Convert.ToInt32(this.txtCurrentPage.Text);

                return pageSize * (currentPage - 1);
            }

        }
        #endregion

        #region 检索结束位（到第几条数据结束）
        /// <summary>
        /// 检索结束位（到第几条数据结束）
        /// </summary>
        public int getEndIndex()
        {
            if (this.cboPageSize.Text == "全部")
            {
                return this._RecordCount;
            }
            else
            {
                int pageSize;
                // 初期时
                if (this.cboPageSize.Text == "")
                {
                    pageSize = _DefaultDisplayCount;
                }
                else
                {
                    // 获取每页显示件数
                    pageSize = Convert.ToInt32(this.cboPageSize.Text);
                }

                // 获取当前显示页数
                int currentPage = Convert.ToInt32(this.txtCurrentPage.Text);

                return pageSize * currentPage;
            }

        }
        #endregion

        #region 首页
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            // <首页>按钮设为不可用
            this.btnFirstPage.Enabled = false;
            // <上一页>按钮设为不可用
            this.btnPrePage.Enabled = false;
            // <下一页>按钮设为可用
            this.btnNextPage.Enabled = true;
            // <尾页>按钮设为可用
            this.btnLastPage.Enabled = true;
            // <当前页>文本框设为“1”
            this.txtCurrentPage.Text = "1";
            this._CurrentPage = 1;

            // 调用委托事件
            FrmChild_EventHandler(this);
        }
        #endregion

        #region 上一页
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrePage_Click(object sender, EventArgs e)
        {
            // <下一页>按钮设为可用
            this.btnNextPage.Enabled = true;
            // <尾页>按钮设为可用
            this.btnLastPage.Enabled = true;

            // 获取当前页数
            this._CurrentPage = Convert.ToInt32(this.txtCurrentPage.Text);
            // 如果当前页数 > 1，说明当前显示页数不是第一页
            if (this._CurrentPage > 1)
            {
                // 当前页数 - 1
                this._CurrentPage = this._CurrentPage - 1;
                // 重新设置当前页数
                this.txtCurrentPage.Text = this._CurrentPage.ToString();

                // <上一页>按钮设为不可用(解决当当前页数变为第一页时，<上一页>按钮还可以点击一次的BUG)
                if (this._CurrentPage == 1)
                {
                    // <上一页>按钮设为不可用
                    this.btnPrePage.Enabled = false;
                    // <首页>按钮设为不可用
                    this.btnFirstPage.Enabled = false;
                }
            }
            // 否则，如果当前显示页数是第一页
            else
            {
                // <上一页>按钮设为不可用
                this.btnPrePage.Enabled = false;
                // <首页>按钮设为不可用
                this.btnFirstPage.Enabled = false;
            }

            // 调用委托事件
            FrmChild_EventHandler(this);

        }
        #endregion

        #region 下一页
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            // <上一页>按钮设为可用
            this.btnPrePage.Enabled = true;
            // <首页>按钮设为可用
            this.btnFirstPage.Enabled = true;

            // 获取当前页数
            this._CurrentPage = Convert.ToInt32(this.txtCurrentPage.Text);
            // 获取总页数
            this._TotalPages = Convert.ToInt32(this.lblTotalPages.Text);
            // 如果当前显示页数 < 总页数，说明当前显示页数不是最后一页
            if (this._CurrentPage < this._TotalPages)
            {
                // 当前显示页数 + 1
                this._CurrentPage = this._CurrentPage + 1;
                // 重新设置当前页数
                this.txtCurrentPage.Text = this._CurrentPage.ToString();

                // <下一页>按钮设为不可用(解决当当前页数变为最后一页时，<下一页>按钮还可以点击一次的BUG)
                if (this._CurrentPage == this._TotalPages)
                {
                    // <下一页>按钮设为不可用
                    this.btnNextPage.Enabled = false;
                    // <尾页>按钮设为不可用
                    this.btnLastPage.Enabled = false;
                }
            }
            // 否则，如果当前显示页数是最后一页
            else
            {
                // <下一页>按钮设为不可用
                this.btnNextPage.Enabled = false;
                // <尾页>按钮设为不可用
                this.btnLastPage.Enabled = false;
            }

            // 调用委托事件
            FrmChild_EventHandler(this);

        }
        #endregion

        #region 尾页
        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnLastPage_Click(object sender, EventArgs e)
        {
            // <尾页>按钮设为不可用
            this.btnLastPage.Enabled = false;
            // <下一页>按钮设为不可用
            this.btnNextPage.Enabled = false;
            // <上一页>按钮设为可用
            this.btnPrePage.Enabled = true;
            // <首页>按钮设为可用
            this.btnFirstPage.Enabled = true;

            // <当前显示页数>文本框设为最大页数
            this.txtCurrentPage.Text = _PageSize.ToString();
            this._CurrentPage = _PageSize;

            // 调用委托事件
            FrmChild_EventHandler(this);
        }
        #endregion

        #region  每页显示件数的Combox变更事件
        /// <summary>
        /// 每页显示件数的Combox变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboPageSize_SelectedValueChanged(object sender, EventArgs e)
        {
            // 设置当前显示页数
            this.txtCurrentPage.Text = "1";
            this._CurrentPage = 1;

            // 获取每页显示件数
            if (this.cboPageSize.Text == "全部")
            {

                this.txtCurrentPage.Text = "1";
                this._CurrentPage = 1;

                this.lblTotalPages.Text = "1";
                this._TotalPages = 1;

            }
            else
            {
                // 初期时
                if (this.cboPageSize.Text == "")
                {
                    this._PageSize = _DefaultDisplayCount;
                    this.cboPageSize.Text = _DefaultDisplayCount.ToString();
                }
                else
                {
                    // 获取每页显示件数
                    this._PageSize = Convert.ToInt32(this.cboPageSize.Text);
                }

                // 计算出总页数
                this._TotalPages = (int)Math.Ceiling((decimal)(_RecordCount / (decimal)this._PageSize));
                // 设置总页数
                this.lblTotalPages.Text = this._TotalPages.ToString();
            }


            // 如果总页数 = 1
            if (this._TotalPages == 1)
            {
                this.btnFirstPage.Enabled = false;
                this.btnPrePage.Enabled = false;
                this.btnNextPage.Enabled = false;
                this.btnLastPage.Enabled = false;
            }
            else
            {
                // 如果当前页数 = 1
                if (Convert.ToInt32(this.txtCurrentPage.Text) == 1)
                {
                    this.btnFirstPage.Enabled = false;
                    this.btnPrePage.Enabled = false;
                    this.btnNextPage.Enabled = true;
                    this.btnLastPage.Enabled = true;
                }
                else
                {
                    // 当前页数如果 = 总页数
                    if (Convert.ToInt32(this.txtCurrentPage.Text) == this._TotalPages)
                    {
                        this.btnFirstPage.Enabled = true;
                        this.btnPrePage.Enabled = true;
                        this.btnNextPage.Enabled = false;
                        this.btnLastPage.Enabled = false;
                    }

                    // 如果当前总页数 < 总页数
                    if (Convert.ToInt32(this.txtCurrentPage.Text) < this._TotalPages)
                    {
                        this.btnFirstPage.Enabled = true;
                        this.btnPrePage.Enabled = true;
                        this.btnNextPage.Enabled = true;
                        this.btnLastPage.Enabled = true;
                    }
                }
            }

            // 调用委托事件
            FrmChild_EventHandler(this);
        }
        #endregion

        #region  跳转到N页
        /// <summary>
        /// 跳转到N页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGotoPage_Click(object sender, EventArgs e)
        {
            // 检查
            if (!check())
            {
                return;
            }

            // 设置当前页数
            this.txtCurrentPage.Text = this.txtGoto.Text;
            this._CurrentPage = Convert.ToInt32(this.txtCurrentPage.Text);

            // 获取每页显示件数
            if (this.cboPageSize.Text == "全部")
            {
                this.txtCurrentPage.Text = "1";
                this._CurrentPage = 1;
                this.lblTotalPages.Text = "1";
                this._TotalPages = 1;
            }
            else
            {
                // 初期时
                if (this.cboPageSize.Text == "")
                {
                    this._PageSize = _DefaultDisplayCount;
                    this.cboPageSize.Text = _DefaultDisplayCount.ToString();
                }
                else
                {
                    // 获取每页显示件数
                    this._PageSize = Convert.ToInt32(this.cboPageSize.Text);
                }

                // 计算出总页数
                this._TotalPages = (int)Math.Ceiling((decimal)(_RecordCount / (decimal)_PageSize));
                // 设置总页数
                this.lblTotalPages.Text = _TotalPages.ToString();
            }

            // 如果总页数 = 1
            if (this._TotalPages == 1)
            {
                this.btnFirstPage.Enabled = false;
                this.btnPrePage.Enabled = false;
                this.btnNextPage.Enabled = false;
                this.btnLastPage.Enabled = false;
            }
            else
            {
                // 如果当前页数 = 1
                if (Convert.ToInt32(this.txtCurrentPage.Text) == 1)
                {
                    this.btnFirstPage.Enabled = false;
                    this.btnPrePage.Enabled = false;
                    this.btnNextPage.Enabled = true;
                    this.btnLastPage.Enabled = true;
                }
                else
                {
                    // 当前页数如果 = 总页数
                    if (Convert.ToInt32(this.txtCurrentPage.Text) == this._TotalPages)
                    {
                        this.btnFirstPage.Enabled = true;
                        this.btnPrePage.Enabled = true;
                        this.btnNextPage.Enabled = false;
                        this.btnLastPage.Enabled = false;
                    }

                    // 如果当前总页数 < 总页数
                    if (Convert.ToInt32(this.txtCurrentPage.Text) < this._TotalPages)
                    {
                        this.btnFirstPage.Enabled = true;
                        this.btnPrePage.Enabled = true;
                        this.btnNextPage.Enabled = true;
                        this.btnLastPage.Enabled = true;
                    }
                }
            }

            // 调用委托事件
            FrmChild_EventHandler(this);
        }
        #endregion

        #region  到第几页文本框回车触发事件
        /// <summary>
        /// 到第几页文本框回车触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGoto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.btnGotoPage_Click(sender, e);
            }
        }
        #endregion

        /// <summary>
        /// 检查
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            bool isNumber = System.Text.RegularExpressions.Regex.IsMatch(this.txtGoto.Text, @"^[1-9]\d*$");
            // 判断是否是整型
            if (!isNumber)
            {
                this.txtGoto.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("跳转目标页应为大于0的正整数！");
                return false;
            }
            else
            {
                this.txtGoto.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }

            // 最大页数为空的时候
            if (LibCommon.Validator.IsEmpty(this.lblTotalPages.Text))
            {
                Alert.alert("当前无任何页！");
                this.txtGoto.Focus();
                return false;
            }

            // 如果跳转目标页 > 最大页数
            if (Convert.ToInt32(this.txtGoto.Text) > Convert.ToInt32(this.lblTotalPages.Text))
            {
                this.txtGoto.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("跳转目标页超过最大页数！");
                this.txtGoto.Focus();
                return false;
            }
            else
            {
                this.txtGoto.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }

            // 如果跳转目标页 > 最大页数
            if (Convert.ToInt32(this.txtGoto.Text) <= 0)
            {
                this.txtGoto.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert("跳转目标页应大于零！");
                this.txtGoto.Focus();
                return false;
            }
            else
            {
                this.txtGoto.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }

            // 判断每页显示件数是否是正整数
            if (this.cboPageSize.Text != "全部")
            {
                bool isNumber2 = System.Text.RegularExpressions.Regex.IsMatch(this.cboPageSize.Text, @"^[1-9]\d*$");
                if (!isNumber2)
                {
                    this.cboPageSize.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert("每页显示数应为大于0的正整数！");
                    return false;
                }
                else
                {
                    this.cboPageSize.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
            }


            return true;
        }
    }
}
