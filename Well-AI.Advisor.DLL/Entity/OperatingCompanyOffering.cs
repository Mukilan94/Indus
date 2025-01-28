using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WellAI.Advisor.DLL.Entity
{
    [Table("OperatingCompanyOffering")]
    public class OperatingCompanyOffering
    {
        [Key]
        [StringLength(40)]
        public string Id { get; set; }

        [StringLength(40)]
        public string OperatingTenantId { get; set; }

        public string Offerings { get; set; }
    }
}
