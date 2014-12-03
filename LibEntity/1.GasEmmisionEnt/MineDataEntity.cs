// ******************************************************************
// 概  述：
// 作  者：
// 日  期：
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
    public class MineDataEntity
    {
        // ID
        private int id;

        /// <summary>
        /// 设置或获取ID
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        // 巷道编号
        private int tunnelID;

        /// <summary>
        /// 设置或获取巷道编号
        /// </summary>
        public int TunnelID
        {
            get { return tunnelID; }
            set { tunnelID = value; }
        }

        // 坐标X
        private double coordinateX;

        /// <summary>
        /// 设置或获取坐标X
        /// </summary>
        public double CoordinateX
        {
            get { return coordinateX; }
            set { coordinateX = value; }
        }

        // 坐标Y
        private double coordinateY;

        /// <summary>
        /// 设置或获取坐标Y
        /// </summary>
        public double CoordinateY
        {
            get { return coordinateY; }
            set { coordinateY = value; }
        }

        // 坐标Z
        private double coordinateZ;

        /// <summary>
        /// 设置或获取坐标Z
        /// </summary>
        public double CoordinateZ
        {
            get { return coordinateZ; }
            set { coordinateZ = value; }
        }

        // 时间
        private DateTime datetime;

        /// <summary>
        /// 设置或获取时间
        /// </summary>
        public DateTime Datetime
        {
            get { return datetime; }
            set { datetime = value; }
        }

        // 工作制式
        private string workStyle;

        /// <summary>
        /// 设置或获取工作制式
        /// </summary>
        public string WorkStyle
        {
            get { return workStyle; }
            set { workStyle = value; }
        }

        // 班次
        private string workTime;

        /// <summary>
        /// 设置或获取班次
        /// </summary>
        public string WorkTime
        {
            get { return workTime; }
            set { workTime = value; }
        }

        // 队别名称
        private string teamName;

        /// <summary>
        /// 设置或获取队别名称
        /// </summary>
        public string TeamName
        {
            get { return teamName; }
            set { teamName = value; }
        }

        // 填报人
        private string submitter;

        /// <summary>
        /// 设置或获取填报人
        /// </summary>
        public string Submitter
        {
            get { return submitter; }
            set { submitter = value; }
        }

        /// <summary>
        /// 转换为CoalExistenceEntity
        /// </summary>
        /// <returns></returns>
        public CoalExistenceEntity changeToCoalExistenceEntity()
        {
            CoalExistenceEntity ceEntity = new CoalExistenceEntity();
            ceEntity.Id = Id;
            ceEntity.TunnelID = TunnelID;
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
        /// 转换为VentilationInfoEntity
        /// </summary>
        /// <returns></returns>
        public VentilationInfoEntity changeToVentilationInfoEntity()
        {
            VentilationInfoEntity viEntity = new VentilationInfoEntity();
            viEntity.Id = Id;
            viEntity.TunnelID = TunnelID;
            viEntity.CoordinateX = CoordinateX;
            viEntity.CoordinateY = CoordinateY;
            viEntity.CoordinateZ = CoordinateZ;
            viEntity.Datetime = Datetime;
            viEntity.WorkStyle = WorkStyle;
            viEntity.WorkTime = WorkTime;
            viEntity.TeamName = TeamName;
            viEntity.Submitter = Submitter;
            return viEntity;
        }

        /// <summary>
        /// 转换为GasDataEntity
        /// </summary>
        /// <returns></returns>
        public GasDataEntity changeToGasDataEntity()
        {
            GasDataEntity gdEntity = new GasDataEntity();
            gdEntity.Id = Id;
            gdEntity.TunnelID = TunnelID;
            gdEntity.CoordinateX = CoordinateX;
            gdEntity.CoordinateY = CoordinateY;
            gdEntity.CoordinateZ = CoordinateZ;
            gdEntity.Datetime = Datetime;
            gdEntity.WorkStyle = WorkStyle;
            gdEntity.WorkTime = WorkTime;
            gdEntity.TeamName = TeamName;
            gdEntity.Submitter = Submitter;
            return gdEntity;
        }

        /// <summary>
        /// 转换为ManagementEntity
        /// </summary>
        /// <returns></returns>
        public ManagementEntity changeToManagementEntity()
        {
            ManagementEntity mEntity = new ManagementEntity();
            mEntity.Id = Id;
            mEntity.TunnelID = TunnelID;
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
        /// 转换为UsualForecastEntity
        /// </summary>
        /// <returns></returns>
        public UsualForecastEntity changeToUsualForecastEntity()
        {
            UsualForecastEntity ufEntity = new UsualForecastEntity();
            ufEntity.Id = Id;
            ufEntity.TunnelID = TunnelID;
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
        /// 软换为GeologicStructureEntity
        /// </summary>
        /// <returns></returns>
        public GeologicStructureEntity changeToGeologicStructureEntity()
        {
            GeologicStructureEntity geoLogicStructure = new GeologicStructureEntity();
            geoLogicStructure.Id = Id;
            geoLogicStructure.TunnelID = TunnelID;
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
