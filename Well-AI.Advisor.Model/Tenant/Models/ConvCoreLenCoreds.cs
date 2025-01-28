using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreLenCoreds
    {
        public ConvCoreLenCoreds()
        {
            ConvCores = new HashSet<ConvCores>();
        }

        [Key]
        public int LenCoredId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("LenCored")]
        public virtual ICollection<ConvCores> ConvCores { get; set; }
    }
}
