﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreIbutMns
    {
        public ConvCoreIbutMns()
        {
            ConvCoreChromatographs = new HashSet<ConvCoreChromatographs>();
        }

        [Key]
        public int IbutMnId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("IbutMn")]
        public virtual ICollection<ConvCoreChromatographs> ConvCoreChromatographs { get; set; }
    }
}
