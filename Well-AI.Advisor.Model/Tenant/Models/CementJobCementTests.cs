using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobCementTests
    {
        public CementJobCementTests()
        {
            CementJobs = new HashSet<CementJobs>();
        }

        [Key]
        public int CementTestId { get; set; }
        public int? PresTestId { get; set; }
        [Column("ETimTestId")]
        public int? EtimTestId { get; set; }
        public string CementShoeCollar { get; set; }
        public string CetRun { get; set; }
        public string CetBondQual { get; set; }
        public string CblRun { get; set; }
        public string CblBondQual { get; set; }
        public int? CblPresId { get; set; }
        public string TempSurvey { get; set; }
        [Column("ETimCementLogId")]
        public int? EtimCementLogId { get; set; }
        public int? FormPitId { get; set; }
        public string ToolCompanyPit { get; set; }
        [Column("ETimPitStartId")]
        public int? EtimPitStartId { get; set; }
        public int? MdCementTopId { get; set; }
        public string TopCementMethod { get; set; }
        [Column("TocOK")]
        public string TocOk { get; set; }
        public string JobRating { get; set; }
        public string RemedialCement { get; set; }
        public string NumRemedial { get; set; }
        public string FailureMethod { get; set; }
        public int? LinerTopId { get; set; }
        public int? LinerLapId { get; set; }
        [Column("ETimBeforeTestId")]
        public int? EtimBeforeTestId { get; set; }
        public string TestNegativeTool { get; set; }
        public int? TestNegativeEmwId { get; set; }
        public string TestPositiveTool { get; set; }
        public int? TestPositiveEmwId { get; set; }
        public string CementFoundOnTool { get; set; }
        [Column("MdDVToolJobMdDVToolId")]
        public int? MdDvtoolJobMdDvtoolId { get; set; }

        [ForeignKey(nameof(CblPresId))]
        [InverseProperty(nameof(CementJobCblPress.CementJobCementTests))]
        public virtual CementJobCblPress CblPres { get; set; }
        [ForeignKey(nameof(EtimBeforeTestId))]
        [InverseProperty(nameof(CementJobEtimBeforeTests.CementJobCementTests))]
        public virtual CementJobEtimBeforeTests EtimBeforeTest { get; set; }
        [ForeignKey(nameof(EtimCementLogId))]
        [InverseProperty(nameof(CementJobEtimCementLogs.CementJobCementTests))]
        public virtual CementJobEtimCementLogs EtimCementLog { get; set; }
        [ForeignKey(nameof(EtimPitStartId))]
        [InverseProperty(nameof(CementJobEtimPitStarts.CementJobCementTests))]
        public virtual CementJobEtimPitStarts EtimPitStart { get; set; }
        [ForeignKey(nameof(EtimTestId))]
        [InverseProperty(nameof(CementJobEtimTests.CementJobCementTests))]
        public virtual CementJobEtimTests EtimTest { get; set; }
        [ForeignKey(nameof(FormPitId))]
        [InverseProperty(nameof(CementJobFormPits.CementJobCementTests))]
        public virtual CementJobFormPits FormPit { get; set; }
        [ForeignKey(nameof(LinerLapId))]
        [InverseProperty(nameof(CementJobLinerLaps.CementJobCementTests))]
        public virtual CementJobLinerLaps LinerLap { get; set; }
        [ForeignKey(nameof(LinerTopId))]
        [InverseProperty(nameof(CementJobLinerTops.CementJobCementTests))]
        public virtual CementJobLinerTops LinerTop { get; set; }
        [ForeignKey(nameof(MdCementTopId))]
        [InverseProperty(nameof(CementJobMdCementTops.CementJobCementTests))]
        public virtual CementJobMdCementTops MdCementTop { get; set; }
        [ForeignKey(nameof(MdDvtoolJobMdDvtoolId))]
        [InverseProperty(nameof(CementJobMdDvtools.CementJobCementTests))]
        public virtual CementJobMdDvtools MdDvtoolJobMdDvtool { get; set; }
        [ForeignKey(nameof(PresTestId))]
        [InverseProperty(nameof(CementJobPresTests.CementJobCementTests))]
        public virtual CementJobPresTests PresTest { get; set; }
        [ForeignKey(nameof(TestNegativeEmwId))]
        [InverseProperty(nameof(CementJobTestNegativeEmws.CementJobCementTests))]
        public virtual CementJobTestNegativeEmws TestNegativeEmw { get; set; }
        [ForeignKey(nameof(TestPositiveEmwId))]
        [InverseProperty(nameof(CementJobTestPositiveEmws.CementJobCementTests))]
        public virtual CementJobTestPositiveEmws TestPositiveEmw { get; set; }
        [InverseProperty("CementTest")]
        public virtual ICollection<CementJobs> CementJobs { get; set; }
    }
}
