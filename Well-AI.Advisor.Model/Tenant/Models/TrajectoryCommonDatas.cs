using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryCommonDatas
    {
        public TrajectoryCommonDatas()
        {
            Trajectorys = new HashSet<Trajectorys>();
        }

        [Key]
        public int TrajectoryCommonDataId { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

        [InverseProperty("CommonDataTrajectoryCommonData")]
        public virtual ICollection<Trajectorys> Trajectorys { get; set; }
    }
}
