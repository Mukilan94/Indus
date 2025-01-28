using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAI.Advisor.Areas.OperatingCompany.Models
{
    public class UserViewModel
    {
        [ScaffoldColumn(false)]
        public int UserID
        {
            get;
            set;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }

        public string JobTitle { get; set; }

        public string Email
        {
            get;
            set;
        }

        public DateTime? BirthDate
        {
            get;
            set;
        }
        public string AdditionalNotes
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public List<UserRole> Permissions;
    }
}