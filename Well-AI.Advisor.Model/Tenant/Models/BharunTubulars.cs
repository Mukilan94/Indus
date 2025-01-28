using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class BharunTubulars
    {
        public BharunTubulars()
        {
            BharunDrillingParamss = new HashSet<BharunDrillingParamss>();
            Bharuns = new HashSet<Bharuns>();
        }

        [Key]
        public string UidRef { get; set; }
        public string Text { get; set; }

        [InverseProperty("TubularUidRefNavigation")]
        public virtual ICollection<BharunDrillingParamss> BharunDrillingParamss { get; set; }
        [InverseProperty("TubularUidRefNavigation")]
        public virtual ICollection<Bharuns> Bharuns { get; set; }
    }
}
