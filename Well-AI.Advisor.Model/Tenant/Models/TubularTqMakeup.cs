using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularTqMakeup
    {
        public TubularTqMakeup()
        {
            TubularConnection = new HashSet<TubularConnection>();
        }

        [Key]
        public int TqMakeupId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TqMakeup")]
        public virtual ICollection<TubularConnection> TubularConnection { get; set; }
    }
}
