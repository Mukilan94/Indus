﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class BharunTqOnBotMxs
    {
        public BharunTqOnBotMxs()
        {
            BharunDrillingParamss = new HashSet<BharunDrillingParamss>();
        }

        [Key]
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TqOnBotMxUomNavigation")]
        public virtual ICollection<BharunDrillingParamss> BharunDrillingParamss { get; set; }
    }
}
