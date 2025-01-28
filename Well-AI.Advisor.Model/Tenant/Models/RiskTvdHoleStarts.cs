﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RiskTvdHoleStarts
    {
        public RiskTvdHoleStarts()
        {
            Risks = new HashSet<Risks>();
        }

        [Key]
        public int TvdHoleStartId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TvdHoleStart")]
        public virtual ICollection<Risks> Risks { get; set; }
    }
}
