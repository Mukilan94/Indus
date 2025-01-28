using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CementJobTempBHCTs")]
    public partial class CementJobTempBhcts
    {
        public CementJobTempBhcts()
        {
            CementJobCementStages = new HashSet<CementJobCementStages>();
        }

        [Key]
        [Column("TempBHCTId")]
        public int TempBhctid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TempBhct")]
        public virtual ICollection<CementJobCementStages> CementJobCementStages { get; set; }
    }
}
