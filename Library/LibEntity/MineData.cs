using System;
using System.Collections;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace LibEntity
{
    public class MineData : ActiveRecordBase
    {
        private Tunnel _tunnel = new Tunnel();// ID
        private DateTime _datetime;

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
        public Tunnel Tunnel
        {
            get { return _tunnel; }
            set { _tunnel = value; }
        }

        // 坐标X

        /// <summary>
        ///     设置或获取坐标X
        /// </summary>
        [Property("COORDINATE_X")]
        public double CoordinateX { get; set; }

        // 坐标Y

        /// <summary>
        ///     设置或获取坐标Y
        /// </summary>
        [Property("COORDINATE_Y")]
        public double CoordinateY { get; set; }

        // 坐标Z

        /// <summary>
        ///     设置或获取坐标Z
        /// </summary>
        [Property("COORDINATE_Z")]
        public double CoordinateZ { get; set; }

        // 时间

        /// <summary>
        ///     设置或获取时间
        /// </summary>
        [Property("DATETIME")]
        public DateTime Datetime
        {
            get { return _datetime; }
            set { _datetime = value; }
        }

        // 工作制式

        /// <summary>
        ///     设置或获取工作制式
        /// </summary>
        [Property("WORK_STYLE")]
        public string WorkStyle { get; set; }

        // 班次

        /// <summary>
        ///     设置或获取班次
        /// </summary>
        [Property("WORK_TIME")]
        public string WorkTime { get; set; }

        // 队别名称

        /// <summary>
        ///     设置或获取队别名称
        /// </summary>
        [Property("TEAM_NAME")]
        public string TeamName { get; set; }

        // 填报人

        /// <summary>
        ///     设置或获取填报人
        /// </summary>
        [Property("SUBMITTER")]
        public string Submitter { get; set; }

        /// <summary>
        ///     转换为CoalExistenceEntity
        /// </summary>
        /// <returns></returns>
        public CoalExistence changeToCoalExistenceEntity()
        {
            var ceEntity = new CoalExistence();
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
        public Ventilation ChangeToVentilationInfoEntity()
        {
            var viEntity = new Ventilation
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
        public GasData ChangeToGasDataEntity()
        {
            var gdEntity = new GasData
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
        public Management changeToManagementEntity()
        {
            var mEntity = new Management();
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
        public UsualForecast changeToUsualForecastEntity()
        {
            var ufEntity = new UsualForecast();
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
        public GeologicStructure changeToGeologicStructureEntity()
        {
            var geoLogicStructure = new GeologicStructure();
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


        public static void DeleteByTunnelId<T>(int tunnelId) where T : MineData
        {
            var criterion = new ICriterion[]
            {
                Restrictions.Eq("Tunnel.TunnelId", tunnelId)
            };
            foreach (var i in FindAll(typeof(T), criterion))
            {
                Delete(i);
            }
        }

    }
}