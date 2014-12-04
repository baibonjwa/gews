// ******************************************************************
// 概  述：井下数据瓦斯信息实体
// 作  者：宋英杰
// 创建日期：2014/4/15
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
    public class GasDataEntity:MineDataEntity
    {
        //瓦斯探头断电次数
        private double powerFailure;
        /// <summary>
        /// 设置或获取瓦斯探头断电次数
        /// </summary>
        public double PowerFailure
        {
            get { return powerFailure; }
            set { powerFailure = value; }
        }
        //吸钻预兆次数
        private double drillTimes;
        /// <summary>
        /// 设置或获取吸钻预兆次数
        /// </summary>
        public double DrillTimes
        {
            get { return drillTimes; }
            set { drillTimes = value; }
        }
        //瓦斯忽大忽小预兆次数
        private double gasTimes;
        //设置或获取瓦斯忽大忽小预兆次数
        public double GasTimes
        {
            get { return gasTimes; }
            set { gasTimes = value; }
        }
        //气温下降预兆次数
        private double tempDownTimes;
        /// <summary>
        /// 设置或获取气温下降预兆次数
        /// </summary>
        public double TempDownTimes
        {
            get { return tempDownTimes; }
            set { tempDownTimes = value; }
        }
        //煤炮频繁预兆次数
        private double coalBangTimes;
        //设置或获取煤炮频繁预兆次数
        public double CoalBangTimes
        {
            get { return coalBangTimes; }
            set { coalBangTimes = value; }
        }
        //喷孔次数
        private double craterTimes;
        //设置或获取喷孔次数
        public double CraterTimes
        {
            get { return craterTimes; }
            set { craterTimes = value; }
        }
        //顶钻次数
        private double stoperTimes;
        /// <summary>
        /// 设置或获取顶钻次数
        /// </summary>
        public double StoperTimes
        {
            get { return stoperTimes; }
            set { stoperTimes = value; }
        }

        //瓦斯浓度
        private double gasThickness;

        /// <summary>
        /// 瓦斯浓度
        /// </summary>
        public double GasThickness
        {
            get { return gasThickness; }
            set { gasThickness = value; }
        }
    }
}
