using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibEntity;

namespace LibBusiness
{
    public class TunnelUtils
    {
        /// <summary>
        /// 判断巷道是否属于回采面
        /// </summary>
        /// <param name="sTunnelType"></param>
        /// <returns></returns>
        public static bool isStoping(TunnelTypeEnum tunnelType)
        {
            if (tunnelType == TunnelTypeEnum.STOPING_FY ||
                tunnelType == TunnelTypeEnum.STOPING_ZY ||
                tunnelType == TunnelTypeEnum.STOPING_QY ||
                tunnelType == TunnelTypeEnum.STOPING_OTHER
                )
            {
                return true;
            }

            return false;
        }

        //public static Dictionary<TunnelTypeEnum, Tunnel> getTunnelDict(WorkingFace wf)
        //{
        //    Dictionary<TunnelTypeEnum, Tunnel> dReturn = new Dictionary<TunnelTypeEnum, Tunnel>();
        //    if (wf.TunnelSet != null)
        //    {
        //        foreach (Tunnel entity in wf.TunnelSet)
        //        {
        //            if (entity.TunnelType == TunnelTypeEnum.STOPING_ZY) //主运
        //                dReturn.Add(TunnelTypeEnum.STOPING_ZY, entity);
        //            if (entity.TunnelType == TunnelTypeEnum.STOPING_FY) // 辅运
        //                dReturn.Add(TunnelTypeEnum.STOPING_FY, entity);
        //            if (entity.TunnelType == TunnelTypeEnum.STOPING_QY) // 切眼
        //                dReturn.Add(TunnelTypeEnum.STOPING_QY, entity);
        //        }
        //    }

        //    return dReturn;
        //}


    }
}
