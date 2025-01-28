using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreLithPcs
    {
        public ConvCoreLithPcs()
        {
            ConvCoreLithologys = new HashSet<ConvCoreLithologys>();
        }

        [Key]
        public int LithPcId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("LithPc")]
        public virtual ICollection<ConvCoreLithologys> ConvCoreLithologys { get; set; }
    }
}
