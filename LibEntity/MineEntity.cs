// ******************************************************************
// 概  述：矿井实体
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
    public class MineEntity
    {
        /** 矿井编号 **/
        private int mineId;

        /// <summary>
        /// 矿井编号
        /// </summary>
        public int MineId
        {
            get { return mineId; }
            set { mineId = value; }
        }

        /** 矿井名称 **/
        private string mineName;

        /// <summary>
        /// 矿井名称
        /// </summary>
        public string MineName
        {
            get { return mineName; }
            set { mineName = value; }
        }
    }
}