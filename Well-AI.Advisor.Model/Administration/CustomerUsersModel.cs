using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Model.Administration
{
    public class CustomerUsersModel
    {
        public string UserID { get; set; }
        [Display(Name=("Name"))]
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public bool? IsMaster { get; set; }
        public bool? IsActive { get; set; }
        public bool IsPrimary { get; set; }
        public bool Field { get; set; }

        [Required(ErrorMessage = "Please Enter Valid Email Address")]
        [EmailAddress]
        public string Email { get; set; }
        public string AdditionalNotes { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Mobile { get; set; }
        public bool WellOfficeUser { get; set; }
        public string SelectedRoles { get; set; }
        public string SelectedWells { get; set; }
        public string ProfileImageName { get; set; }
        public string UserTenantId { get; set; }
        public string ErrorMessage { get; set; }
        public int AccountType { get; set; }
        public List<UserRole> Permissions;
        public List<IdentityRole> roles;
        public List<WellMasterDataViewModel> wells;
    }
}
