using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class BharunRopMxs
    {
        public BharunRopMxs()
        {
            BharunDrillingParamss = new HashSet<BharunDrillingParamss>();
        }

        [Key]
        public int BharunRopMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("RopMxBharunRopMx")]
        public virtual ICollection<BharunDrillingParamss> BharunDrillingParamss { get; set; }
    }
}
