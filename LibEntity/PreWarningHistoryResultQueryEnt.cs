// ******************************************************************
// 概  述：历史预警信息结果数据库逻辑
// 作  者：秦凯
// 创建日期：2014/03/22
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class PreWarningHistoryResultEnt : PreWarningResultQueryEnt
    {
        private string _gas = "";
        /// <summary>
        /// 瓦斯
        /// </summary>
        public string Gas
        {
            get { return _gas; }
            set { _gas = value; }
        }

        private string _coal = "";
        /// <summary>
        /// 煤层
        /// </summary>
        public string Coal
        {
            get { return _coal; }
            set { _coal = value; }
        }

        private string _geology = "";
        /// <summary>
        /// 地质
        /// </summary>
        public string Geology
        {
            get { return _geology; }
            set { _geology = value; }
        }

        private string _ventilation = "";
        /// <summary>
        /// 通风
        /// </summary>
        public string Ventilation
        {
            get { return _ventilation; }
            set { _ventilation = value; }
        }

        private string _management = "";
        /// <summary>
        /// 管理
        /// </summary>
        public string Management
        {
            get { return _management; }
            set { _management = value; }
        }

    }

    public class PreWarningHistoryResultWithWorkingfaceEnt : PreWarningResultQueryWithWorkingfaceEnt
    {
        private string _gas = "";
        /// <summary>
        /// 瓦斯
        /// </summary>
        public string Gas
        {
            get { return _gas; }
            set { _gas = value; }
        }

        private string _coal = "";
        /// <summary>
        /// 煤层
        /// </summary>
        public string Coal
        {
            get { return _coal; }
            set { _coal = value; }
        }

        private string _geology = "";
        /// <summary>
        /// 地质
        /// </summary>
        public string Geology
        {
            get { return _geology; }
            set { _geology = value; }
        }

        private string _ventilation = "";
        /// <summary>
        /// 通风
        /// </summary>
        public string Ventilation
        {
            get { return _ventilation; }
            set { _ventilation = value; }
        }

        private string _management = "";
        /// <summary>
        /// 管理
        /// </summary>
        public string Management
        {
            get { return _management; }
            set { _management = value; }
        }

    }

}
