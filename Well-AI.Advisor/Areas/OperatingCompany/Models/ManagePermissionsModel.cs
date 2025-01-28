using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebAI.Advisor.Areas.OperatingCompany.Models
{
    public class ManagePermissionsModel
    {
        public List<UserViewModel> users;
        public List<UserRole> roles;
        public SelectListItem selectedUser;
        public List<SelectListItem> selectedRoles;
    }
}
