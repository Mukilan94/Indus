using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TargetCommonDatas
    {
        public TargetCommonDatas()
        {
            Targets = new HashSet<Targets>();
        }

        [Key]
        public int TargetCommonDataId { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

        [InverseProperty("CommonDataTargetCommonData")]
        public virtual ICollection<Targets> Targets { get; set; }
    }
}
