using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.Identity
{
    public class WellIdentityUser : IdentityUser
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
        public bool? Field { get; set; }
        public bool? WellUser { get; set; }
        public string ProfileImageName { get; set; }
        public bool? IsUser { get; set; }
        public bool? IsDispatchUser { get; set; }
        public string UsersOptions { get; set; }
        public string? UserLocation { get; set; }
        public string? UserRouteStatus { get; set; }
        public DateTime? UserScheduledArrival { get; set; }
        public DateTime? UserETA { get; set; }
        //public string UserKey { get; set; }
    }

    public class AccountDetailsModel
    {
       [Required]
        public string Username { get; set; }

       [Required]
        public string Email { get; set; }

       [Required]
        public string Password { get; set; }
    }

    public class PersonalDetailsModel
    {
       [Required]
        public string FullName { get; set; }

       [Required]
        public string Country { get; set; }

       [Required]
        public string Gender { get; set; }

        public string About { get; set; }
    }

    public class PaymentDetailsModel
    {
       [Required]
        public string PaymentType { get; set; }

       [Required]
        public string CardNumber { get; set; }

       [Required]
        public string CSVNumber { get; set; }

       [Required]
        public string ExpirationDate { get; set; }

       [Required]
        public string CardHolderName { get; set; }
    }

    public class UserDetailsModel
    {
        public AccountDetailsModel AccountDetails { get; set; }

        public PersonalDetailsModel PersonalDetails { get; set; }

        public PaymentDetailsModel PaymentDetails { get; set; }
    }
}
