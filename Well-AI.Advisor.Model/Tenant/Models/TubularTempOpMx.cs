using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularTempOpMx
    {
        public TubularTempOpMx()
        {
            TubularMotor = new HashSet<TubularMotor>();
        }

        [Key]
        public int TempOpMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TempOpMx")]
        public virtual ICollection<TubularMotor> TubularMotor { get; set; }
    }
}
