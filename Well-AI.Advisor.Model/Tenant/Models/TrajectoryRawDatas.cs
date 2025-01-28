using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryRawDatas
    {
        public TrajectoryRawDatas()
        {
            TrajectoryStations = new HashSet<TrajectoryStations>();
        }

        [Key]
        public int RawDataId { get; set; }
        public int? GravAxialRawId { get; set; }
        public int? GravTran1RawId { get; set; }
        public int? GravTran2RawId { get; set; }
        public int? MagAxialRawId { get; set; }
        public int? MagTran1RawId { get; set; }
        public int? MagTran2RawId { get; set; }

        [ForeignKey(nameof(GravAxialRawId))]
        [InverseProperty(nameof(TrajectoryGravAxialRaws.TrajectoryRawDatas))]
        public virtual TrajectoryGravAxialRaws GravAxialRaw { get; set; }
        [ForeignKey(nameof(GravTran1RawId))]
        [InverseProperty(nameof(TrajectoryGravTran1Raws.TrajectoryRawDatas))]
        public virtual TrajectoryGravTran1Raws GravTran1Raw { get; set; }
        [ForeignKey(nameof(GravTran2RawId))]
        [InverseProperty(nameof(TrajectoryGravTran2Raws.TrajectoryRawDatas))]
        public virtual TrajectoryGravTran2Raws GravTran2Raw { get; set; }
        [ForeignKey(nameof(MagAxialRawId))]
        [InverseProperty(nameof(TrajectoryMagAxialRaws.TrajectoryRawDatas))]
        public virtual TrajectoryMagAxialRaws MagAxialRaw { get; set; }
        [ForeignKey(nameof(MagTran1RawId))]
        [InverseProperty(nameof(TrajectoryMagTran1Raws.TrajectoryRawDatas))]
        public virtual TrajectoryMagTran1Raws MagTran1Raw { get; set; }
        [ForeignKey(nameof(MagTran2RawId))]
        [InverseProperty(nameof(TrajectoryMagTran2Raws.TrajectoryRawDatas))]
        public virtual TrajectoryMagTran2Raws MagTran2Raw { get; set; }
        [InverseProperty("RawData")]
        public virtual ICollection<TrajectoryStations> TrajectoryStations { get; set; }
    }
}
