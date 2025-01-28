using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class SideWallCoreLithology
    {
        public SideWallCoreLithology()
        {
            SideWallCoreSwcSample = new HashSet<SideWallCoreSwcSample>();
        }

        [Key]
        public string Uid { get; set; }
        public string Type { get; set; }
        public string CodeLith { get; set; }
        public string LithPcUom { get; set; }
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
        public string DensShaleUom { get; set; }
        public string QualifierUid { get; set; }

        [ForeignKey(nameof(DensShaleUom))]
        [InverseProperty(nameof(SideWallCoreDensShale.SideWallCoreLithology))]
        public virtual SideWallCoreDensShale DensShaleUomNavigation { get; set; }
        [ForeignKey(nameof(LithPcUom))]
        [InverseProperty(nameof(SideWallLithPcs.SideWallCoreLithology))]
        public virtual SideWallLithPcs LithPcUomNavigation { get; set; }
        [ForeignKey(nameof(QualifierUid))]
        [InverseProperty(nameof(SideWallCoreQualifier.SideWallCoreLithology))]
        public virtual SideWallCoreQualifier QualifierU { get; set; }
        [InverseProperty("LithologyU")]
        public virtual ICollection<SideWallCoreSwcSample> SideWallCoreSwcSample { get; set; }
    }
}
