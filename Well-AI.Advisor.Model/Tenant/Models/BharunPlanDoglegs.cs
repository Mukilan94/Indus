using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class BharunPlanDoglegs
    {
        public BharunPlanDoglegs()
        {
            Bharuns = new HashSet<Bharuns>();
        }

        [Key]
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PlanDoglegUomNavigation")]
        public virtual ICollection<Bharuns> Bharuns { get; set; }
    }
}
