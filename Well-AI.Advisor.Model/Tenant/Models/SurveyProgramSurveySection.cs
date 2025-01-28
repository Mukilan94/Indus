using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class SurveyProgramSurveySection
    {
        [Key]
        public int SurveySectionId { get; set; }
        public string Sequence { get; set; }
        public string Name { get; set; }
        public int? MdStartId { get; set; }
        public int? MdEndId { get; set; }
        public string NameSurveyCompany { get; set; }
        public string NameTool { get; set; }
        public string TypeTool { get; set; }
        public string ModelError { get; set; }
        public string Overwrite { get; set; }
        public int? FrequencyMxId { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }
        public string Uid { get; set; }
        public int? SurveyProgramId { get; set; }

        [ForeignKey(nameof(FrequencyMxId))]
        [InverseProperty(nameof(SurveyProgramFrequencyMx.SurveyProgramSurveySection))]
        public virtual SurveyProgramFrequencyMx FrequencyMx { get; set; }
        [ForeignKey(nameof(MdEndId))]
        [InverseProperty(nameof(SurveyProgramMdEnd.SurveyProgramSurveySection))]
        public virtual SurveyProgramMdEnd MdEnd { get; set; }
        [ForeignKey(nameof(MdStartId))]
        [InverseProperty(nameof(SurveyProgramMdStart.SurveyProgramSurveySection))]
        public virtual SurveyProgramMdStart MdStart { get; set; }
        [ForeignKey(nameof(SurveyProgramId))]
        [InverseProperty(nameof(SurveyPrograms.SurveyProgramSurveySection))]
        public virtual SurveyPrograms SurveyProgram { get; set; }
    }
}
