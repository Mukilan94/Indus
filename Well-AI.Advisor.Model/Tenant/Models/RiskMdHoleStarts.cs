using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RiskMdHoleStarts
    {
        public RiskMdHoleStarts()
        {
            Risks = new HashSet<Risks>();
        }

        [Key]
        public int MdHoleStartId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdHoleStart")]
        public virtual ICollection<Risks> Risks { get; set; }
    }
}
