using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class BharunInclMxs
    {
        public BharunInclMxs()
        {
            BharunDrillingParamss = new HashSet<BharunDrillingParamss>();
        }

        [Key]
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("InclMxUomNavigation")]
        public virtual ICollection<BharunDrillingParamss> BharunDrillingParamss { get; set; }
    }
}
