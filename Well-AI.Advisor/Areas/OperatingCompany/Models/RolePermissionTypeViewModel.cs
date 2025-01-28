using System;
using System.Collections.Generic;

namespace WebAI.Advisor.Areas.OperatingCompany.Models
{
    public class RolePermissionTypeViewModel
    {
        public int Value { get; set; }
        //description of checkbox 
        public string Text { get; set; }
        //whether the checkbox is selected or not
        public bool IsChecked { get; set; }
    }

    public class RolePermissionTypeViewModelList
    {
        public string Name { get; set; }
        public string PermissionName { get; set; }
    }
}
