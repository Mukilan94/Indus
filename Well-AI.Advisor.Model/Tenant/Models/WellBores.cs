using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellBores
    {
        [Key]
        public int WellboreId { get; set; }
        public string NameWell { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        [Column("SuffixAPI")]
        public string SuffixApi { get; set; }
        public string NumGovt { get; set; }
        public string StatusWellbore { get; set; }
        public string PurposeWellbore { get; set; }
        public string TypeWellbore { get; set; }
        public string Shape { get; set; }
        [Column("DTimKickoff")]
        public string DtimKickoff { get; set; }
        public int? MdId { get; set; }
        public int? TvdId { get; set; }
        public int? MdKickoffId { get; set; }
        public int? TvdKickoffId { get; set; }
        public int? MdPlannedId { get; set; }
        public int? TvdPlannedId { get; set; }
        public int? MdSubSeaPlannedId { get; set; }
        public int? TvdSubSeaPlannedId { get; set; }
        public int? DayTargetId { get; set; }
        public int? CommonDataWellBoreCommonDataId { get; set; }
        public string Uid { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(CommonDataWellBoreCommonDataId))]
        [InverseProperty(nameof(WellBoreCommonData.WellBores))]
        public virtual WellBoreCommonData CommonDataWellBoreCommonData { get; set; }
        [ForeignKey(nameof(DayTargetId))]
        [InverseProperty(nameof(WellboreDayTarget.WellBores))]
        public virtual WellboreDayTarget DayTarget { get; set; }
        [ForeignKey(nameof(MdId))]
        [InverseProperty(nameof(WellboreMd.WellBores))]
        public virtual WellboreMd Md { get; set; }
        [ForeignKey(nameof(MdKickoffId))]
        [InverseProperty(nameof(WellboreMdKickoff.WellBores))]
        public virtual WellboreMdKickoff MdKickoff { get; set; }
        [ForeignKey(nameof(MdPlannedId))]
        [InverseProperty(nameof(WellboreMdPlanned.WellBores))]
        public virtual WellboreMdPlanned MdPlanned { get; set; }
        [ForeignKey(nameof(MdSubSeaPlannedId))]
        [InverseProperty(nameof(WellboreMdSubSeaPlanned.WellBores))]
        public virtual WellboreMdSubSeaPlanned MdSubSeaPlanned { get; set; }
        [ForeignKey(nameof(TvdId))]
        [InverseProperty(nameof(WellboreTvd.WellBores))]
        public virtual WellboreTvd Tvd { get; set; }
        [ForeignKey(nameof(TvdKickoffId))]
        [InverseProperty(nameof(WellboreTvdKickoff.WellBores))]
        public virtual WellboreTvdKickoff TvdKickoff { get; set; }
        [ForeignKey(nameof(TvdPlannedId))]
        [InverseProperty(nameof(WellboreTvdPlanned.WellBores))]
        public virtual WellboreTvdPlanned TvdPlanned { get; set; }
        [ForeignKey(nameof(TvdSubSeaPlannedId))]
        [InverseProperty(nameof(WellboreTvdSubSeaPlanned.WellBores))]
        public virtual WellboreTvdSubSeaPlanned TvdSubSeaPlanned { get; set; }
    }
}
