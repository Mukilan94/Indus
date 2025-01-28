using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobRatePumps
    {
        public CementJobRatePumps()
        {
            CementJobCementPumpSchedules = new HashSet<CementJobCementPumpSchedules>();
        }

        [Key]
        public int RatePumpId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("RatePump")]
        public virtual ICollection<CementJobCementPumpSchedules> CementJobCementPumpSchedules { get; set; }
    }
}
