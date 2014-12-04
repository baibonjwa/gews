using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibEntity.CommonEnt
{
    public class PowerCodeElementEntity
    {
        /// <summary>
        /// 在权限代码中的位置
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// 权限位名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 程序集
        /// </summary>
        public String Assembly { get; set; }

        /// <summary>
        /// 命名空间和子类名称，格式："命名空间名.子类名"
        /// </summary>
        public String ClassName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public String Method { get; set; }

        /// <summary>
        /// 该权限位的描述
        /// </summary>
        public String Description { get; set; }

    }
}
