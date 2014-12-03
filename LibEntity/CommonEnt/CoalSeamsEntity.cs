// ******************************************************************
// 概  述：煤层实体
// 作  者：伍鑫
// 创建日期：2014/02/25
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGeometry;

namespace LibEntity
{
    public class CoalSeamsEntity
    {
        /** 煤层编号 **/
        private int coalSeamsId;

        /// <summary>
        /// 煤层编号
        /// </summary>
        public int CoalSeamsId
        {
            get { return coalSeamsId; }
            set { coalSeamsId = value; }
        }

        /** 煤层名称 **/
        private string coalSeamsName;

        /// <summary>
        /// 煤层名称
        /// </summary>
        public string CoalSeamsName
        {
            get { return coalSeamsName; }
            set { coalSeamsName = value; }
        }
    }
}