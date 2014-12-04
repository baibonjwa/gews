using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    /// <summary>
    /// 工作时间类
    /// </summary>
    public class WorkingTime
    {
        private string _strWorkTimeName = "";
        /// <summary>
        /// 获取设置工作制名称
        /// </summary>
        public string WorkTimeName
        {
            get { return _strWorkTimeName; }
            set { _strWorkTimeName = value; }
        }

        private string _strWorkTimeFrom = "";
        /// <summary>
        /// 获取设置工作起始时间
        /// </summary>
        public string WorkTimeFrom
        {
            get { return _strWorkTimeFrom; }
            set { _strWorkTimeFrom = value; }
        }

        private string _strWorkTimeTo = "";
        /// <summary>
        /// 获取设置工作终止时间
        /// </summary>
        public string WorkTimeTo
        {
            get { return _strWorkTimeTo; }
            set { _strWorkTimeTo = value; }
        }
    }
}
