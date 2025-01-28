using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularDiaNozzle
    {
        public TubularDiaNozzle()
        {
            TubularMotor = new HashSet<TubularMotor>();
            TubularNozzle = new HashSet<TubularNozzle>();
        }

        [Key]
        public int DiaNozzleId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiaNozzle")]
        public virtual ICollection<TubularMotor> TubularMotor { get; set; }
        [InverseProperty("DiaNozzle")]
        public virtual ICollection<TubularNozzle> TubularNozzle { get; set; }
    }
}
