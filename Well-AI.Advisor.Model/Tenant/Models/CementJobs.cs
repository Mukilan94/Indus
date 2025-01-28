using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobs
    {
        [Key]
        public int CementJobId { get; set; }
        public string Uid { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public string JobType { get; set; }
        public string JobConfig { get; set; }
        [Column("DTimJob")]
        public string DtimJob { get; set; }
        public string NameCementedString { get; set; }
        public string NameWorkString { get; set; }
        public string Contractor { get; set; }
        public string CementEngr { get; set; }
        public string OffshoreJob { get; set; }
        public int? MdWaterId { get; set; }
        public string ReturnsToSeabed { get; set; }
        public string Reciprocating { get; set; }
        public int? WocId { get; set; }
        public int? MdPlugTopId { get; set; }
        public int? MdPlugBotId { get; set; }
        public int? MdHoleId { get; set; }
        public int? MdShoeId { get; set; }
        public int? TvdShoeId { get; set; }
        public int? MdStringSetId { get; set; }
        public int? TvdStringSetId { get; set; }
        public int? CementStageId { get; set; }
        public int? CementTestId { get; set; }
        public string TypePlug { get; set; }
        public string NameCementString { get; set; }
        [Column("DTimPlugSet")]
        public string DtimPlugSet { get; set; }
        public string CementDrillOut { get; set; }
        [Column("DTimCementDrillOut")]
        public string DtimCementDrillOut { get; set; }
        public string TypeSqueeze { get; set; }
        public int? MdSqueezeId { get; set; }
        [Column("DTimSqueeze")]
        public string DtimSqueeze { get; set; }
        public string ToolCompany { get; set; }
        public string TypeTool { get; set; }
        [Column("DTimPipeRotStart")]
        public string DtimPipeRotStart { get; set; }
        [Column("DTimPipeRotEnd")]
        public string DtimPipeRotEnd { get; set; }
        public int? RpmPipeId { get; set; }
        public int? TqInitPipeRotId { get; set; }
        public int? TqPipeAvId { get; set; }
        public int? TqPipeMxId { get; set; }
        [Column("DTimRecipStart")]
        public string DtimRecipStart { get; set; }
        [Column("DTimRecipEnd")]
        public string DtimRecipEnd { get; set; }
        public int? OverPullId { get; set; }
        public int? SlackOffId { get; set; }
        public int? RpmPipeRecipId { get; set; }
        public int? LenPipeRecipStrokeId { get; set; }
        public string CoilTubing { get; set; }
        public int? CommonDataId { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(CementStageId))]
        [InverseProperty(nameof(CementJobCementStages.CementJobs))]
        public virtual CementJobCementStages CementStage { get; set; }
        [ForeignKey(nameof(CementTestId))]
        [InverseProperty(nameof(CementJobCementTests.CementJobs))]
        public virtual CementJobCementTests CementTest { get; set; }
        [ForeignKey(nameof(CommonDataId))]
        [InverseProperty(nameof(CementJobCommonDatas.CementJobs))]
        public virtual CementJobCommonDatas CommonData { get; set; }
        [ForeignKey(nameof(LenPipeRecipStrokeId))]
        [InverseProperty(nameof(CementJobLenPipeRecipStrokes.CementJobs))]
        public virtual CementJobLenPipeRecipStrokes LenPipeRecipStroke { get; set; }
        [ForeignKey(nameof(MdHoleId))]
        [InverseProperty(nameof(CementJobMdHoles.CementJobs))]
        public virtual CementJobMdHoles MdHole { get; set; }
        [ForeignKey(nameof(MdPlugBotId))]
        [InverseProperty(nameof(CementJobMdPlugBots.CementJobs))]
        public virtual CementJobMdPlugBots MdPlugBot { get; set; }
        [ForeignKey(nameof(MdPlugTopId))]
        [InverseProperty(nameof(CementJobMdPlugTops.CementJobs))]
        public virtual CementJobMdPlugTops MdPlugTop { get; set; }
        [ForeignKey(nameof(MdShoeId))]
        [InverseProperty(nameof(CementJobMdShoes.CementJobs))]
        public virtual CementJobMdShoes MdShoe { get; set; }
        [ForeignKey(nameof(MdSqueezeId))]
        [InverseProperty(nameof(CementJobMdSqueezes.CementJobs))]
        public virtual CementJobMdSqueezes MdSqueeze { get; set; }
        [ForeignKey(nameof(MdStringSetId))]
        [InverseProperty(nameof(CementJobMdStringSets.CementJobs))]
        public virtual CementJobMdStringSets MdStringSet { get; set; }
        [ForeignKey(nameof(MdWaterId))]
        [InverseProperty(nameof(CementJobMdWaters.CementJobs))]
        public virtual CementJobMdWaters MdWater { get; set; }
        [ForeignKey(nameof(OverPullId))]
        [InverseProperty(nameof(CementJobOverPulls.CementJobs))]
        public virtual CementJobOverPulls OverPull { get; set; }
        [ForeignKey(nameof(RpmPipeId))]
        [InverseProperty(nameof(CementJobRpmPipes.CementJobs))]
        public virtual CementJobRpmPipes RpmPipe { get; set; }
        [ForeignKey(nameof(RpmPipeRecipId))]
        [InverseProperty(nameof(CementJobRpmPipeRecips.CementJobs))]
        public virtual CementJobRpmPipeRecips RpmPipeRecip { get; set; }
        [ForeignKey(nameof(SlackOffId))]
        [InverseProperty(nameof(CementJobSlackOffs.CementJobs))]
        public virtual CementJobSlackOffs SlackOff { get; set; }
        [ForeignKey(nameof(TqInitPipeRotId))]
        [InverseProperty(nameof(CementJobTqInitPipeRots.CementJobs))]
        public virtual CementJobTqInitPipeRots TqInitPipeRot { get; set; }
        [ForeignKey(nameof(TqPipeAvId))]
        [InverseProperty(nameof(CementJobTqPipeAvs.CementJobs))]
        public virtual CementJobTqPipeAvs TqPipeAv { get; set; }
        [ForeignKey(nameof(TqPipeMxId))]
        [InverseProperty(nameof(CementJobTqPipeMxs.CementJobs))]
        public virtual CementJobTqPipeMxs TqPipeMx { get; set; }
        [ForeignKey(nameof(TvdShoeId))]
        [InverseProperty(nameof(CementJobTvdShoes.CementJobs))]
        public virtual CementJobTvdShoes TvdShoe { get; set; }
        [ForeignKey(nameof(TvdStringSetId))]
        [InverseProperty(nameof(CementJobTvdStringSets.CementJobs))]
        public virtual CementJobTvdStringSets TvdStringSet { get; set; }
        [ForeignKey(nameof(WocId))]
        [InverseProperty(nameof(CementJobWocs.CementJobs))]
        public virtual CementJobWocs Woc { get; set; }
    }
}
