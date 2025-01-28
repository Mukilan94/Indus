using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("WellYAxisAzimuths")]
    public partial class WellYaxisAzimuths
    {
        public WellYaxisAzimuths()
        {
            WellLocalCrss = new HashSet<WellLocalCrss>();
        }

        [Key]
        [Column("YAxisAzimuthId")]
        public int YaxisAzimuthId { get; set; }
        public string Uom { get; set; }
        public string NorthDirection { get; set; }
        public string Text { get; set; }

        [InverseProperty("YaxisAzimuth")]
        public virtual ICollection<WellLocalCrss> WellLocalCrss { get; set; }
    }
}
