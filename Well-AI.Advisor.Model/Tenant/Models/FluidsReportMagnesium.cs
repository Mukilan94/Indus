﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReportMagnesium
    {
        public FluidsReportMagnesium()
        {
            FluidsReportFluid = new HashSet<FluidsReportFluid>();
        }

        [Key]
        public int MagnesiumId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Magnesium")]
        public virtual ICollection<FluidsReportFluid> FluidsReportFluid { get; set; }
    }
}
