using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogQualifier
    {
        public MudLogQualifier()
        {
            MudLogLithology = new HashSet<MudLogLithology>();
        }

        [Key]
        public int QualifierId { get; set; }
        public string Type { get; set; }
        public int? AbundanceId { get; set; }
        public string Description { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(AbundanceId))]
        [InverseProperty(nameof(MudLogAbundance.MudLogQualifier))]
        public virtual MudLogAbundance Abundance { get; set; }
        [InverseProperty("Qualifier")]
        public virtual ICollection<MudLogLithology> MudLogLithology { get; set; }
    }
}
