// ******************************************************************
// 概  述：
// 作  者：
// 日  期：
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************

using System;
using Castle.ActiveRecord;

namespace LibEntity
{
    public class MineDataEntity
    {
        // ID

        /// <summary>
        ///     设置或获取ID
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Identity, "ID")]
        public int Id { get; set; }

        // 巷道编号

        /// <summary>
        ///     设置或获取巷道编号
        /// </summary>
        [BelongsTo("TUNNEL_ID")]
        public Tunnel Tunnel { get; set; }

        // 坐标X

        /// <summary>
        ///     设置或获取坐标X
        /// </summary>
        public double CoordinateX { get; set; }

        // 坐标Y

        /// <summary>
        ///     设置或获取坐标Y
        /// </summary>
        public double CoordinateY { get; set; }

        // 坐标Z

        /// <summary>
        ///     设置或获取坐标Z
        /// </summary>
        public double CoordinateZ { get; set; }

        // 时间

        /// <summary>
        ///     设置或获取时间
        /// </summary>
        public DateTime Datetime { get; set; }

        // 工作制式

        /// <summary>
        ///     设置或获取工作制式
        /// </summary>
        public string WorkStyle { get; set; }

        // 班次

        /// <summary>
        ///     设置或获取班次
        /// </summary>
        public string WorkTime { get; set; }

        // 队别名称

        /// <summary>
        ///     设置或获取队别名称
        /// </summary>
        public string TeamName { get; set; }

        // 填报人

        /// <summary>
        ///     设置或获取填报人
        /// </summary>
        public string Submitter { get; set; }

        /// <summary>
        ///     转换为CoalExistenceEntity
        /// </summary>
        /// <returns></returns>
        public CoalExistenceEntity changeToCoalExistenceEntity()
        {
            var ceEntity = new CoalExistenceEntity();
            ceEntity.Id = Id;
            ceEntity.Tunnel = Tunnel;
            ceEntity.CoordinateX = CoordinateX;
            ceEntity.CoordinateY = CoordinateY;
            ceEntity.CoordinateZ = CoordinateZ;
            ceEntity.Datetime = Datetime;
            ceEntity.WorkStyle = WorkStyle;
            ceEntity.WorkTime = WorkTime;
            ceEntity.TeamName = TeamName;
            ceEntity.Submitter = Submitter;
            return ceEntity;
        }

        /// <summary>
        ///     转换为VentilationInfoEntity
        /// </summary>
        /// <returns></returns>
        public VentilationInfoEntity ChangeToVentilationInfoEntity()
        {
            var viEntity = new VentilationInfoEntity
            {
                Id = Id,
                Tunnel = Tunnel,
                CoordinateX = CoordinateX,
                CoordinateY = CoordinateY,
                CoordinateZ = CoordinateZ,
                Datetime = Datetime,
                WorkStyle = WorkStyle,
                WorkTime = WorkTime,
                TeamName = TeamName,
                Submitter = Submitter
            };
            return viEntity;
        }

        /// <summary>
        ///     转换为GasDataEntity
        /// </summary>
        /// <returns></returns>
        public GasDataEntity ChangeToGasDataEntity()
        {
            var gdEntity = new GasDataEntity
            {
                Id = Id,
                Tunnel = Tunnel,
                CoordinateX = CoordinateX,
                CoordinateY = CoordinateY,
                CoordinateZ = CoordinateZ,
                Datetime = Datetime,
                WorkStyle = WorkStyle,
                WorkTime = WorkTime,
                TeamName = TeamName,
                Submitter = Submitter
            };
            return gdEntity;
        }

        /// <summary>
        ///     转换为ManagementEntity
        /// </summary>
        /// <returns></returns>
        public ManagementEntity changeToManagementEntity()
        {
            var mEntity = new ManagementEntity();
            mEntity.Id = Id;
            mEntity.Tunnel = Tunnel;
            mEntity.CoordinateX = CoordinateX;
            mEntity.CoordinateY = CoordinateY;
            mEntity.CoordinateZ = CoordinateZ;
            mEntity.Datetime = Datetime;
            mEntity.WorkStyle = WorkStyle;
            mEntity.WorkTime = WorkTime;
            mEntity.TeamName = TeamName;
            mEntity.Submitter = Submitter;
            return mEntity;
        }

        /// <summary>
        ///     转换为UsualForecastEntity
        /// </summary>
        /// <returns></returns>
        public UsualForecastEntity changeToUsualForecastEntity()
        {
            var ufEntity = new UsualForecastEntity();
            ufEntity.Id = Id;
            ufEntity.Tunnel = Tunnel;
            ufEntity.CoordinateX = CoordinateX;
            ufEntity.CoordinateY = CoordinateY;
            ufEntity.CoordinateZ = CoordinateZ;
            ufEntity.Datetime = Datetime;
            ufEntity.WorkStyle = WorkStyle;
            ufEntity.WorkTime = WorkTime;
            ufEntity.TeamName = TeamName;
            ufEntity.Submitter = Submitter;
            return ufEntity;
        }

        /// <summary>
        ///     软换为GeologicStructureEntity
        /// </summary>
        /// <returns></returns>
        public GeologicStructureEntity changeToGeologicStructureEntity()
        {
            var geoLogicStructure = new GeologicStructureEntity();
            geoLogicStructure.Id = Id;
            geoLogicStructure.Tunnel = Tunnel;
            geoLogicStructure.CoordinateX = CoordinateX;
            geoLogicStructure.CoordinateY = CoordinateY;
            geoLogicStructure.CoordinateZ = CoordinateZ;
            geoLogicStructure.Datetime = Datetime;
            geoLogicStructure.WorkStyle = WorkStyle;
            geoLogicStructure.WorkTime = WorkTime;
            geoLogicStructure.TeamName = TeamName;
            geoLogicStructure.Submitter = Submitter;
            return geoLogicStructure;
        }
    }
}