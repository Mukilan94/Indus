using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class SurveyProgramCommonData
    {
        public SurveyProgramCommonData()
        {
            SurveyPrograms = new HashSet<SurveyPrograms>();
        }

        [Key]
        public int SurveyProgramCommonDataId { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

        [InverseProperty("CommonDataSurveyProgramCommonData")]
        public virtual ICollection<SurveyPrograms> SurveyPrograms { get; set; }
    }
}
