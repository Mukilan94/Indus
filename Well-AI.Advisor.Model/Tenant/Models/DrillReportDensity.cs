﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportDensity
    {
        public DrillReportDensity()
        {
            DrillReportFluids = new HashSet<DrillReportFluids>();
        }

        [Key]
        public int DensityId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Density")]
        public virtual ICollection<DrillReportFluids> DrillReportFluids { get; set; }
    }
}
