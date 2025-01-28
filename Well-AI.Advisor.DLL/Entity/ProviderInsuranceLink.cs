using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("ProviderInsuranceLink")]
    public class ProviderInsuranceLink
    {
        [Key]
        [StringLength(40)]
        public string Id {get;set;}

        [StringLength(40)]
        public string ServiceTenantId { get; set; }

        [StringLength(40)]
        public string OperationTenantId { get; set; }

        [StringLength(40)]
        public string FileId { get; set; }// link to column FileId in table WellFiles of master db
        public DateTime? Expire { get; set; }

        [StringLength(20)]
        public string Status { get; set; }
    }
}
