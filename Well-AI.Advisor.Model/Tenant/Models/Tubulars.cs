using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class Tubulars
    {
        public Tubulars()
        {
            OpsReports = new HashSet<OpsReports>();
            TubularComponent = new HashSet<TubularComponent>();
        }

        [Key]
        public int TubularId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public string TypeTubularAssy { get; set; }
        public string ValveFloat { get; set; }
        public string SourceNuclear { get; set; }
        public int? DiaHoleAssyId { get; set; }
        public int? CommonDataTubularyCommonDataId { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(CommonDataTubularyCommonDataId))]
        [InverseProperty(nameof(TubularCommonDatas.Tubulars))]
        public virtual TubularCommonDatas CommonDataTubularyCommonData { get; set; }
        [ForeignKey(nameof(DiaHoleAssyId))]
        [InverseProperty(nameof(TubularDiaHoleAssy.Tubulars))]
        public virtual TubularDiaHoleAssy DiaHoleAssy { get; set; }
        [InverseProperty("Tubular")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
        [InverseProperty("Tubular")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
    }
}
