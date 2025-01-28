using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.Entity
{
    /// <summary>
    /// Folders in azure blob storage, created automatically for each tenant.
    /// </summary>
    [Table("WellFileFolder")]
    public class WellFileFolder
    {
        [Key]
        [Required]
        public string Id { get; set; }
        public string FolderName { get; set; }
        public int AccountType { get; set; }
        public string Parent { get; set; }
        public bool Enable { get; set; }
    }
}
