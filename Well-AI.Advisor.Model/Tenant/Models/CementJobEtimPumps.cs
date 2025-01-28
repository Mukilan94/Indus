using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CementJobETimPumps")]
    public partial class CementJobEtimPumps
    {
        public CementJobEtimPumps()
        {
            CementJobCementPumpSchedules = new HashSet<CementJobCementPumpSchedules>();
        }

        [Key]
        [Column("ETimPumpId")]
        public int EtimPumpId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimPump")]
        public virtual ICollection<CementJobCementPumpSchedules> CementJobCementPumpSchedules { get; set; }
    }
}
