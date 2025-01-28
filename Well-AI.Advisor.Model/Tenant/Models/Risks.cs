using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class Risks
    {
        [Key]
        public int RiskId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public int? ObjectReferenceId { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public int? MdHoleStartId { get; set; }
        public int? MdHoleEndId { get; set; }
        public int? TvdHoleStartId { get; set; }
        public int? TvdHoleEndId { get; set; }
        public string DiaHoleUom { get; set; }
        public string SeverityLevel { get; set; }
        public string ProbabilityLevel { get; set; }
        public string Summary { get; set; }
        public string Details { get; set; }
        public string Identification { get; set; }
        public string Mitigation { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(DiaHoleUom))]
        [InverseProperty(nameof(RiskDiaHole.Risks))]
        public virtual RiskDiaHole DiaHoleUomNavigation { get; set; }
        [ForeignKey(nameof(MdHoleEndId))]
        [InverseProperty(nameof(RiskMdHoleEnds.Risks))]
        public virtual RiskMdHoleEnds MdHoleEnd { get; set; }
        [ForeignKey(nameof(MdHoleStartId))]
        [InverseProperty(nameof(RiskMdHoleStarts.Risks))]
        public virtual RiskMdHoleStarts MdHoleStart { get; set; }
        [ForeignKey(nameof(ObjectReferenceId))]
        [InverseProperty(nameof(RiskObjectReferences.Risks))]
        public virtual RiskObjectReferences ObjectReference { get; set; }
        [ForeignKey(nameof(TvdHoleEndId))]
        [InverseProperty(nameof(RiskTvdHoleEnds.Risks))]
        public virtual RiskTvdHoleEnds TvdHoleEnd { get; set; }
        [ForeignKey(nameof(TvdHoleStartId))]
        [InverseProperty(nameof(RiskTvdHoleStarts.Risks))]
        public virtual RiskTvdHoleStarts TvdHoleStart { get; set; }
    }
}
