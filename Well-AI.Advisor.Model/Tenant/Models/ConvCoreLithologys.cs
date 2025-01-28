using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreLithologys
    {
        public ConvCoreLithologys()
        {
            ConvCoreGeologyIntervals = new HashSet<ConvCoreGeologyIntervals>();
            ConvCoreQualifiers = new HashSet<ConvCoreQualifiers>();
        }

        [Key]
        public int LithologyId { get; set; }
        public string Type { get; set; }
        public string CodeLith { get; set; }
        public int? LithPcId { get; set; }
        public string Description { get; set; }
        public string LithClass { get; set; }
        public string GrainType { get; set; }
        public string DunhamClass { get; set; }
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
        public string Uid { get; set; }

        [ForeignKey(nameof(DensShaleId))]
        [InverseProperty(nameof(ConvCoreDensShales.ConvCoreLithologys))]
        public virtual ConvCoreDensShales DensShale { get; set; }
        [ForeignKey(nameof(LithPcId))]
        [InverseProperty(nameof(ConvCoreLithPcs.ConvCoreLithologys))]
        public virtual ConvCoreLithPcs LithPc { get; set; }
        [InverseProperty("Lithology")]
        public virtual ICollection<ConvCoreGeologyIntervals> ConvCoreGeologyIntervals { get; set; }
        [InverseProperty("ConvCoreLithologyLithology")]
        public virtual ICollection<ConvCoreQualifiers> ConvCoreQualifiers { get; set; }
    }
}
