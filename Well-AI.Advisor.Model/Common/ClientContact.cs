using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Common
{
    [Table("ClientContact")]
    public class ClientContact
    {
        [Key]
        public int ClientContactId { get; set; }
        [StringLength(254)]
        public string UserId { get; set; }
        [StringLength(254)]
        public string TenantId { get; set; }
        [StringLength(254)]
        public string ContactId { get; set; }
        public bool IsActive { get; set; }
    }
}
