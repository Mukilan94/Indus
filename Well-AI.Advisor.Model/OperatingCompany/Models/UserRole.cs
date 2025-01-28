using System.Collections.Generic;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class UserRole
    {
        public string Id;
        public string Title;      
        public List<UserAction> Permissions;        
    }

    public class UserPermission
    {
        public int? Id;
        public string Title; 
        public List<UserAction> Components;
    }

    public class UserAction
    {
        public int Id;
        public string Title;
        public bool IsActive;
    }

    public class UserPermissionComponants
    {
        public int? Id;
        public string Title;
        public List<UserAction> Components;
    }

}
