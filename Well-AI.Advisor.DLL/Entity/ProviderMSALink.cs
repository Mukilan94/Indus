using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("ProviderMSALink")]
    public class ProviderMSALink
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

        public bool? IsActive { get; set; }
        //Phase II Changes - 01/19/2021
        public bool? IsApproved { get; set; }
        public DateTime? FileUploadTime { get; set; }

        public byte? NotificationStatus { get; set; }
    }
}
