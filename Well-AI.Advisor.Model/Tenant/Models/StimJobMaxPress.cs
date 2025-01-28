﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobMaxPress
    {
        public StimJobMaxPress()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int MaxPresId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MaxPres")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
