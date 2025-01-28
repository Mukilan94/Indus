using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CementJobETimPresHelds")]
    public partial class CementJobEtimPresHelds
    {
        public CementJobEtimPresHelds()
        {
            CementJobCementStages = new HashSet<CementJobCementStages>();
        }

        [Key]
        [Column("ETimPresHeldId")]
        public int EtimPresHeldId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimPresHeld")]
        public virtual ICollection<CementJobCementStages> CementJobCementStages { get; set; }
    }
}
