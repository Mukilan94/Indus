using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("USAStates")]
    public class USAState
    {
        [Key]
        [Required]
        public int StateId { get; set; }
        public string Name { get; set; }
    }
}
