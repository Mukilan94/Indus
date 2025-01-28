using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigShakers
    {
        public RigShakers()
        {
            Rigs = new HashSet<Rigs>();
        }

        [Key]
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        [Column("DTimInstall")]
        public string DtimInstall { get; set; }
        [Column("DTimRemove")]
        public string DtimRemove { get; set; }
        public string Type { get; set; }
        public string LocationShaker { get; set; }
        public string NumDecks { get; set; }
        public string NumCascLevel { get; set; }
        public string MudCleaner { get; set; }
        public int? CapFlowId { get; set; }
        public string Owner { get; set; }
        public int? SizeMeshMnId { get; set; }

        [ForeignKey(nameof(CapFlowId))]
        [InverseProperty(nameof(RigCapFlows.RigShakers))]
        public virtual RigCapFlows CapFlow { get; set; }
        [ForeignKey(nameof(SizeMeshMnId))]
        [InverseProperty(nameof(RigSizeMeshMns.RigShakers))]
        public virtual RigSizeMeshMns SizeMeshMn { get; set; }
        [InverseProperty("ShakerU")]
        public virtual ICollection<Rigs> Rigs { get; set; }
    }
}
