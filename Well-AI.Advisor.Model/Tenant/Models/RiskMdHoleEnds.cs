using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RiskMdHoleEnds
    {
        public RiskMdHoleEnds()
        {
            Risks = new HashSet<Risks>();
        }

        [Key]
        public int MdHoleEndId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdHoleEnd")]
        public virtual ICollection<Risks> Risks { get; set; }
    }
}
