using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogAbundance
    {
        public MudLogAbundance()
        {
            MudLogQualifier = new HashSet<MudLogQualifier>();
        }

        [Key]
        public int AbundanceId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Abundance")]
        public virtual ICollection<MudLogQualifier> MudLogQualifier { get; set; }
    }
}
