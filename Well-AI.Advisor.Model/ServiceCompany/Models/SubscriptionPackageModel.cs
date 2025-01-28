using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class SubscriptionPackageModel
    {
        public System.Guid PackageId { get; set; }
        public string PackageName { get; set; }
        public int Unit { get; set; }
        public string PackageAmount { get; set; }
        public bool IsActive { get; set; }
        public int PackageOrder { get; set; }
        public int AccountType { get; set; }
        public string AccountTypeDescription { get; set; }
        public string NewPackageAmount { get; set; }
    }
    public class SubscriptionPackageViewModel
    {
        public List<PaymentMethodViewModel> PaymentDetail { get; set; }
        public SubscriptionPackageModel SubscriptionPackageModel { get; set; }
    }
    public class PaymentMethodViewModel
    {
        public string ID { get; set; }


        public string Holder { get; set; }

        public string Nickname { get; set; }

        public string Number { get; set; }

        public string ValidTill { get; set; }

        [Required]
        public string CVV { get; set; }

        public string PayType { get; set; }

        public string FullName { get; set; }

        public string FullAddress { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Zip { get; set; }

        public bool? Default { get; set; }
        public bool? Agreement { get; set; }


        public string? CardType { get; set; }


        public string AccountNumber { get; set; }



        public string AccountName { get; set; }



        public string RoutingNumber { get; set; }


        public string CheckNumber { get; set; }

        public string CountryName { get; set; }
        public string StateName { get; set; }
    }
    public class SubscriptionViewModel
    {
        public string AddressId { get; set; }
        public string TotalAmount { get; set; }
        public string RigCount { get; set; }
        public string PackageType { get; set; }
        public string PackageId { get; set; }
    }
    public class BillingInvoiceHistorySRVNewModel
    {
        [ScaffoldColumn(false)]
        public string InvoiceId { get; set; }
        [Required]
        public string InvoiceNo { get; set; }
        [Required]
        public string Product { get; set; }
        [Required]
        public DateTime BillDate { get; set; }
        [Required]
        public string Amount { get; set; }
        [Required]
        public string Paymentmethod { get; set; }



      

    }
    public class ARBSubscriptionModel
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; }
        [Required]
        public string Paymentmethod { get; set; }
        [Required]
        [Display(Name = "10005000400")]
        public string Cardnumber { get; set; }
        [Required]
        public DateTime Expirationdate { get; set; }
        [Required]
        public string Amount { get; set; }
        [Required]
        public string Subscriptionname { get; set; }
        [Required]
        public string Invoiceno { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Subscriptioninterval { get; set; }
        [Required]
        public string month { get; set; }
        [Required]
        public string day { get; set; }
        [Required]
        public DateTime Startdate { get; set; }
        [Required] 
        public DateTime Enddate { get; set; }
        [Required]
        public string Nodate { get; set; }
        [Required]
        public string Endsafter { get; set; }
        [Required]
        public string customerid { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Fax { get; set; }
        [Required]
        public string Email { get; set; }
    }

}



