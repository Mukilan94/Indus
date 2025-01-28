using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularClearanceBearBox
    {
        public TubularClearanceBearBox()
        {
            TubularMotor = new HashSet<TubularMotor>();
        }

        [Key]
        public int ClearanceBearBoxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ClearanceBearBox")]
        public virtual ICollection<TubularMotor> TubularMotor { get; set; }
    }
}
