using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigPowerDrawWork
    {
        public RigPowerDrawWork()
        {
            Rigs = new HashSet<Rigs>();
        }

        [Key]
        public int PowerDrawWorksId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PowerDrawWorks")]
        public virtual ICollection<Rigs> Rigs { get; set; }
    }
}
