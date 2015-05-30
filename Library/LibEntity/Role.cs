using System;

namespace LibEntity.CommonEnt
{
    public class RoleEntity
    {
        /// <summary>
        ///     角色Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     角色名称
        /// </summary>
        public String RoleName { get; set; }

        /// <summary>
        ///     权限代码
        /// </summary>
        public String PowerCode { get; set; }

        /// <summary>
        ///     角色备注
        /// </summary>
        public String Remarks { get; set; }
    }
}