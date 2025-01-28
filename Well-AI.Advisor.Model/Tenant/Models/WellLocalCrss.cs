using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("WellLocalCRSs")]
    public partial class WellLocalCrss
    {
        public WellLocalCrss()
        {
            WellCrss = new HashSet<WellCrss>();
        }

        [Key]
        [Column("LocalCRSId")]
        public int LocalCrsid { get; set; }
        public string UsesWellAsOrigin { get; set; }
        [Column("YAxisAzimuthId")]
        public int? YaxisAzimuthId { get; set; }
        [Column("XRotationCounterClockwise")]
        public string XrotationCounterClockwise { get; set; }

        [ForeignKey(nameof(YaxisAzimuthId))]
        [InverseProperty(nameof(WellYaxisAzimuths.WellLocalCrss))]
        public virtual WellYaxisAzimuths YaxisAzimuth { get; set; }
        [InverseProperty("LocalCrs")]
        public virtual ICollection<WellCrss> WellCrss { get; set; }
    }
}
