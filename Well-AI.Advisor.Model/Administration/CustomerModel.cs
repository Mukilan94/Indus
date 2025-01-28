using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace WellAI.Advisor.Model.Administration
{
    public class CustomerProfileModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public string LogoPath { get; set; }
        public string TenantId { get; set; }
        [Display(Name ="Name")]
        public string SubscriptionName { get; set; }
        [Display(Name = "Description")]
        public string SubscriptionDescription { get; set; }
        [Display(Name = "Per Price")]
        public string SubscriptionPrice { get; set; }
        [Display(Name ="Total Rigs")]
        public int SubscriptionRigsCount { get; set; }
        [Display(Name = "Total Amount")]
        public double SubscriptionTotalAmount { get; set; }
        [Display(Name ="Start")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? SubscriptionStart { get; set; }
        [Display(Name ="End")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? SubscriptionEnd { get; set; }
        public string IsEnableSubscription { get; set; }
        public string SubscriptionId { get; set; }
        public int AccountType { get; set; }
        public DataTable Columns { get; set; }

        public string ContactName{ get; set; }
        public int PackageOrder { get; set; }

        public List<SubscriptionPackage> SubscriptionPackages { get; set; }
    }


    public class SubscriptionPackage
    {
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
}
