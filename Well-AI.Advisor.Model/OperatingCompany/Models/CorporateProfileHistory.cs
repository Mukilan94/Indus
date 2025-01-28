using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    [Table("CorporateProfileHistory")]
    public class CorporateProfileHistory
    {
        [Key]
        [Required]
        [StringLength(40)]
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
        [StringLength(40)]
        public string UserId { get; set; }
        [StringLength(40)]
        public string TenantId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
