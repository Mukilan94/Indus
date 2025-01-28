using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class SurveyProgramMdEnd
    {
        public SurveyProgramMdEnd()
        {
            SurveyProgramSurveySection = new HashSet<SurveyProgramSurveySection>();
        }

        [Key]
        public int MdEndId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdEnd")]
        public virtual ICollection<SurveyProgramSurveySection> SurveyProgramSurveySection { get; set; }
    }
}
