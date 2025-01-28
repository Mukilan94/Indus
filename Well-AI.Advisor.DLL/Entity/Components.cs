using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("components")]
    public class Components
    {
        [Key]
        [Required]
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public string Label { get; set; }
        public int? AccountType { get; set; }
        public int? SrvAccountType { get; set; }

    }
}
