using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPresKellyHoses
    {
        public OpsReportPresKellyHoses()
        {
            OpsReportHses = new HashSet<OpsReportHses>();
        }

        [Key]
        public int PresKellyHoseId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresKellyHose")]
        public virtual ICollection<OpsReportHses> OpsReportHses { get; set; }
    }
}
