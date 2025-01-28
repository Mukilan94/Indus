using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogLithology
    {
        public MudLogLithology()
        {
            MudLogGeologyInterval = new HashSet<MudLogGeologyInterval>();
        }

        [Key]
        public int LithologyId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Texture { get; set; }
        public string Hardness { get; set; }
        public string SizeGrain { get; set; }
        public string Roundness { get; set; }
        public string Sorting { get; set; }
        public string MatrixCement { get; set; }
        public string PorosityVisible { get; set; }
        public string Permeability { get; set; }
        public int? DensShaleId { get; set; }
        public int? QualifierId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(DensShaleId))]
        [InverseProperty(nameof(MudLogDensShale.MudLogLithology))]
        public virtual MudLogDensShale DensShale { get; set; }
        [ForeignKey(nameof(QualifierId))]
        [InverseProperty(nameof(MudLogQualifier.MudLogLithology))]
        public virtual MudLogQualifier Qualifier { get; set; }
        [InverseProperty("Lithology")]
        public virtual ICollection<MudLogGeologyInterval> MudLogGeologyInterval { get; set; }
    }
}
