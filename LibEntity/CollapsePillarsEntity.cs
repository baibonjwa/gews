// ******************************************************************
// 概  述：陷落柱实体
// 作  者：宋英杰
// 创建日期：2013/11/28
// 版本号：1.0
// ******************************************************************

namespace LibEntity
{
    public class CollapsePillarsEntity
    {

        /**陷落柱编号**/
        public int CollapsePillarsId { get; set; }

        /**陷落柱名称**/
        public string CollapsePillarsName { get; set; }

        /**位置X**/
        public string CollapsePillarsLocationX { get; set; }

        /**位置Y**/
        public string CollapsePillarsLocationY { get; set; }


        /**位置Z**/
        public string CollapsePillarsLocationZ { get; set; }

        /**陷落柱长轴长**/
        public string CollapsePillarsLongAxisLength { get; set; }

        /**陷落柱短轴长**/
        public string CollapsePillarsShortAxialLength { get; set; }

        /**陷落柱描述**/
        public string CollapsePillarsDescribe { get; set; }
    }
}