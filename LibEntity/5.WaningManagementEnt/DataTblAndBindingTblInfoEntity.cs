// ******************************************************************
// 概  述：预警数据字段与规则编码对应关系表信息实体
// 作  者：杨小颖  
// 创建日期：2014/01/14
// 版本号：1.0
// ******************************************************************
using System;


namespace LibEntity
{
    public class DataTblAndBindingTblInfoEntity
    {

        private string _dataTableName = "";

        //预警数据表名称
        public string DataTableName
        {
            get { return _dataTableName; }
            set { _dataTableName = value; }
        }

        private string _bindingTableName = "";

        //预警数据字段与规则编码对应关系表名称
        public string BindingTableName
        {
            get { return _bindingTableName; }
            set { _bindingTableName = value; }
        }

        private bool _needConstrains = true;

        //是否需要考虑巷道ID、时间等约束条件
        public bool NeedConstrains
        {
            get { return _needConstrains; }
            set { _needConstrains = value; }
        }
    }
}
