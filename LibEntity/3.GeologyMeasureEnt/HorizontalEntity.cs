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
    public class HorizontalEntity
    {
        /** 水平编号 **/
        private int horizontalId;

        /// <summary>
        /// 水平编号
        /// </summary>
        public int HorizontalId
        {
            get { return horizontalId; }
            set { horizontalId = value; }
        }

        /** 水平名称 **/
        private string horizontalName;

        /// <summary>
        /// 水平名称
        /// </summary>
        public string HorizontalName
        {
            get { return horizontalName; }
            set { horizontalName = value; }
        }

        /** 矿井 **/
        private MineEntity mine;

        /// <summary>
        /// 矿井
        /// </summary>
        public MineEntity Mine
        {
            get { return mine; }
            set { mine = value; }
        }
    }
}