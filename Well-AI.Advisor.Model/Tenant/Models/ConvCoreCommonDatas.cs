using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreCommonDatas
    {
        public ConvCoreCommonDatas()
        {
            ConvCores = new HashSet<ConvCores>();
        }

        [Key]
        public int CommonDataId { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

        [InverseProperty("CommonData")]
        public virtual ICollection<ConvCores> ConvCores { get; set; }
    }
}
