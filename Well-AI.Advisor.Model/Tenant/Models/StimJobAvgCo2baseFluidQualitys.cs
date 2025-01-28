using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("StimJobAvgCO2BaseFluidQualitys")]
    public partial class StimJobAvgCo2baseFluidQualitys
    {
        public StimJobAvgCo2baseFluidQualitys()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("AvgCO2BaseFluidQualityId")]
        public int AvgCo2baseFluidQualityId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AvgCo2baseFluidQuality")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("AvgCo2baseFluidQuality")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
