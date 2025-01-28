using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigCommonDatas
    {
        public RigCommonDatas()
        {
            Rigs = new HashSet<Rigs>();
        }

        [Key]
        public int RigCommonDataId { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

        [InverseProperty("CommonDataRigCommonData")]
        public virtual ICollection<Rigs> Rigs { get; set; }
    }
}
