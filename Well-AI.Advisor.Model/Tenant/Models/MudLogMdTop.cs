using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogMdTop
    {
        public MudLogMdTop()
        {
            MudLogChromatograph = new HashSet<MudLogChromatograph>();
            MudLogGeologyInterval = new HashSet<MudLogGeologyInterval>();
            MudLogParameter = new HashSet<MudLogParameter>();
        }

        [Key]
        public int MdTopId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdTop")]
        public virtual ICollection<MudLogChromatograph> MudLogChromatograph { get; set; }
        [InverseProperty("MdTop")]
        public virtual ICollection<MudLogGeologyInterval> MudLogGeologyInterval { get; set; }
        [InverseProperty("MdTop")]
        public virtual ICollection<MudLogParameter> MudLogParameter { get; set; }
    }
}
