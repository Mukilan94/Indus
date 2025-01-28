using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("subscriptionpackage")]
    public class SubscriptionPackage
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
}
