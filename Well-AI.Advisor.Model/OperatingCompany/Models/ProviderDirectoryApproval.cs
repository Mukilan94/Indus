using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    [Table("ProviderDirectoryApproval")]
    public class ProviderDirectoryApproval
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(254)]
        public string Name { get; set; }
    }
}
