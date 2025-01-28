using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryMdMxs
    {
        public TrajectoryMdMxs()
        {
            Trajectorys = new HashSet<Trajectorys>();
        }

        [Key]
        public int MdMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdMx")]
        public virtual ICollection<Trajectorys> Trajectorys { get; set; }
    }
}
