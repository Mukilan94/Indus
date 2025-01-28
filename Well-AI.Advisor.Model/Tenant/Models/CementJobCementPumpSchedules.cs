using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobCementPumpSchedules
    {
        public CementJobCementPumpSchedules()
        {
            CementJobCementingFluids = new HashSet<CementJobCementingFluids>();
        }

        [Key]
        public int CementPumpScheduleId { get; set; }
        [Column("ETimPumpId")]
        public int? EtimPumpId { get; set; }
        public int? RatePumpId { get; set; }
        public int? VolPumpId { get; set; }
        public string StrokePump { get; set; }
        public int? PresBackId { get; set; }
        [Column("ETimShutdownId")]
        public int? EtimShutdownId { get; set; }
        public string Comments { get; set; }

        [ForeignKey(nameof(EtimPumpId))]
        [InverseProperty(nameof(CementJobEtimPumps.CementJobCementPumpSchedules))]
        public virtual CementJobEtimPumps EtimPump { get; set; }
        [ForeignKey(nameof(EtimShutdownId))]
        [InverseProperty(nameof(CementJobEtimShutdowns.CementJobCementPumpSchedules))]
        public virtual CementJobEtimShutdowns EtimShutdown { get; set; }
        [ForeignKey(nameof(PresBackId))]
        [InverseProperty(nameof(CementJobPresBacks.CementJobCementPumpSchedules))]
        public virtual CementJobPresBacks PresBack { get; set; }
        [ForeignKey(nameof(RatePumpId))]
        [InverseProperty(nameof(CementJobRatePumps.CementJobCementPumpSchedules))]
        public virtual CementJobRatePumps RatePump { get; set; }
        [ForeignKey(nameof(VolPumpId))]
        [InverseProperty(nameof(CementJobVolPumps.CementJobCementPumpSchedules))]
        public virtual CementJobVolPumps VolPump { get; set; }
        [InverseProperty("CementPumpSchedule")]
        public virtual ICollection<CementJobCementingFluids> CementJobCementingFluids { get; set; }
    }
}
