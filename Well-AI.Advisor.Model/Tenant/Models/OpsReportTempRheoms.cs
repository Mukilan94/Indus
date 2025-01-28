﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportTempRheoms
    {
        public OpsReportTempRheoms()
        {
            OpsReportRheometers = new HashSet<OpsReportRheometers>();
        }

        [Key]
        public int TempRheomId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TempRheom")]
        public virtual ICollection<OpsReportRheometers> OpsReportRheometers { get; set; }
    }
}
