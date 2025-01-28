﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("BharunCTimReams")]
    public partial class BharunCtimReams
    {
        public BharunCtimReams()
        {
            BharunDrillingParamss = new HashSet<BharunDrillingParamss>();
        }

        [Key]
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("CtimReamUomNavigation")]
        public virtual ICollection<BharunDrillingParamss> BharunDrillingParamss { get; set; }
    }
}
