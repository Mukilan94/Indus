﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreEthAvs
    {
        public ConvCoreEthAvs()
        {
            ConvCoreChromatographs = new HashSet<ConvCoreChromatographs>();
        }

        [Key]
        public int EthAvId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EthAv")]
        public virtual ICollection<ConvCoreChromatographs> ConvCoreChromatographs { get; set; }
    }
}
