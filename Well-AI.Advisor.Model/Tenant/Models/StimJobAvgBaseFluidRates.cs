﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobAvgBaseFluidRates
    {
        public StimJobAvgBaseFluidRates()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int AvgBaseFluidRateId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AvgBaseFluidRate")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("AvgBaseFluidRate")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
