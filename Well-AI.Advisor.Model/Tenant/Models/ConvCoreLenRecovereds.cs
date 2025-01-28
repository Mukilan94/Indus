using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreLenRecovereds
    {
        public ConvCoreLenRecovereds()
        {
            ConvCores = new HashSet<ConvCores>();
        }

        [Key]
        public int LenRecoveredId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("LenRecovered")]
        public virtual ICollection<ConvCores> ConvCores { get; set; }
    }
}
