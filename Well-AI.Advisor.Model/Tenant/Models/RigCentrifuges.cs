using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigCentrifuges
    {
        public RigCentrifuges()
        {
            Rigs = new HashSet<Rigs>();
        }

        [Key]
        public int CentrifugeId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        [Column("DTimInstall")]
        public string DtimInstall { get; set; }
        [Column("DTimRemove")]
        public string DtimRemove { get; set; }
        public string Type { get; set; }
        public int? CapFlowId { get; set; }
        public string Owner { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(CapFlowId))]
        [InverseProperty(nameof(RigCapFlows.RigCentrifuges))]
        public virtual RigCapFlows CapFlow { get; set; }
        [InverseProperty("Centrifuge")]
        public virtual ICollection<Rigs> Rigs { get; set; }
    }
}
