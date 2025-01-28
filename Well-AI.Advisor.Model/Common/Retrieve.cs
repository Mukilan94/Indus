using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WellAI.Advisor.Model.OperatingCompany.Models;


namespace WellAI.Advisor.Model.Common
{
    public class Retrieve
    {
        
        public RetriveSubscriptionDetails productSubscription { get; set; }
        public RetriveSubscriptionPackage subscriptionPackage { get; set; }
        public RetriveCorporateProfile corporateProfile { get; set; }
        public RetrivePaymentMethod paymentDetails { get; set; }
         
    }

    public class RetriveSubscriptionDetails
    {
        public System.Guid ID { get; set; }
        [StringLength(450)]
        public string SubscriptionId { get; set; }
        public int SubscriptionCount { get; set; }
        public int? SubscriptionDispatchCount { get; set; }
        public int? CurrentCount { get; set; }
        [StringLength(50)]
        public string PackageId { get; set; }
        public decimal PackageAmount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsEnable { get; set; }
        public DateTime? SubStartdate { get; set; }
        public DateTime? SubEndDate { get; set; }

        [StringLength(450)]
        public string TenantId { get; set; }
        [StringLength(50)]
        public string CorporateProfileId { get; set; }
        [StringLength(50)]
        public DateTime? NextRenewalDate { get; set; }
        public byte? SubscriptionType { get; set; }
        [StringLength(450)]
        public string? PaymentMethodId { get; set; }

        public static implicit operator RetriveSubscriptionDetails(CorporateProfile v)
        {
            throw new NotImplementedException();
        }
    }
    public class RetriveSubscriptionPackage
    {
        [Key]
        public System.Guid PackageId { get; set; }
        public string Name { get; set; }
        public string PackageName { get; set; }
        public string PackageAmount { get; set; }
        public bool IsActive { get; set; }
        public int PackageOrder { get; set; }
        public int AccountType { get; set; }
        public string AccountTypeDescription { get; set; }
        public string Description { get; set; }
        public Int16 Length { get; set; }
        public byte Unit { get; set; }
        public int TotalOccurrences { get; set; }
        public int TrialOccurrences { get; set; }
    }
    public class RetriveCorporateProfile
    {
        [Key]
        [Required]
        [StringLength(254)]
        public string ID { get; set; }
        [StringLength(500)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Website { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(500)]
        public string Address1 { get; set; }
        [StringLength(500)]
        public string Address2 { get; set; }
        [StringLength(254)]
        public string City { get; set; }
        [StringLength(50)]
        public string Country { get; set; }
        [StringLength(50)]
        public string Zip { get; set; }
        [StringLength(50)]
        public string State { get; set; }
        public string LogoPath { get; set; }
        [StringLength(254)]
        public string UserId { get; set; }
        [StringLength(40)]
        public string TenantId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CServices { get; set; }

        [StringLength(500)]
        public string BillingAddress1 { get; set; }
        [StringLength(500)]
        public string BillingAddress2 { get; set; }
        [StringLength(500)]
        public string BillingCity { get; set; }
        [StringLength(50)]
        public string BillingZip { get; set; }
        [StringLength(500)]
        public string BillingState { get; set; }
        [StringLength(50)]
        public string BillingPhone { get; set; }
        [StringLength(500)]
        public string BillingEmail { get; set; }

        public static implicit operator RetriveCorporateProfile(CorporateProfile v)
        {
            throw new NotImplementedException();
        }
    }
    public class RetrivePaymentMethod
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(254)]
        //[Required(ErrorMessage = "Enter The Name")]
        public string Holder { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(256)]
        //[Required(ErrorMessage = "Enter The Nick name")]
        public string Nickname { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Card Number must be numeric")]
        [StringLength(16, ErrorMessage = "Card Number Must Be have 16 Digits", MinimumLength = 16), MaxLength(200)] public string Number { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        [StringLength(256)]
        [Display(Name = "Method")]
        public string PayType { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(256)]
        //[Required(ErrorMessage = "Enter The First Name")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(256)]
        //[Required(ErrorMessage = "Enter The Middle Initial")]
        public string MiddleInitial { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(256)]
        //[Required(ErrorMessage = "Enter The Last Name")]
        public string LastName { get; set; }
        [StringLength(500)]
        public string Address1 { get; set; }
        [StringLength(500)]
        public string Address2 { get; set; }
        [StringLength(256)]
        public string City { get; set; }
        [StringLength(256)]
        public string State { get; set; }
        [StringLength(256)]
        public string Country { get; set; }
        [StringLength(256)]
        public string Zip { get; set; }

        public bool Default { get; set; }
        public bool Agreement { get; set; }

        [StringLength(20)]
        public int CardType { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [StringLength(50)]
        //[Required(ErrorMessage = "Enter The Account Name")]
        public string AccountName { get; set; }

        [StringLength(50)]
        public string RoutingNumber { get; set; }

        [StringLength(50)]
        public string CheckNumber { get; set; }

        [StringLength(40)]
        public string TenantId { get; set; }

        //ach payment
        [StringLength(50)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string TypeofAccount { get; set; }

        [StringLength(50)]
        public string CustomerName { get; set; }

    }
}


