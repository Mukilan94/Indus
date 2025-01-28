using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularOffsetBot
    {
        public TubularOffsetBot()
        {
            TubularSensor = new HashSet<TubularSensor>();
        }

        [Key]
        public int OffsetBotId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("OffsetBot")]
        public virtual ICollection<TubularSensor> TubularSensor { get; set; }
    }
}
