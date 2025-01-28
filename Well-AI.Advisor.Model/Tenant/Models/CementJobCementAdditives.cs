using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobCementAdditives
    {
        public CementJobCementAdditives()
        {
            CementJobCementingFluids = new HashSet<CementJobCementingFluids>();
        }

        [Key]
        public int CementAdditiveId { get; set; }
        public string NameAdd { get; set; }
        public string TypeAdd { get; set; }
        public string FormAdd { get; set; }
        public int? DensAddId { get; set; }
        public string TypeConc { get; set; }
        public int? ConcentrationId { get; set; }
        public int? AdditiveId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(AdditiveId))]
        [InverseProperty(nameof(CementJobAdditives.CementJobCementAdditives))]
        public virtual CementJobAdditives Additive { get; set; }
        [ForeignKey(nameof(ConcentrationId))]
        [InverseProperty(nameof(CementJobConcentrations.CementJobCementAdditives))]
        public virtual CementJobConcentrations Concentration { get; set; }
        [ForeignKey(nameof(DensAddId))]
        [InverseProperty(nameof(CementJobDensAdds.CementJobCementAdditives))]
        public virtual CementJobDensAdds DensAdd { get; set; }
        [InverseProperty("CementAdditive")]
        public virtual ICollection<CementJobCementingFluids> CementJobCementingFluids { get; set; }
    }
}
