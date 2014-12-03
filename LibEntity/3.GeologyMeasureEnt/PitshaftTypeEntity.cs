// ******************************************************************
// 概  述：井筒类型实体
// 作  者：伍鑫
// 创建日期：2014/03/06
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGeometry;

namespace LibEntity
{
    public class PitshaftTypeEntity
    {
        /** 井筒类型编号 **/
        private int pitshaftTypeId;

        /// <summary>
        /// 井筒类型编号
        /// </summary>
        public int PitshaftTypeId
        {
            get { return pitshaftTypeId; }
            set { pitshaftTypeId = value; }
        }

        /** 井筒类型名称 **/
        private string pitshaftTypeName;

        /// <summary>
        /// 井筒类型名称
        /// </summary>
        public string PitshaftTypeName
        {
            get { return pitshaftTypeName; }
            set { pitshaftTypeName = value; }
        }
    }
}
