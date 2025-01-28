﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigRatingBlocks
    {
        public RigRatingBlocks()
        {
            Rigs = new HashSet<Rigs>();
        }

        [Key]
        public int RatingBlockId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("RatingBlock")]
        public virtual ICollection<Rigs> Rigs { get; set; }
    }
}
