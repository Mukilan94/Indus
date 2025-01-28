using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobBottomholeStaticTemperatures
    {
        public StimJobBottomholeStaticTemperatures()
        {
            StimJobs = new HashSet<StimJobs>();
        }

        [Key]
        public int BottomholeStaticTemperatureId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("BottomholeStaticTemperature")]
        public virtual ICollection<StimJobs> StimJobs { get; set; }
    }
}
