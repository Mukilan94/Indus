using System;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class RolePermissionTypeViewModel
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }

    public class RolePermissionTypeViewModelList
    {
        public string Name { get; set; }
        public string PermissionName { get; set; }
    }
}
