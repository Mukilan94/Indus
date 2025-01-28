﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReportLcm
    {
        public FluidsReportLcm()
        {
            FluidsReportFluid = new HashSet<FluidsReportFluid>();
        }

        [Key]
        public int LcmId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Lcm")]
        public virtual ICollection<FluidsReportFluid> FluidsReportFluid { get; set; }
    }
}
