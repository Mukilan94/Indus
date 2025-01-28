using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("WellConfig")]
    public class WellConfig
    {
        [Key]
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VariableName { get; set; }
        public string Value { get; set; }
    }
}
