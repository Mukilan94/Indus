using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class Bharuns
    {
        [Key]
        public string Uid { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public string TubularUidRef { get; set; }
        [Column("DTimStart")]
        public DateTime DtimStart { get; set; }
        [Column("DTimStop")]
        public DateTime DtimStop { get; set; }
        [Column("DTimStartDrilling")]
        public DateTime DtimStartDrilling { get; set; }
        [Column("DTimStopDrilling")]
        public DateTime DtimStopDrilling { get; set; }
        public string PlanDoglegUom { get; set; }
        public string ActDoglegUom { get; set; }
        public string ActDoglegMxUom { get; set; }
        public string StatusBha { get; set; }
        public int NumBitRun { get; set; }
        public int NumStringRun { get; set; }
        public string ReasonTrip { get; set; }
        public string ObjectiveBha { get; set; }
        public string DrillingParamsUid { get; set; }
        public int? CommonDataId { get; set; }
        public string UidWell { get; set; }
        public string UidWellbore { get; set; }

        [ForeignKey(nameof(ActDoglegMxUom))]
        [InverseProperty(nameof(BharunActDoglegMxs.Bharuns))]
        public virtual BharunActDoglegMxs ActDoglegMxUomNavigation { get; set; }
        [ForeignKey(nameof(ActDoglegUom))]
        [InverseProperty(nameof(BharunActDoglegs.Bharuns))]
        public virtual BharunActDoglegs ActDoglegUomNavigation { get; set; }
        [ForeignKey(nameof(CommonDataId))]
        [InverseProperty(nameof(BharunCommonDatas.Bharuns))]
        public virtual BharunCommonDatas CommonData { get; set; }
        [ForeignKey(nameof(DrillingParamsUid))]
        [InverseProperty(nameof(BharunDrillingParamss.Bharuns))]
        public virtual BharunDrillingParamss DrillingParamsU { get; set; }
        [ForeignKey(nameof(PlanDoglegUom))]
        [InverseProperty(nameof(BharunPlanDoglegs.Bharuns))]
        public virtual BharunPlanDoglegs PlanDoglegUomNavigation { get; set; }
        [ForeignKey(nameof(TubularUidRef))]
        [InverseProperty(nameof(BharunTubulars.Bharuns))]
        public virtual BharunTubulars TubularUidRefNavigation { get; set; }
    }
}
