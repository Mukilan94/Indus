using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class SubscriptionOperatorModel
    {
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

        public string MSADocumentId { get; set; }

        public int? Rating { get; set; }
        //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
        public bool Preferred { get; set; }
        public bool Secondary { get; set; }

        public DateTime? InsuranceStart { get; set; }

        public DateTime? InsuranceExpire { get; set; }


        //Phase II - 05/21/2021
        public string InsuranceId { get; set; }
        public string InsuranceDocument { get; set; }

        [StringLength(254)]
        /// TenantId of Operation Company
        public string TenantId { get; set; }
    }
}
