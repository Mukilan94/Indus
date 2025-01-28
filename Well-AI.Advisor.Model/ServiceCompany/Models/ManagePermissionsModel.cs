using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class ManagePermissionsModel
    {
        public List<UserViewSRVModel> users;
        public List<UserRole> roles;
        public SelectListItem selectedUser;
        public List<SelectListItem> selectedRoles;
    }
}
