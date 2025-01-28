using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("Stage")]

    public class Stage
    {
        [Key]
        [StringLength(40)]
        public string Id { get; set; }

        [StringLength(254)]
        public string Name { get; set; }
    }
}

