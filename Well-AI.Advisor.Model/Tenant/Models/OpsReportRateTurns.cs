﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportRateTurns
    {
        public OpsReportRateTurns()
        {
            OpsReportTrajectoryStations = new HashSet<OpsReportTrajectoryStations>();
        }

        [Key]
        public int RateTurnId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("RateTurn")]
        public virtual ICollection<OpsReportTrajectoryStations> OpsReportTrajectoryStations { get; set; }
    }
}
