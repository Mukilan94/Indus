using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularDiaRotorNozzle
    {
        public TubularDiaRotorNozzle()
        {
            TubularMotor = new HashSet<TubularMotor>();
        }

        [Key]
        public int DiaRotorNozzleId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiaRotorNozzle")]
        public virtual ICollection<TubularMotor> TubularMotor { get; set; }
    }
}
