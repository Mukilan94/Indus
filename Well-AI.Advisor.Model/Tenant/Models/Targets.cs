using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class Targets
    {
        public Targets()
        {
            TargetLocations = new HashSet<TargetLocations>();
            TargetSections = new HashSet<TargetSections>();
        }

        [Key]
        public int TargetId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public int? DispNsCenterId { get; set; }
        public int? DispEwCenterId { get; set; }
        public int? TvdId { get; set; }
        public int? DispNsOffsetId { get; set; }
        public int? DispEwOffsetId { get; set; }
        public int? ThickAboveId { get; set; }
        public int? ThickBelowId { get; set; }
        public int? DipId { get; set; }
        public int? StrikeId { get; set; }
        public int? RotationId { get; set; }
        public int? LenMajorAxisId { get; set; }
        public int? WidMinorAxisId { get; set; }
        public string TypeTargetScope { get; set; }
        public int? DispNsSectOrigId { get; set; }
        public int? DispEwSectOrigId { get; set; }
        public string AziRef { get; set; }
        public string CatTarg { get; set; }
        public int? CommonDataTargetCommonDataId { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(CommonDataTargetCommonDataId))]
        [InverseProperty(nameof(TargetCommonDatas.Targets))]
        public virtual TargetCommonDatas CommonDataTargetCommonData { get; set; }
        [ForeignKey(nameof(DipId))]
        [InverseProperty(nameof(TargetDips.Targets))]
        public virtual TargetDips Dip { get; set; }
        [ForeignKey(nameof(DispEwCenterId))]
        [InverseProperty(nameof(TargetDispEwCenters.Targets))]
        public virtual TargetDispEwCenters DispEwCenter { get; set; }
        [ForeignKey(nameof(DispEwOffsetId))]
        [InverseProperty(nameof(TargetDispEwOffsets.Targets))]
        public virtual TargetDispEwOffsets DispEwOffset { get; set; }
        [ForeignKey(nameof(DispEwSectOrigId))]
        [InverseProperty(nameof(TargetDispEwSectOrigs.Targets))]
        public virtual TargetDispEwSectOrigs DispEwSectOrig { get; set; }
        [ForeignKey(nameof(DispNsCenterId))]
        [InverseProperty(nameof(TargetDispNsCenters.Targets))]
        public virtual TargetDispNsCenters DispNsCenter { get; set; }
        [ForeignKey(nameof(DispNsOffsetId))]
        [InverseProperty(nameof(TargetDispNsOffsets.Targets))]
        public virtual TargetDispNsOffsets DispNsOffset { get; set; }
        [ForeignKey(nameof(DispNsSectOrigId))]
        [InverseProperty(nameof(TargetDispNsSectOrigs.Targets))]
        public virtual TargetDispNsSectOrigs DispNsSectOrig { get; set; }
        [ForeignKey(nameof(LenMajorAxisId))]
        [InverseProperty(nameof(TargetLenMajorAxiss.Targets))]
        public virtual TargetLenMajorAxiss LenMajorAxis { get; set; }
        [ForeignKey(nameof(RotationId))]
        [InverseProperty(nameof(TargetRotations.Targets))]
        public virtual TargetRotations Rotation { get; set; }
        [ForeignKey(nameof(StrikeId))]
        [InverseProperty(nameof(TargetStrikes.Targets))]
        public virtual TargetStrikes Strike { get; set; }
        [ForeignKey(nameof(ThickAboveId))]
        [InverseProperty(nameof(TargetThickAboves.Targets))]
        public virtual TargetThickAboves ThickAbove { get; set; }
        [ForeignKey(nameof(ThickBelowId))]
        [InverseProperty(nameof(TargetThickBelows.Targets))]
        public virtual TargetThickBelows ThickBelow { get; set; }
        [ForeignKey(nameof(TvdId))]
        [InverseProperty(nameof(TargetTvds.Targets))]
        public virtual TargetTvds Tvd { get; set; }
        [ForeignKey(nameof(WidMinorAxisId))]
        [InverseProperty(nameof(TargetWidMinorAxiss.Targets))]
        public virtual TargetWidMinorAxiss WidMinorAxis { get; set; }
        [InverseProperty("Target")]
        public virtual ICollection<TargetLocations> TargetLocations { get; set; }
        [InverseProperty("Target")]
        public virtual ICollection<TargetSections> TargetSections { get; set; }
    }
}
