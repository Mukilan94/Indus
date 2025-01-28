using System;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class UserUpdateInputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Name { get; set; }
        public string UserId { get; set; }
        [StringLength(12, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string PhoneNumber { get; set; }
    }
}
