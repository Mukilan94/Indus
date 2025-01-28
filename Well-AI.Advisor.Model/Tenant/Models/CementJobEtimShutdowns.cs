using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CementJobETimShutdowns")]
    public partial class CementJobEtimShutdowns
    {
        public CementJobEtimShutdowns()
        {
            CementJobCementPumpSchedules = new HashSet<CementJobCementPumpSchedules>();
        }

        [Key]
        [Column("ETimShutdownId")]
        public int EtimShutdownId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimShutdown")]
        public virtual ICollection<CementJobCementPumpSchedules> CementJobCementPumpSchedules { get; set; }
    }
}
