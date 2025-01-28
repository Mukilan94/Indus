using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class SurveyPrograms
    {
        public SurveyPrograms()
        {
            SurveyProgramSurveySection = new HashSet<SurveyProgramSurveySection>();
        }

        [Key]
        public int SurveyProgramId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public string SurveyVer { get; set; }
        [Column("DTimTrajProg")]
        public string DtimTrajProg { get; set; }
        public string Engineer { get; set; }
        public string Final { get; set; }
        public int? CommonDataSurveyProgramCommonDataId { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(CommonDataSurveyProgramCommonDataId))]
        [InverseProperty(nameof(SurveyProgramCommonData.SurveyPrograms))]
        public virtual SurveyProgramCommonData CommonDataSurveyProgramCommonData { get; set; }
        [InverseProperty("SurveyProgram")]
        public virtual ICollection<SurveyProgramSurveySection> SurveyProgramSurveySection { get; set; }
    }
}
