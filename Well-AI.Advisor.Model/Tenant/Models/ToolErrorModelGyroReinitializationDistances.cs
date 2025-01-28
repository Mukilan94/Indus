using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorModelGyroReinitializationDistances
    {
        public ToolErrorModelGyroReinitializationDistances()
        {
            ToolErrorModelModelParameter = new HashSet<ToolErrorModelModelParameter>();
        }

        [Key]
        public int GyroReinitializationDistanceId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GyroReinitializationDistance")]
        public virtual ICollection<ToolErrorModelModelParameter> ToolErrorModelModelParameter { get; set; }
    }
}
