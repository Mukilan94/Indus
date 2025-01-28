using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorModelAuthorizations
    {
        public ToolErrorModelAuthorizations()
        {
            ToolErrorModels = new HashSet<ToolErrorModels>();
        }

        [Key]
        public int AuthorizationId { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
        public string Authority { get; set; }
        public string Status { get; set; }
        public string Version { get; set; }
        public string Comment { get; set; }

        [InverseProperty("Authorization")]
        public virtual ICollection<ToolErrorModels> ToolErrorModels { get; set; }
    }
}
