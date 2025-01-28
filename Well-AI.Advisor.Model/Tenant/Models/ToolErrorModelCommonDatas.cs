using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorModelCommonDatas
    {
        public ToolErrorModelCommonDatas()
        {
            ToolErrorModels = new HashSet<ToolErrorModels>();
        }

        [Key]
        public int ToolErrorModelCommonDataId { get; set; }
        [Column("DTimLastChange")]
        public string DtimLastChange { get; set; }

        [InverseProperty("CommonDataToolErrorModelCommonData")]
        public virtual ICollection<ToolErrorModels> ToolErrorModels { get; set; }
    }
}
