// ******************************************************************
// 概  述：绑定的表实体
// 作  者：杨小颖  
// 创建日期：2014/01/14
// 版本号：1.0
// ******************************************************************
using System;


namespace LibEntity
{
    public enum COLUMN_USE_MANNER
    {
        DIRECT_USE,//直接使用
        CALC_NEAREST_DIS,//点到曲线的最近距离
        CALC_VALUE,//计算值
    }

    public class BindingTableEntity
    {

        private string _columnName;

        //字段名（列名）
        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        private string _bindingWarningRules;

        //绑定的规则编码
        public string BindingWarningRules
        {
            get { return _bindingWarningRules; }
            set { _bindingWarningRules = value; }
        }

        private COLUMN_USE_MANNER _useManner;

        //字段使用方式
        public COLUMN_USE_MANNER UseManner
        {
            get { return _useManner; }
            set { _useManner = value; }
        }
    }
}
