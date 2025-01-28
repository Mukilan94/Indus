using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularConnection
    {
        public TubularConnection()
        {
            TubularComponent = new HashSet<TubularComponent>();
        }

        [Key]
        public int ConnectionId { get; set; }
        public int? IdTubularIdId { get; set; }
        public int? OdId { get; set; }
        public int? LenId { get; set; }
        public string TypeThread { get; set; }
        public int? SizeThreadId { get; set; }
        public int? TensYieldId { get; set; }
        public int? TqYieldId { get; set; }
        public string Position { get; set; }
        public int? CriticalCrossSectionId { get; set; }
        public int? PresLeakId { get; set; }
        public int? TqMakeupId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(CriticalCrossSectionId))]
        [InverseProperty(nameof(TubularCriticalCrossSection.TubularConnection))]
        public virtual TubularCriticalCrossSection CriticalCrossSection { get; set; }
        [ForeignKey(nameof(IdTubularIdId))]
        [InverseProperty(nameof(TubularId.TubularConnection))]
        public virtual TubularId IdTubularId { get; set; }
        [ForeignKey(nameof(LenId))]
        [InverseProperty(nameof(TubularLen.TubularConnection))]
        public virtual TubularLen Len { get; set; }
        [ForeignKey(nameof(OdId))]
        [InverseProperty(nameof(TubularOd.TubularConnection))]
        public virtual TubularOd Od { get; set; }
        [ForeignKey(nameof(PresLeakId))]
        [InverseProperty(nameof(TubularPresLeak.TubularConnection))]
        public virtual TubularPresLeak PresLeak { get; set; }
        [ForeignKey(nameof(SizeThreadId))]
        [InverseProperty(nameof(TubularSizeThread.TubularConnection))]
        public virtual TubularSizeThread SizeThread { get; set; }
        [ForeignKey(nameof(TensYieldId))]
        [InverseProperty(nameof(TubularTensYield.TubularConnection))]
        public virtual TubularTensYield TensYield { get; set; }
        [ForeignKey(nameof(TqMakeupId))]
        [InverseProperty(nameof(TubularTqMakeup.TubularConnection))]
        public virtual TubularTqMakeup TqMakeup { get; set; }
        [ForeignKey(nameof(TqYieldId))]
        [InverseProperty(nameof(TubularTqYield.TubularConnection))]
        public virtual TubularTqYield TqYield { get; set; }
        [InverseProperty("Connection")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
    }
}
