using System;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{

    public class RowValue
    {
        public string PatientName { get; set; }
        public decimal OrderAmount { get; set; }
        public int PatientAge { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class ColumnMetaDataInfo
    {
        public string FieldName { get; set; }
        public int Width { get; set; }
        public string Caption { get; set; }
        public string Format { get; set; }
        public string Align { get; set; }
        public bool Display { get; set; }
        public Type DataType { get; set; }
    }

    public class GridViewModel
    {
        public List<ColumnMetaDataInfo> Columns { get; set; }
        public List<RowValue> RowValues { get; set; }
    }

    public class RolePermissionModel
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public bool IsPermitted { get; set; }
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public List<RolePermissionComponentModel> RolePermissionComponent { get; set; }
    }
}
