namespace LibEntity
{
    /// <summary>
    ///     工作时间类
    /// </summary>
    public class WorkingTime
    {
        private string _strWorkTimeFrom = "";
        private string _strWorkTimeName = "";
        private string _strWorkTimeTo = "";

        /// <summary>
        ///     获取设置工作制名称
        /// </summary>
        public string WorkTimeName
        {
            get { return _strWorkTimeName; }
            set { _strWorkTimeName = value; }
        }

        /// <summary>
        ///     获取设置工作起始时间
        /// </summary>
        public string WorkTimeFrom
        {
            get { return _strWorkTimeFrom; }
            set { _strWorkTimeFrom = value; }
        }

        /// <summary>
        ///     获取设置工作终止时间
        /// </summary>
        public string WorkTimeTo
        {
            get { return _strWorkTimeTo; }
            set { _strWorkTimeTo = value; }
        }
    }
}