// ******************************************************************
// 概  述：水平实体
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
    [Serializable]
    public class MiningAreaEntity
    {
        /** 采区编号 **/
        private int miningAreaId;

        /// <summary>
        /// 采区编号
        /// </summary>
        public int MiningAreaId
        {
            get { return miningAreaId; }
            set { miningAreaId = value; }
        }

        /** 采区名称 **/
        private string miningAreaName;

        /// <summary>
        /// 采区名称
        /// </summary>
        public string MiningAreaName
        {
            get { return miningAreaName; }
            set { miningAreaName = value; }
        }

        /** 水平 **/
        private HorizontalEntity horizontal;

        /// <summary>
        /// 水平
        /// </summary>
        public HorizontalEntity Horizontal
        {
            get { return horizontal; }
            set { horizontal = value; }
        }
    }
}