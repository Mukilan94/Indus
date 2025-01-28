using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobMdBottoms
    {
        public StimJobMdBottoms()
        {
            StimJobTubulars = new HashSet<StimJobTubulars>();
        }

        [Key]
        public int MdBottomId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdBottom")]
        public virtual ICollection<StimJobTubulars> StimJobTubulars { get; set; }
    }
}
