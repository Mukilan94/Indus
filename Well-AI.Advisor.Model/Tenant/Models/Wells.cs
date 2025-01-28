using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class Wells
    {
        public Wells()
        {
            WellCrss = new HashSet<WellCrss>();
            WellDatums = new HashSet<WellDatums>();
            WellReferencePoints = new HashSet<WellReferencePoints>();
        }

        [Key]
        public int WellId { get; set; }
        public string Name { get; set; }
        public string NameLegal { get; set; }
        public string NumLicense { get; set; }
        public string NumGovt { get; set; }
        [Column("DTimLicense")]
        public string DtimLicense { get; set; }
        public string Field { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string Block { get; set; }
        public string TimeZone { get; set; }
        public string Operator { get; set; }
        public string OperatorDiv { get; set; }
        public int? PcInterestId { get; set; }
        [Column("NumAPI")]
        public string NumApi { get; set; }
        public string StatusWell { get; set; }
        public string PurposeWell { get; set; }
        [Column("DTimSpud")]
        public string DtimSpud { get; set; }
        [Column("DTimPa")]
        public string DtimPa { get; set; }
        public int? WellheadElevationElevationId { get; set; }
        public int? GroundElevationId { get; set; }
        public int? WaterDepthId { get; set; }
        public int? WellLocationLocationId { get; set; }
        public int? CommonDataId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(CommonDataId))]
        [InverseProperty(nameof(WellCommonDatas.Wells))]
        public virtual WellCommonDatas CommonData { get; set; }
        [ForeignKey(nameof(GroundElevationId))]
        [InverseProperty(nameof(WellGroundElevations.Wells))]
        public virtual WellGroundElevations GroundElevation { get; set; }
        [ForeignKey(nameof(PcInterestId))]
        [InverseProperty(nameof(WellPcInterests.Wells))]
        public virtual WellPcInterests PcInterest { get; set; }
        [ForeignKey(nameof(WaterDepthId))]
        [InverseProperty(nameof(WellWaterDepths.Wells))]
        public virtual WellWaterDepths WaterDepth { get; set; }
        [ForeignKey(nameof(WellLocationLocationId))]
        [InverseProperty(nameof(WellLocations.Wells))]
        public virtual WellLocations WellLocationLocation { get; set; }
        [ForeignKey(nameof(WellheadElevationElevationId))]
        [InverseProperty(nameof(WellheadElevations.Wells))]
        public virtual WellheadElevations WellheadElevationElevation { get; set; }
        [InverseProperty("Well")]
        public virtual ICollection<WellCrss> WellCrss { get; set; }
        [InverseProperty("Well")]
        public virtual ICollection<WellDatums> WellDatums { get; set; }
        [InverseProperty("Well")]
        public virtual ICollection<WellReferencePoints> WellReferencePoints { get; set; }
    }
}
