using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CementJobTempBHSTs")]
    public partial class CementJobTempBhsts
    {
        public CementJobTempBhsts()
        {
            CementJobCementStages = new HashSet<CementJobCementStages>();
        }

        [Key]
        [Column("TempBHSTId")]
        public int TempBhstid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TempBhst")]
        public virtual ICollection<CementJobCementStages> CementJobCementStages { get; set; }
    }
}
