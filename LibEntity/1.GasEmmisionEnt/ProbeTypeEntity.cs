// ******************************************************************
// 概  述：探头类型实体
// 作  者：伍鑫
// 日  期：2014/03/01
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    class ProbeTypeEntity
    {
        /** 探头类型编号 **/
        private int probeTypeId;

        /// <summary>
        /// 探头类型编号
        /// </summary>
        public int ProbeTypeId
        {
            get { return probeTypeId; }
            set { probeTypeId = value; }
        }

        /** 探头类型名称 **/
        private string probeTypeName;

        /// <summary>
        /// 探头类型名称
        /// </summary>
        public string ProbeTypeName
        {
            get { return probeTypeName; }
            set { probeTypeName = value; }
        }

    }
}
