using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class SubscriptionPackageModel
    {

        public System.Guid PackageId { get; set; }
        public string TenantId { get; set; }
        public string Name { get; set; }
        public string PackageName { get; set; }
        public string PackageAmount { get; set; }
        public string NewPackageAmount { get; set; }
        public bool IsActive { get; set; }
        public int PackageOrder { get; set; }
        public int AccountType { get; set; }
        public string AccountTypeDescription { get; set; }
        public int Length { get; set; }
        public int Unit { get; set; }
        public int TotalOccurrences { get; set; }
        public int TrialOccurrences { get; set; }

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

        public string? CVV { get; set; }

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
        public int SubscriptionCount { get; set; }
        public int SubscriptionDispatchCount { get; set; }
        public string PackageType { get; set; }
        public string PackageId { get; set; }
        public string TenantId { get; set; }
        public int AccountType { get; set; }

        public string AuhorizeNetAccountType { get; set; }
    }
    public class SubscriptionViewModel_Admin
    {
        public string AddressId { get; set; }
        public string TotalAmount { get; set; }
        public string RigCount { get; set; }
        public int SubscriptionCount { get; set; }
        public int SubscriptionDispatchCount { get; set; }
        public string PackageType { get; set; }
        public string PackageId { get; set; }
        public string TenantId { get; set; }
        public int AccountType { get; set; }

        public string AuhorizeNetAccountType { get; set; }
    }
}
