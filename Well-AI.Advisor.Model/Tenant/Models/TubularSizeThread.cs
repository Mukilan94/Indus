using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularSizeThread
    {
        public TubularSizeThread()
        {
            TubularConnection = new HashSet<TubularConnection>();
        }

        [Key]
        public int SizeThreadId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("SizeThread")]
        public virtual ICollection<TubularConnection> TubularConnection { get; set; }
    }
}
