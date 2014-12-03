using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibEntity;
using LibBusiness;
using System.Data;
using LibCommon;

namespace LibBusiness
{
    /// <summary>
    /// 基本信息，矿井，水平，采区，工作面，巷道。
    /// 原则上，信息比较少的矿井，水平，采区，工作面的信息都是一次加载。
    /// 信息比较多的，例如巷道，逐条加载。
    /// 
    /// 巷道信息的缓存机制：原则上，在内存中存放10条矿井信息，更加使用频率排序。
    /// </summary>
    public class BasicInfoManager
    {
        private BasicInfoManager() { }
        private static BasicInfoManager instance = null;
        // 矿井
        public Dictionary<int, MineEntity> mineList = new Dictionary<int, MineEntity>();
        // 水平
        private Dictionary<int, HorizontalEntity> horizList = new Dictionary<int, HorizontalEntity>();

        // 采区
        private Dictionary<int, MiningAreaEntity> maList = new Dictionary<int, MiningAreaEntity>();

        // 工作面
        private Dictionary<int, WorkingFaceEntity> wfList = new Dictionary<int, WorkingFaceEntity>();

        // 巷道
        private Dictionary<int, TunnelEntity> tunnelList = new Dictionary<int, TunnelEntity>();

        // 煤层
        private Dictionary<int, CoalSeamsEntity> coalSeamList = new Dictionary<int, CoalSeamsEntity>();

        // 岩性
        private Dictionary<int, LithologyEntity> lithologyList = new Dictionary<int, LithologyEntity>();

        // 对别
        private Dictionary<int, TeamInfoEntity> teamList = new Dictionary<int, TeamInfoEntity>();

        public static BasicInfoManager getInstance()
        {
            if (null == instance)
            {
                instance = new BasicInfoManager();
                instance.init();
            }
            return instance;
        }

        private void init()
        {
            refreshMineInfo();
            refreshHorizontalInfo();
            refreshMiningAreaInfo();
            refreshWorkingFaceInfo();

            refreshCoalSeamsInfo();
            refreshLithologyInfo();
            refreshTeamInfo();

            refreshTunnelInfo(-1);
        }


        /// <summary>
        /// 加载更新矿信息
        /// </summary>
        public void refreshMineInfo()
        {
            mineList.Clear();
            // 
            DataSet ds = MineBLL.selectAllMineInfo();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                MineEntity entity = new MineEntity();
                entity.MineId = Convert.ToInt32(dr[MineDbConstNames.MINE_ID]);
                entity.MineName = dr[MineDbConstNames.MINE_NAME].ToString();
                mineList.Add(entity.MineId, entity);
            }
        }

        public MineEntity getMineById(int mineId)
        {
            try
            {
                return mineList[mineId];
            }
            catch (KeyNotFoundException e)
            {
                Log.Warn("[Get Mine]" + e.ToString());
                return null;
            }
        }

        public string getMineNameById(int mineId)
        {
            MineEntity entity = getMineById(mineId);
            if (null != entity)
                return entity.MineName;
            else
                return string.Empty;
        }

        /// <summary>
        /// 加载更新水平信息
        /// </summary>
        public void refreshHorizontalInfo()
        {
            horizList.Clear();

            DataSet ds = HorizontalBLL.selectAllHorizontalInfo();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                HorizontalEntity entity = new HorizontalEntity();
                entity.HorizontalId = Convert.ToInt32(dr[HorizontalDbConstNames.HORIZONTAL_ID]);
                entity.HorizontalName = dr[HorizontalDbConstNames.HORIZONTAL_NAME].ToString();
                entity.Mine = getMineById(Convert.ToInt32(dr[HorizontalDbConstNames.MINE_ID]));
                horizList.Add(entity.HorizontalId, entity);
            }
        }

        public string getHorizontalNameById(int horizontalId)
        {
            HorizontalEntity entity = getHorizontalById(horizontalId);
            if (null != entity)
                return entity.HorizontalName;
            else
                return string.Empty;
        }

        public HorizontalEntity getHorizontalById(int horizontalId)
        {
            try
            {
                return horizList[horizontalId];
            }
            catch (KeyNotFoundException e)
            {
                Log.Warn("" + e.ToString());
                return null;
            }
        }

        /// <summary>
        /// 加载采区信息
        /// </summary>
        public void refreshMiningAreaInfo()
        {
            maList.Clear();

            DataSet ds = MiningAreaBLL.selectAllMiningAreaInfo();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                MiningAreaEntity entity = new MiningAreaEntity();
                entity.MiningAreaId = Convert.ToInt32(dr[MiningAreaDbConstNames.MININGAREA_ID]);
                entity.MiningAreaName = dr[MiningAreaDbConstNames.MININGAREA_NAME].ToString();
                entity.Horizontal = getHorizontalById(Convert.ToInt32(dr[MiningAreaDbConstNames.HORIZONTAL_ID]));
                maList.Add(entity.MiningAreaId, entity);
            }
        }

        /// <summary>
        /// 获取采区名称
        /// </summary>
        /// <param name="miningAreaId"></param>
        /// <returns></returns>
        public string getMiningAreaNameById(int miningAreaId)
        {
            MiningAreaEntity entity = getMiningAreaById(miningAreaId);
            if (null != entity)
                return entity.MiningAreaName;
            else
                return string.Empty;
        }

        /// <summary>
        /// 获取采区实体
        /// </summary>
        /// <param name="miningAreaId"></param>
        /// <returns></returns>
        public MiningAreaEntity getMiningAreaById(int miningAreaId)
        {
            try
            {
                return maList[miningAreaId];
            }
            catch (KeyNotFoundException e)
            {
                Log.Warn("[Get Mining Area]: mining area id= " + miningAreaId + ", " + e.ToString());
                return null;
            }
        }

        /// <summary>
        /// 更新指定工作面信息
        /// </summary>
        public void refreshWorkingFaceInfo(WorkingFaceEntity entity)
        {
            // 加载工作面信息
            DataSet ds = WorkingFaceBLL.selectWorkingFaceInfoByWorkingFaceId(entity.WorkingFaceID);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                entity.WorkingFaceName = dr[WorkingFaceDbConstNames.WORKINGFACE_NAME] == DBNull.Value ? string.Empty : dr[WorkingFaceDbConstNames.WORKINGFACE_NAME].ToString();

                // 采区
                entity.MiningArea = getMiningAreaById(
                    dr[WorkingFaceDbConstNames.MININGAREA_ID] == DBNull.Value ? Const.INVALID_ID : Convert.ToInt32(dr[WorkingFaceDbConstNames.MININGAREA_ID].ToString())
                    );

                double x = dr[WorkingFaceDbConstNames.COORDINATE_X] == DBNull.Value ? 0.0 : Convert.ToDouble(dr[WorkingFaceDbConstNames.COORDINATE_X]);
                double y = dr[WorkingFaceDbConstNames.COORDINATE_Y] == DBNull.Value ? 0.0 : Convert.ToDouble(dr[WorkingFaceDbConstNames.COORDINATE_Y]);
                double z = dr[WorkingFaceDbConstNames.COORDINATE_Z] == DBNull.Value ? 0.0 : Convert.ToDouble(dr[WorkingFaceDbConstNames.COORDINATE_Z]);
                entity.Coordinate = new CoordinateEntity(x, y, z);

                entity.StartDate = dr[WorkingFaceDbConstNames.START_DATE] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr[WorkingFaceDbConstNames.START_DATE].ToString());
                entity.StopDate = dr[WorkingFaceDbConstNames.STOP_DATE] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr[WorkingFaceDbConstNames.STOP_DATE].ToString());
                entity.IsFinish = dr[WorkingFaceDbConstNames.IS_FINISH] == DBNull.Value ? 0 : Convert.ToInt32(dr[WorkingFaceDbConstNames.IS_FINISH]);
                entity.TeamNameID = dr[WorkingFaceDbConstNames.TEAM_NAME_ID] == DBNull.Value ? Const.INVALID_ID : Convert.ToInt32(dr[WorkingFaceDbConstNames.TEAM_NAME_ID].ToString());
            }
        }

        /// <summary>
        /// 更新工作面信息
        /// </summary>
        public void refreshWorkingFaceInfo()
        {
            wfList.Clear();

            // 加载工作面信息
            DataSet ds = WorkingFaceBLL.selectAllWorkingFace();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                WorkingFaceEntity entity = new WorkingFaceEntity();

                entity.WorkingFaceID = dr[WorkingFaceDbConstNames.WORKINGFACE_ID] == DBNull.Value ? Const.INVALID_ID : Convert.ToInt32(dr[WorkingFaceDbConstNames.WORKINGFACE_ID].ToString());
                entity.WorkingFaceName = dr[WorkingFaceDbConstNames.WORKINGFACE_NAME] == DBNull.Value ? string.Empty : dr[WorkingFaceDbConstNames.WORKINGFACE_NAME].ToString();

                // 采区
                entity.MiningArea = getMiningAreaById(
                    dr[WorkingFaceDbConstNames.MININGAREA_ID] == DBNull.Value ? Const.INVALID_ID : Convert.ToInt32(dr[WorkingFaceDbConstNames.MININGAREA_ID].ToString())
                    );

                double x = dr[WorkingFaceDbConstNames.COORDINATE_X] == DBNull.Value ? 0.0 : Convert.ToDouble(dr[WorkingFaceDbConstNames.COORDINATE_X]);
                double y = dr[WorkingFaceDbConstNames.COORDINATE_Y] == DBNull.Value ? 0.0 : Convert.ToDouble(dr[WorkingFaceDbConstNames.COORDINATE_Y]);
                double z = dr[WorkingFaceDbConstNames.COORDINATE_Z] == DBNull.Value ? 0.0 : Convert.ToDouble(dr[WorkingFaceDbConstNames.COORDINATE_Z]);
                entity.Coordinate = new CoordinateEntity(x, y, z);

                entity.StartDate = dr[WorkingFaceDbConstNames.START_DATE] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr[WorkingFaceDbConstNames.START_DATE].ToString());
                entity.StopDate = dr[WorkingFaceDbConstNames.STOP_DATE] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr[WorkingFaceDbConstNames.STOP_DATE].ToString());
                entity.IsFinish = dr[WorkingFaceDbConstNames.IS_FINISH] == DBNull.Value ? 0 : Convert.ToInt32(dr[WorkingFaceDbConstNames.IS_FINISH]);
                entity.TeamNameID = dr[WorkingFaceDbConstNames.TEAM_NAME_ID] == DBNull.Value ? Const.INVALID_ID : Convert.ToInt32(dr[WorkingFaceDbConstNames.TEAM_NAME_ID].ToString());
                entity.WorkingfaceTypeEnum = dr[WorkingFaceDbConstNames.WORKINGFACE_TYPE] == DBNull.Value ? WorkingfaceTypeEnum.OTHER : (WorkingfaceTypeEnum)dr[WorkingFaceDbConstNames.WORKINGFACE_TYPE];

                wfList.Add(entity.WorkingFaceID, entity);
            }
        }

        /// <summary>
        /// 根据工作面面id，获取工作面实体
        /// </summary>
        /// <param name="workingFaceId">工作面id</param>
        /// <returns>工作面实体</returns>
        public WorkingFaceEntity getWorkingFaceById(int workingFaceId)
        {
            try
            {
                return wfList[workingFaceId];
            }
            catch (KeyNotFoundException e)
            {
                Log.Warn("[Get Working Face]: 字典索引错误：" + e.ToString());
                return null;
            }
        }

        /// <summary>
        /// 根据工作面名称，获取工作面实体
        /// </summary>
        /// <param name="workingFaceName"></param>
        /// <returns></returns>
        public WorkingFaceEntity getWorkingFaceByName(string workingFaceName)
        {
            foreach (WorkingFaceEntity entity in wfList.Values)
            {
                if (string.Equals(entity.WorkingFaceName, workingFaceName))
                    return entity;
            }

            return null;
        }

        /// <summary>
        /// 更新煤层信息
        /// </summary>
        public void refreshCoalSeamsInfo()
        {
            coalSeamList.Clear();

            DataSet ds = CoalSeamsBLL.selectAllCoalSeamsInfo();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                CoalSeamsEntity entity = new CoalSeamsEntity();
                entity.CoalSeamsId = Convert.ToInt32(dr[CoalSeamsDbConstNames.COAL_SEAMS_ID]);
                entity.CoalSeamsName = dr[CoalSeamsDbConstNames.COAL_SEAMS_NAME].ToString();
                coalSeamList.Add(entity.CoalSeamsId, entity);
            }
        }

        public CoalSeamsEntity getCoalSeamById(int coalSeamId)
        {
            try
            {
                return coalSeamList[coalSeamId];
            }
            catch (KeyNotFoundException e)
            {
                Log.Warn("[Get Team]: 字典索引错误：" + e.ToString());
                return null;
            }

        }

        public string getCoalSeamNameById(int coalSeamId)
        {
            CoalSeamsEntity entity = getCoalSeamById(coalSeamId);
            if (null != entity)
                return entity.CoalSeamsName;
            else
                return string.Empty;
        }

        /// <summary>
        /// 获取岩性信息
        /// </summary>
        public void refreshLithologyInfo()
        {
            lithologyList.Clear();

            DataSet ds = LithologyBLL.selectAllLithologyInfo();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                LithologyEntity entity = new LithologyEntity();
                entity.LithologyId = Convert.ToInt32(dr[LithologyDbConstNames.LITHOLOGY_ID]);
                entity.LithologyName = dr[LithologyDbConstNames.LITHOLOGY_NAME].ToString();
                lithologyList.Add(entity.LithologyId, entity);
            }
        }

        public LithologyEntity getLithologyById(int lighologyId)
        {
            try
            {
                return lithologyList[lighologyId];
            }
            catch (KeyNotFoundException e)
            {
                Log.Warn("[Get Lithology]: 字典索引错误：" + e.ToString());
                return null;
            }
        }

        public String getLithologyNameById(int lighologyId)
        {
            LithologyEntity entity = getLithologyById(lighologyId);

            if (null == entity)
                return "";
            else
                return entity.LithologyName;
        }

        /// <summary>
        /// 根据巷道Id， 更新巷道信息
        /// </summary>
        /// <param name="tunnelId">巷道id</param>
        public TunnelEntity refreshTunnelInfo(int tunnelId)
        {
            if (-1 == tunnelId)
                return null;

            DataSet ds = TunnelInfoBLL.selectOneTunnelInfoByTunnelID(tunnelId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                TunnelEntity entity = getTunnelEntityByDataRow(dr);
                return entity;
            }

            return null;
        }

        /// <summary>
        /// 将DataSet中的信息转化为TunnelEntity List.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public List<TunnelEntity> getTunnelListByDataSet(DataSet ds)
        {
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count <= 0)
            {
                Log.Warn("[Get Tunnel List Info]" + "数据集中没有数据");
                return null;
            }

            List<TunnelEntity> lReturn = new List<TunnelEntity>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TunnelEntity entity = getTunnelEntityByDataRow(dr);
                lReturn.Add(entity);
            }

            return lReturn;
        }

        /// <summary>
        /// 将DataSet中的信息转化为TunnelEntity List.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public HashSet<TunnelEntity> getTunnelSetByDataSet(DataSet ds)
        {
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count <= 0)
            {
                Log.Warn("[Get Tunnel List Info]" + "数据集中没有数据");
                return null;
            }

            HashSet<TunnelEntity> lReturn = new HashSet<TunnelEntity>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TunnelEntity entity = getTunnelEntityByDataRow(dr);
                lReturn.Add(entity);
            }

            return lReturn;
        }

        /// <summary>
        /// 将DataRow转化为巷道实体
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private TunnelEntity getTunnelEntityByDataRow(DataRow dr)
        {
            TunnelEntity entity = new TunnelEntity();
            // 巷道id
            entity.TunnelID = Convert.ToInt32(dr[TunnelInfoDbConstNames.ID]);
            // 巷道名称
            entity.TunnelName = dr[TunnelInfoDbConstNames.TUNNEL_NAME].ToString();
            // 巷道类型
            entity.TunnelType = (TunnelTypeEnum)Enum.Parse(typeof(TunnelTypeEnum), dr[TunnelInfoDbConstNames.TUNNEL_TYPE].ToString());
            // 巷道所属工作面
            entity.WorkingFace = getWorkingFaceById(Convert.ToInt32(dr[TunnelInfoDbConstNames.WORKINGFACE_ID]));
            // 巷道预警规则参数
            entity.TunnelParam = dr[TunnelInfoDbConstNames.PARAM].ToString();

            // 煤巷岩巷
            entity.CoalOrStone = dr[TunnelInfoDbConstNames.COAL_OR_STONE].ToString();

            // 岩性 //围岩类型
            entity.TunnelLithologyID = Convert.ToInt32(dr[TunnelInfoDbConstNames.LITHOLOGY_ID]);

            //设计长度
            entity.TunnelDesignLength = Convert.ToDouble(dr[TunnelInfoDbConstNames.DESIGNLENGTH]);

            //设计面积
            entity.TunnelDesignArea = Convert.ToDouble(dr[TunnelInfoDbConstNames.DESIGNAREA]);

            //支护方式
            entity.TunnelSupportPattern = dr[TunnelInfoDbConstNames.SUPPORT_PATTERN].ToString();

            //断面类型
            entity.TunnelSectionType = dr[TunnelInfoDbConstNames.SECTION_TYPE].ToString();

            //断面参数
            entity.TunnelParam = dr[TunnelInfoDbConstNames.PARAM].ToString();

            //绑定煤层
            entity.CoalLayerID = Convert.ToInt32(dr[TunnelInfoDbConstNames.COAL_LAYER_ID]);

            entity.BindingID = dr[TunnelInfoDbConstNames.BINDINGID].ToString();
            //绑定巷道宽度
            entity.TunnelWid = Convert.ToDouble(dr[TunnelInfoDbConstNames.TUNNEL_WID]);
            return entity;
        }

        /// <summary>
        /// 更加巷道Id，获取巷道实体
        /// </summary>
        /// <param name="tunnelId">巷道id</param>
        /// <returns>巷道实体</returns>
        public TunnelEntity getTunnelByID(int tunnelId)
        {
            TunnelEntity entity = null;

            try
            {
                entity = tunnelList[tunnelId];
                // 计数器增加
                tunnelList[tunnelId].UsedTimes += 1;
            }
            catch (KeyNotFoundException e)
            {
                entity = refreshTunnelInfo(tunnelId);
                if (entity != null)
                    entity.UsedTimes = 1;
            }

            return entity;
        }

        /// <summary>
        /// 更新队别信息
        /// </summary>
        public void refreshTeamInfo()
        {
            teamList.Clear();

            DataSet ds = TeamBLL.selectTeamInfo();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TeamInfoEntity entity = new TeamInfoEntity();
                entity.TeamID = Convert.ToInt32(dr[TeamDbConstNames.ID]);
                entity.TeamName = dr[TeamDbConstNames.TEAM_NAME].ToString();
                entity.TeamLeader = dr[TeamDbConstNames.TEAM_LEADER].ToString();
                entity.TeamMember = dr[TeamDbConstNames.TEAM_MEMBER].ToString();
                teamList.Add(entity.TeamID, entity);
            }
        }

        public TeamInfoEntity getTeamInfoByID(int teamId)
        {
            try
            {
                return teamList[teamId];
            }
            catch (KeyNotFoundException e)
            {
                Log.Warn("[Get Team]: 字典索引错误：" + e.ToString());
                return null;
            }
        }

        public String getTeamNameById(int teamId)
        {
            TeamInfoEntity entity = getTeamInfoByID(teamId);

            if (null == entity)
                return "";
            else
                return entity.TeamName;
        }

        public List<TunnelEntity> getTunnelListByWorkingFaceId(int wfId)
        {
            DataSet ds = TunnelInfoBLL.selectTunnelByWorkingFaceId(wfId);
            return getTunnelListByDataSet(ds);
        }

        public int getWorkingFaceCountByMiningAreaId(int miningAreaId)
        {
            DataSet ds = MiningAreaBLL.selectWorkingFaceListByMiningAreaId(miningAreaId);
            return ds.Tables[0].Rows.Count;
        }
    }
}
