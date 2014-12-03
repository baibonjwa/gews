// ******************************************************************
// 概  述：岩性实体
// 作  者：伍鑫
// 创建日期：2013/11/26
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity
{
    public class LithologyEntity
    {
        /** 岩性编号 **/
        private int lithologyId;
        /** 岩性名 **/
        private string lithologyName;
        /** 岩性描述 **/
        private string lithologyDescribe;

        public int LithologyId
        {
            get { return lithologyId; }
            set { lithologyId = value; }
        }

        public string LithologyName
        {
            get { return lithologyName; }
            set { lithologyName = value; }
        }

        public string LithologyDescribe
        {
            get { return lithologyDescribe; }
            set { lithologyDescribe = value; }
        }
    }
}
