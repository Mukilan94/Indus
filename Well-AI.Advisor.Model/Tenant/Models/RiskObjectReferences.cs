using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RiskObjectReferences
    {
        public RiskObjectReferences()
        {
            Risks = new HashSet<Risks>();
        }

        [Key]
        public int ObjectReferenceId { get; set; }
        public string UidRef { get; set; }
        public string Object { get; set; }
        public string Text { get; set; }

        [InverseProperty("ObjectReference")]
        public virtual ICollection<Risks> Risks { get; set; }
    }
}
