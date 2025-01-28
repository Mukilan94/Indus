using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CementJobETimMudCirculations")]
    public partial class CementJobEtimMudCirculations
    {
        public CementJobEtimMudCirculations()
        {
            CementJobCementStages = new HashSet<CementJobCementStages>();
        }

        [Key]
        [Column("ETimMudCirculationId")]
        public int EtimMudCirculationId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimMudCirculation")]
        public virtual ICollection<CementJobCementStages> CementJobCementStages { get; set; }
    }
}
