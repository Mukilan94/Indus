
using System.Collections.Generic;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class ComponentModelRec
    {
        public int ComponentId;
        public string ComponentName;
        public bool IsPermitted;
        public int? SrvAccountType;
        public int? AccountType;
    }

    public class ComponentModelRow
    {
        public int? id { get; set; }
        public string title { get; set; }
    }

    public class ComponentRowModel
    {
        public int ComponentId;
        public string ComponentName;
        public bool IsPermitted;       
        public List<UserAction> Components;
    }
}
