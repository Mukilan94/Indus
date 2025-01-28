using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("Supplies")]
    public class Supplies
    {
        [Key]
        public string SupplyId { get; set; }
        public string SupplyName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
