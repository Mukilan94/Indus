﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobFormPits
    {
        public CementJobFormPits()
        {
            CementJobCementTests = new HashSet<CementJobCementTests>();
        }

        [Key]
        public int FormPitId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FormPit")]
        public virtual ICollection<CementJobCementTests> CementJobCementTests { get; set; }
    }
}
