using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class SideWallCoreQualifier
    {
        public SideWallCoreQualifier()
        {
            SideWallCoreLithology = new HashSet<SideWallCoreLithology>();
        }

        [Key]
        public string Uid { get; set; }
        public string Type { get; set; }
        public string AbundanceUom { get; set; }
        public string AbundanceCode { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(AbundanceUom))]
        [InverseProperty(nameof(SideWallCoreAbundance.SideWallCoreQualifier))]
        public virtual SideWallCoreAbundance AbundanceUomNavigation { get; set; }
        [InverseProperty("QualifierU")]
        public virtual ICollection<SideWallCoreLithology> SideWallCoreLithology { get; set; }
    }
}
