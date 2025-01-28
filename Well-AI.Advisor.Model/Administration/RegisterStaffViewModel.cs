using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.Administration
{
    public class RegisterStaffViewModel
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress(ErrorMessage="Please Enter Valid Email Address")]
        public string Email { get; set; }
        [Display(Name ="Full Name")]
        [Required]
        public string FullName { get; set; }
        [Display(Name ="Phone")]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public bool IsActive { get; set; }

        public string ErrorMessage { get; set; }
    }
}
