using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreDiaCores
    {
        public ConvCoreDiaCores()
        {
            ConvCores = new HashSet<ConvCores>();
        }

        [Key]
        public int DiaCoreId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiaCore")]
        public virtual ICollection<ConvCores> ConvCores { get; set; }
    }
}
