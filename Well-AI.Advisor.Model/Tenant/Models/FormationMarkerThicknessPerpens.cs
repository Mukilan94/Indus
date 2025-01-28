using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FormationMarkerThicknessPerpens
    {
        public FormationMarkerThicknessPerpens()
        {
            FormationMarkers = new HashSet<FormationMarkers>();
        }

        [Key]
        public int ThicknessPerpenId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ThicknessPerpen")]
        public virtual ICollection<FormationMarkers> FormationMarkers { get; set; }
    }
}
