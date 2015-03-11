using LibCommon;

namespace LibEntity
{
    public class WarningResultEnt
    {
        private int _iOther = 4;
        private int _strCoal = 4;
        private int _strGas = 4;
        private int _strGeology = 4;
        private string _strID = Const.INVALID_ID.ToString();
        private int _strManagement = 4;
        private int _strVentilation = 4;

        private int _strWaringResult = 4;

        /// <summary>
        ///     数据库中的主键ID
        /// </summary>
        public string ID
        {
            get { return _strID; }
            set { _strID = value; }
        }

        /// <summary>
        ///     预警结果
        /// </summary>
        public int WarningResult
        {
            get { return _strWaringResult; }
            set { _strWaringResult = value; }
        }

        /// <summary>
        ///     瓦斯
        /// </summary>
        public int Gas
        {
            get { return _strGas; }
            set { _strGas = value; }
        }

        /// <summary>
        ///     煤层
        /// </summary>
        public int Coal
        {
            get { return _strCoal; }
            set { _strCoal = value; }
        }

        /// <summary>
        ///     地质
        /// </summary>
        public int Geology
        {
            get { return _strGeology; }
            set { _strGeology = value; }
        }

        /// <summary>
        ///     通风
        /// </summary>
        public int Ventilation
        {
            get { return _strVentilation; }
            set { _strVentilation = value; }
        }

        /// <summary>
        ///     管理
        /// </summary>
        public int Management
        {
            get { return _strManagement; }
            set { _strManagement = value; }
        }

        /// <summary>
        ///     处理状态
        /// </summary>
        public int HandleStatus { get; set; }
    }
}