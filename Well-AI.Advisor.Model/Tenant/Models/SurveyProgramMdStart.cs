using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class SurveyProgramMdStart
    {
        public SurveyProgramMdStart()
        {
            SurveyProgramSurveySection = new HashSet<SurveyProgramSurveySection>();
        }

        [Key]
        public int MdStartId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdStart")]
        public virtual ICollection<SurveyProgramSurveySection> SurveyProgramSurveySection { get; set; }
    }
}
