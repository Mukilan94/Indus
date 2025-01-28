using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryMagDeclUseds
    {
        public TrajectoryMagDeclUseds()
        {
            Trajectorys = new HashSet<Trajectorys>();
        }

        [Key]
        public int MagDeclUsedId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MagDeclUsed")]
        public virtual ICollection<Trajectorys> Trajectorys { get; set; }
    }
}
