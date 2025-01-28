using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreMdCoreTops
    {
        public ConvCoreMdCoreTops()
        {
            ConvCores = new HashSet<ConvCores>();
        }

        [Key]
        public int MdCoreTopId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdCoreTop")]
        public virtual ICollection<ConvCores> ConvCores { get; set; }
    }
}
