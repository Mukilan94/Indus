﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreGasBackgnds
    {
        public ConvCoreGasBackgnds()
        {
            ConvCoreMudGass = new HashSet<ConvCoreMudGass>();
        }

        [Key]
        public int GasBackgndId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GasBackgnd")]
        public virtual ICollection<ConvCoreMudGass> ConvCoreMudGass { get; set; }
    }
}
