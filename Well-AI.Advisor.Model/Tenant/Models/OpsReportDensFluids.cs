using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportDensFluids
    {
        public OpsReportDensFluids()
        {
            OpsReportPitVolumes = new HashSet<OpsReportPitVolumes>();
        }

        [Key]
        public int DensFluidId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DensFluid")]
        public virtual ICollection<OpsReportPitVolumes> OpsReportPitVolumes { get; set; }
    }
}
