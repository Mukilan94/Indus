using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobMasss
    {
        public StimJobMasss()
        {
            StimJobAdditives = new HashSet<StimJobAdditives>();
            StimJobTotalProppantUsages = new HashSet<StimJobTotalProppantUsages>();
        }

        [Key]
        public int MassId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Mass")]
        public virtual ICollection<StimJobAdditives> StimJobAdditives { get; set; }
        [InverseProperty("Mass")]
        public virtual ICollection<StimJobTotalProppantUsages> StimJobTotalProppantUsages { get; set; }
    }
}
