using LibEntity;

namespace LibBusiness
{
    public class TunnelUtils
    {
        /// <summary>
        /// 判断巷道是否属于回采面
        /// </summary>
        /// <param name="tunnelType"></param>
        /// <returns></returns>
        public static bool IsStoping(TunnelTypeEnum tunnelType)
        {
            return tunnelType == TunnelTypeEnum.STOPING_FY ||
                   tunnelType == TunnelTypeEnum.STOPING_ZY ||
                   tunnelType == TunnelTypeEnum.STOPING_QY ||
                   tunnelType == TunnelTypeEnum.STOPING_OTHER;
        }

    }
}
