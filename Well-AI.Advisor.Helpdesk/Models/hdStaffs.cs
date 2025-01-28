using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Well_AI.Advisor.Helpdesk.Models
{
    public class hdStaffs : IdentityUser
{
        public string TenantId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string Mobile { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string AdditionalNotes { get; set; }

        public string Address { get; set; }

        public string City { get; set; }
        public bool? Primary { get; set; }
    }
}
