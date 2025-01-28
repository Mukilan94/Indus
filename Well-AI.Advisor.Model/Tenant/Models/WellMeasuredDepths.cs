using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellMeasuredDepths
    {
        public WellMeasuredDepths()
        {
            WellReferencePoints = new HashSet<WellReferencePoints>();
        }

        [Key]
        public int MeasuredDepthId { get; set; }
        public string Uom { get; set; }
        public string Datum { get; set; }
        public string Text { get; set; }

        [InverseProperty("MeasuredDepth")]
        public virtual ICollection<WellReferencePoints> WellReferencePoints { get; set; }
    }
}
