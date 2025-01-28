using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobPresBacks
    {
        public CementJobPresBacks()
        {
            CementJobCementPumpSchedules = new HashSet<CementJobCementPumpSchedules>();
        }

        [Key]
        public int PresBackId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresBack")]
        public virtual ICollection<CementJobCementPumpSchedules> CementJobCementPumpSchedules { get; set; }
    }
}
