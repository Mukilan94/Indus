﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class BharunInclStarts
    {
        public BharunInclStarts()
        {
            BharunDrillingParamss = new HashSet<BharunDrillingParamss>();
        }

        [Key]
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("InclStartUomNavigation")]
        public virtual ICollection<BharunDrillingParamss> BharunDrillingParamss { get; set; }
    }
}
