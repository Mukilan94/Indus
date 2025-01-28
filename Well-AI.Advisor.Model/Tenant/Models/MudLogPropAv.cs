﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogPropAv
    {
        public MudLogPropAv()
        {
            MudLogChromatograph = new HashSet<MudLogChromatograph>();
        }

        [Key]
        public int PropAvId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PropAv")]
        public virtual ICollection<MudLogChromatograph> MudLogChromatograph { get; set; }
    }
}
