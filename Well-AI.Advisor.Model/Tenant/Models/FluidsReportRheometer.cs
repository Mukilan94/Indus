using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReportRheometer
    {
        [Key]
        public int RheometerId { get; set; }
        public int? TempRheomId { get; set; }
        public int? PresRheomId { get; set; }
        public string Vis3Rpm { get; set; }
        public string Vis6Rpm { get; set; }
        public string Vis100Rpm { get; set; }
        public string Vis200Rpm { get; set; }
        public string Vis300Rpm { get; set; }
        public string Vis600Rpm { get; set; }
        public string Uid { get; set; }
        public string FluidsReportFluidUid { get; set; }

        [ForeignKey(nameof(FluidsReportFluidUid))]
        [InverseProperty(nameof(FluidsReportFluid.FluidsReportRheometer))]
        public virtual FluidsReportFluid FluidsReportFluidU { get; set; }
        [ForeignKey(nameof(PresRheomId))]
        [InverseProperty(nameof(FluidsReportPresRheom.FluidsReportRheometer))]
        public virtual FluidsReportPresRheom PresRheom { get; set; }
        [ForeignKey(nameof(TempRheomId))]
        [InverseProperty(nameof(FluidsReportTempRheom.FluidsReportRheometer))]
        public virtual FluidsReportTempRheom TempRheom { get; set; }
    }
}
