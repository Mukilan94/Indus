using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TargetSections
    {
        public TargetSections()
        {
            TargetLocations = new HashSet<TargetLocations>();
        }

        [Key]
        public int TargetSectionId { get; set; }
        public string SectNumber { get; set; }
        public string TypeTargetSectionScope { get; set; }
        public int? LenRadiusId { get; set; }
        public int? AngleArcId { get; set; }
        public int? ThickAboveId { get; set; }
        public int? ThickBelowId { get; set; }
        public string Uid { get; set; }
        public int? TargetId { get; set; }

        [ForeignKey(nameof(AngleArcId))]
        [InverseProperty(nameof(TargetAngleArcs.TargetSections))]
        public virtual TargetAngleArcs AngleArc { get; set; }
        [ForeignKey(nameof(LenRadiusId))]
        [InverseProperty(nameof(TargetLenRadiuss.TargetSections))]
        public virtual TargetLenRadiuss LenRadius { get; set; }
        [ForeignKey(nameof(TargetId))]
        [InverseProperty(nameof(Targets.TargetSections))]
        public virtual Targets Target { get; set; }
        [ForeignKey(nameof(ThickAboveId))]
        [InverseProperty(nameof(TargetThickAboves.TargetSections))]
        public virtual TargetThickAboves ThickAbove { get; set; }
        [ForeignKey(nameof(ThickBelowId))]
        [InverseProperty(nameof(TargetThickBelows.TargetSections))]
        public virtual TargetThickBelows ThickBelow { get; set; }
        [InverseProperty("TargetSection")]
        public virtual ICollection<TargetLocations> TargetLocations { get; set; }
    }
}
