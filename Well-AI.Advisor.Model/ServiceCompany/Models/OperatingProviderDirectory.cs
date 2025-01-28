using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    [Table("OperatingDirectory")]
    public class OperatingDirectory
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        [Required]
        public string CompanyId { get; set; }

        [StringLength(50)]
        public string Approval { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(50)]
        public string PEC { get; set; }

        [StringLength(50)]
        public string MSA { get; set; }

        public int? Rating { get; set; }
        //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
        public bool Preferred { get; set; }
        public bool Secondary { get; set; }

        public DateTime? InsuranceStart { get; set; }

        public DateTime? InsuranceExpire { get; set; }

        [StringLength(254)]
        public string TenantId { get; set; }

        //Phase II - 05/19/2021 
        [StringLength(40)]
        public string Insurance { get; set; }
    }
}
