using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("crmCompanies")]
    public class CrmCompanies
    {
        [Key]
        [Required]
        public int CompanyId { get; set; }
        public int InstanceID { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateRegion { get; set; }
        [NotMapped]
        public string StateRegionName { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Comments { get; set; }
        public int? LatestCommentID { get; set; }
        public int? AssignedToUserID { get; set; }
        public string Fax { get; set; }
        public string Category { get; set; }
        [NotMapped]
        public string CategoryName { get; set; }
        public string EIN { get; set; }
        public string UserId { get; set; }
        public string TenantId { get; set; }
    }
}
