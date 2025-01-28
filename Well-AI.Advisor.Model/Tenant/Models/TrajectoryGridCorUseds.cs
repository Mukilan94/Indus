using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryGridCorUseds
    {
        public TrajectoryGridCorUseds()
        {
            Trajectorys = new HashSet<Trajectorys>();
        }

        [Key]
        public int GridCorUsedId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GridCorUsed")]
        public virtual ICollection<Trajectorys> Trajectorys { get; set; }
    }
}
