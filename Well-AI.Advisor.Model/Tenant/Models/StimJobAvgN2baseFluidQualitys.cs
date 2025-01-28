using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("StimJobAvgN2BaseFluidQualitys")]
    public partial class StimJobAvgN2baseFluidQualitys
    {
        public StimJobAvgN2baseFluidQualitys()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("AvgN2BaseFluidQualityId")]
        public int AvgN2baseFluidQualityId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AvgN2baseFluidQuality")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        [InverseProperty("AvgN2baseFluidQuality")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
