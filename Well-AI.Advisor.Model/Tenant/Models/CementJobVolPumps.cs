using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobVolPumps
    {
        public CementJobVolPumps()
        {
            CementJobCementPumpSchedules = new HashSet<CementJobCementPumpSchedules>();
        }

        [Key]
        public int VolPumpId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VolPump")]
        public virtual ICollection<CementJobCementPumpSchedules> CementJobCementPumpSchedules { get; set; }
    }
}
