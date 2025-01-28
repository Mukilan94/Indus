using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class UserViewSRVModel
    {
        public string UserID
        {
            get;
            set;
        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string JobTitle { get; set; }
        public bool? IsMaster { get; set; }
        public bool? IsActive { get; set; }
        public bool IsPrimary { get; set; }
        public bool Field { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        public string AdditionalNotes { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        [Required]
        public string Mobile { get; set; }

        public bool WellOfficeUser { get; set; }
        [Required(ErrorMessage ="The Selected Roles field is required")]
        public string SelectedRoles { get; set; }
        public string SelectedWells { get; set; }

        public string ProfileImageName { get; set; }
        public string UserTenantId { get; set; }
        public DateTime? BirthDate { get; set; }

        public string UserName { get; set; }

        public List<UserRole> Permissions;
        public List<IdentityRole> roles;
    }
    public class UserCountModel {
    public int Usercount { get; set; }
    public int UserTotalCount { get; set; }
    }
}