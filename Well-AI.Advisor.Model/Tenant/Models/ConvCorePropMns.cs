﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCorePropMns
    {
        public ConvCorePropMns()
        {
            ConvCoreChromatographs = new HashSet<ConvCoreChromatographs>();
        }

        [Key]
        public int PropMnId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PropMn")]
        public virtual ICollection<ConvCoreChromatographs> ConvCoreChromatographs { get; set; }
    }
}
